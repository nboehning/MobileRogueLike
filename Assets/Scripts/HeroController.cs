using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

    // Settings Variables
    public bool isSwipeShoot;
    public bool isSwipeMove;

    // Movement Variables
	public float speed = 0.5f;
	public bool right, left, up, down;

    // Shooting Variables
    Vector2 prevPosition;
    Vector2 curPosition;
    float touchDelta;
    public int iComfort = 20;

    // Animation Controller
    public Animator animator;

    // Orb Variables
	public float orbSpeed = 20f;
	GameObject orbPrefab;

    // Other
    GameObject deathPanel;
    private int curLevel = 1;
    private int levelToBeat;
	// Use this for initialization
	void Start () 
	{
        Time.timeScale = 1f;
		animator = GetComponent<Animator> ();
		right = left = up = down = false;
		orbPrefab = Resources.Load ("Orb") as GameObject;
        deathPanel = GameObject.Find("DeathPanel");
        deathPanel.SetActive(false);
	    string firstTimeCheck = PlayerPrefs.GetString("IsFirstTime");
	    if (firstTimeCheck != "yes")
	    {
	        levelToBeat = 1;
	    }
	    else
	    {
	        levelToBeat = PlayerPrefs.GetInt("LevelToBeat");
	    }
	}

	void Update()
	{
#if UNITY_EDITOR
        MoveCharacter();
        MoveCharacterMobile();
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnOrb();
        }
        else if (Input.touchCount > 0)
        {
            if (isSwipeShoot)
            {
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        prevPosition = Input.GetTouch(0).position;
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        curPosition = Input.GetTouch(0).position;

                        touchDelta = curPosition.magnitude - prevPosition.magnitude;

                        if (Mathf.Abs(touchDelta) > iComfort)
                        {
                            if (touchDelta > 0)
                            {
                                SpawnOrb();
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Touch next in Input.touches)
                {
                    if (next.phase == TouchPhase.Began)
                    {
                        SpawnOrb();
                    }
                }
            }
        }
#elif UNITY_ANDROID
        MoveCharacterMobile();
        if(Input.touchCount > 0)
        {
            if(isSwipeShoot)
            {
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        prevPosition = Input.GetTouch(0).position;
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        curPosition = Input.GetTouch(0).position;

                        touchDelta = curPosition.magnitude - prevPosition.magnitude;

                        if (Mathf.Abs(touchDelta) > iComfort)
                        {
                            if (touchDelta > 0)
                            {
                                SpawnOrb();
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Touch next in Input.touches)
                {
                    if(next.phase == TouchPhase.Began)
                    {
                        SpawnOrb();
                    }
                }
            }
        }
#elif UNITY_STANDALONE
        MoveCharacter();
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnOrb();
        }
#endif
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Time.timeScale = 0f;
            Destroy(gameObject);
            deathPanel.SetActive(true);
        }
    }

    void SpawnOrb()
    {
        GameObject tempOrb = Instantiate(orbPrefab, transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D tempOrbRigidbody = tempOrb.GetComponent<Rigidbody2D>();
        if (right)
            tempOrbRigidbody.velocity = new Vector2(orbSpeed, 0f);
        if (left)
            tempOrbRigidbody.velocity = new Vector2(-orbSpeed, 0f);
        if (up)
            tempOrbRigidbody.velocity = new Vector2(0f, orbSpeed);
        if (down)
            tempOrbRigidbody.velocity = new Vector2(0f, -orbSpeed);
    }

    void MoveCharacterMobile()
    {
        // Swipe Move
        if (isSwipeMove)
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    prevPosition = Input.GetTouch(0).position;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    curPosition = Input.GetTouch(0).position;

                    touchDelta = curPosition.magnitude - prevPosition.magnitude;

                    if (Mathf.Abs(touchDelta) > iComfort)
                    {
                        if (touchDelta > 0)
                        {
                            if (Mathf.Abs(curPosition.x - prevPosition.x) > Mathf.Abs(curPosition.y - prevPosition.y))
                            {
                                // Right
                                animator.SetBool("left", false);
                                animator.SetBool("right", true);
                                animator.SetBool("up", false);
                                animator.SetBool("down", false);

                                down = left = up = false;
                                right = true;

                                transform.Translate(Vector3.right * speed * Time.deltaTime);
                            }
                            else
                            {
                                // Top
                                animator.SetBool("left", false);
                                animator.SetBool("right", false);
                                animator.SetBool("up", true);
                                animator.SetBool("down", false);

                                down = left = right = false;
                                up = true;

                                transform.Translate(Vector3.up * speed * Time.deltaTime);
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(curPosition.x - prevPosition.x) > Mathf.Abs(curPosition.y - prevPosition.y))
                            {
                                // Left
                                animator.SetBool("left", true);
                                animator.SetBool("right", false);
                                animator.SetBool("up", false);
                                animator.SetBool("down", false);

                                down = right = up = false;
                                left = true;

                                transform.Translate(Vector3.left * speed * Time.deltaTime);
                            }
                            else
                            {
                                // Down
                                animator.SetBool("left", false);
                                animator.SetBool("right", false);
                                animator.SetBool("up", false);
                                animator.SetBool("down", true);

                                right = left = up = false;
                                down = true;

                                transform.Translate(Vector3.down * speed * Time.deltaTime);
                            }
                        }
                    }
                }
            }
        }
        // Tilt Move
        else
        {

        }
    }
	void MoveCharacter()
	{
        float horizValue = Input.GetAxis("Horizontal");
        float vertValue = Input.GetAxis("Vertical");

        // Character Moves Right
		if (horizValue > 0)
		{
			animator.SetBool ("left", false);
			animator.SetBool ("right", true);
			animator.SetBool ("up", false);
			animator.SetBool ("down", false);

			down = left = up = false;
			right = true;

			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
        // Character Moves Left
        else if (horizValue < 0)
		{
			animator.SetBool ("left", true);
			animator.SetBool ("right", false);
			animator.SetBool ("up", false);
			animator.SetBool ("down", false);

			down = right = up = false;
			left = true;

			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}
        // Character Move Up
        if (vertValue > 0)
		{
			animator.SetBool ("left", false);
			animator.SetBool ("right", false);
			animator.SetBool ("up", true);
			animator.SetBool ("down", false);

			down = left = right = false;
			up = true;

			transform.Translate (Vector3.up * speed * Time.deltaTime);
		}
        // Character Moves Down
        else if (vertValue < 0)
		{
			animator.SetBool ("left", false);
			animator.SetBool ("right", false);
			animator.SetBool ("up", false);
			animator.SetBool ("down", true);

			right = left = up = false;
			down = true;

			transform.Translate (Vector3.down * speed * Time.deltaTime);
		}

		if(left)
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		else if(right)
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		if(up)
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		else if(down)
			transform.Translate (Vector3.down * speed * Time.deltaTime);
	}
}

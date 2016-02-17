using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	public float speed = 2.0f;
	public bool right, left, up, down;
	public Animator animator;
	public float orbSpeed = 20f;
	GameObject orbPrefab;


	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		right = left = up = down = false;
		orbPrefab = Resources.Load ("Orb") as GameObject;
	}

	void Update()
	{
        MoveCharacter();

#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnOrb();
        }
        else if(Input.touchCount > 0)
        {
            foreach (Touch next in Input.touches)
            {
                if(next.phase == TouchPhase.Began)
                {
                    SpawnOrb();
                }
            }
        }
#elif UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            foreach (Touch next in Input.touches)
            {
                if(next.phase == TouchPhase.Began)
                {
                    SpawnOrb();
                }
            }
        }
#elif UNITY_STANDALONE
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnOrb();
        }
#endif
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Enemy(Clone)")
        {
            Time.timeScale = 0f;
            Destroy(gameObject);
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

	void MoveCharacter()
	{
		if (Input.GetKey (KeyCode.D))
		{
			animator.SetBool ("left", false);
			animator.SetBool ("right", true);
			animator.SetBool ("up", false);
			animator.SetBool ("down", false);

			down = left = up = false;
			right = true;

			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.A))
		{
			animator.SetBool ("left", true);
			animator.SetBool ("right", false);
			animator.SetBool ("up", false);
			animator.SetBool ("down", false);

			down = right = up = false;
			left = true;

			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.W))
		{
			animator.SetBool ("left", false);
			animator.SetBool ("right", false);
			animator.SetBool ("up", true);
			animator.SetBool ("down", false);

			down = left = right = false;
			up = true;

			transform.Translate (Vector3.up * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.S))
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
		if(right)
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		if(up)
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		if(down)
			transform.Translate (Vector3.down * speed * Time.deltaTime);
	
	}
}

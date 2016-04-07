using UnityEngine;
using System.Collections;

public class EnemyOneController : MonoBehaviour {

	public static float speed = 0.05f;
	public bool right, left, up, down;
	public Animator animator;

	Transform heroPosition;
    private EnemySpawner spawnerScript;
    private GameController controlScript;

    private AudioSource enemyDeathSound;
    private bool scoreCounts;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		right = left = up = down = false;
		heroPosition = GameObject.Find ("Hero").transform;
	    spawnerScript = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
	    controlScript = GameObject.Find("GameController").GetComponent<GameController>();
	    scoreCounts = !controlScript.isEndless;

        enemyDeathSound = Camera.main.GetComponent<AudioSource>();
        InvokeRepeating ("Accelerate", 0.1f, 0.1f);
	}

	void FixedUpdate()
	{
		MoveCharacter();
	}

	void MoveCharacter()
	{
		if (transform.position.y > heroPosition.position.y)
		{
			animator.SetBool ("Left", false);
			animator.SetBool ("Right", false);
			animator.SetBool ("Up", false);
			animator.SetBool ("Down", true);

			left = right = up = false;
			down = true;
		}
		else
		{
			animator.SetBool ("Left", false);
			animator.SetBool ("Right", false);
			animator.SetBool ("Up", true);
			animator.SetBool ("Down", false);

			left = right = down = false;
			up = true;
		}

		if(up)
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		if(down)
			transform.Translate (Vector3.down * speed * Time.deltaTime);

		if (transform.position.x < heroPosition.position.x)
		{
			animator.SetBool ("Left", false);
			animator.SetBool ("Right", true);
			animator.SetBool ("Up", false);
			animator.SetBool ("Down", false);

			left = up = down = false;
			right = true;
		}
		else
		{
			animator.SetBool ("Left", true);
			animator.SetBool ("Right", false);
			animator.SetBool ("Up", false);
			animator.SetBool ("Down", false);

			right = up = down = false;
			left = true;
		}

		if(left)
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		if (right)
			transform.Translate (Vector3.right * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Bullet")
		{
		    enemyDeathSound.Play();
            spawnerScript.IncrementNumOneKilled();
		    if (scoreCounts)
		        controlScript.AddScore(3f);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}

	public void Accelerate()
	{
		speed += .000001f;
	}
}

using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed = 2.0f;
	public bool right, left, up, down;
	public Animator animator;

	Transform heroPosition;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		right = left = up = down = false;
		heroPosition = GameObject.Find ("Hero").transform;
		InvokeRepeating ("Accelerate", 2f, 5f);
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

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "Orb(Clone)")
		{
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}

	void Accelerate()
	{
		speed += 1f;
	}
}

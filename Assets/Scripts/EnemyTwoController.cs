using UnityEngine;
using System.Collections;

public class EnemyTwoController : MonoBehaviour
{

    public static float speedTwo = 0.05f;
    public bool right, left, up, down;
    private int numLives;
    Transform heroPosition;
    private EnemySpawner spawnerScript;
    // Use this for initialization
    void Start()
    {
        right = left = up = down = false;
        heroPosition = GameObject.Find("Hero").transform;
        spawnerScript = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        numLives = Random.Range(1, 4);
        InvokeRepeating("Accelerate", 0.1f, 0.1f);
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (transform.position.y > heroPosition.position.y)
        {
            left = right = up = false;
            down = true;
        }
        else
        {
            left = right = down = false;
            up = true;
        }

        if (up)
            transform.Translate(Vector3.up * speedTwo * Time.deltaTime);
        if (down)
            transform.Translate(Vector3.down * speedTwo * Time.deltaTime);

        if (transform.position.x < heroPosition.position.x)
        {
            left = up = down = false;
            right = true;
        }
        else
        {
            right = up = down = false;
            left = true;
        }

        if (left)
            transform.Translate(Vector3.left * speedTwo * Time.deltaTime);
        if (right)
            transform.Translate(Vector3.right * speedTwo * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            numLives--;
            if (CheckLives())
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }

    bool CheckLives()
    {
        if (numLives <= 0)
            return true;

        return false;
    }

    public void Accelerate()
    {
        speedTwo += .0000000001f;
    }
}

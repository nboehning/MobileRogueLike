using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Vector2 minValues;
    public Vector2 maxValues;
    private GameObject hero;

    void Start()
    {
        hero = GameObject.Find("Hero");
    }

    void Update()
    {
        transform.position = new Vector3(hero.transform.position.x, hero.transform.position.y, -1);

        if (transform.position.x < minValues.x)
            transform.position = new Vector3(minValues.x, transform.position.y, transform.position.z);
        else if(transform.position.x > maxValues.x)
            transform.position = new Vector3(maxValues.x, transform.position.y, transform.position.z);

        if (transform.position.y < minValues.y)
            transform.position = new Vector3(transform.position.x, minValues.y, transform.position.z);
        else if(transform.position.y > maxValues.y)
            transform.position = new Vector3(transform.position.x, maxValues.y, transform.position.z);

    }
}

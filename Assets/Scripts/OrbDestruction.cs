using UnityEngine;
using System.Collections;

public class OrbDestruction : MonoBehaviour {

    public float lifeTime = 5f;

	// Use this for initialization
	void Start ()
    {
        Invoke("DestroyOrb", lifeTime);
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == "Obstacles")
        {
            Destroy(this.gameObject);
        }
    }

	void DestroyOrb()
    {
        Destroy(this.gameObject);
    }
}

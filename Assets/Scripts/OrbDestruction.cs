using UnityEngine;
using System.Collections;

public class OrbDestruction : MonoBehaviour {

    public float lifeTime = 5f;

	// Use this for initialization
	void Start ()
    {
        Invoke("DestroyOrb", lifeTime);
    }
	
	void DestroyOrb()
    {
        Destroy(this.gameObject);
    }
}

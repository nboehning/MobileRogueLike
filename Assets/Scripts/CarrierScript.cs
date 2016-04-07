using UnityEngine;
using System.Collections;

public class CarrierScript : MonoBehaviour
{
    public bool hasWon;
    public float score;
    public int level;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}

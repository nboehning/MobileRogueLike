using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    Text scoreBox;
    float score;

	// Use this for initialization
	void Start ()
    {
        scoreBox = GameObject.Find("TextScore").GetComponent<Text>();
        InvokeRepeating("AddTime", 0.5f, 0.5f);
	}
	
	void AddTime()
    {
        score += 0.5f;

        scoreBox.text = "Score: " + System.Math.Round(score, 1);
    }
}

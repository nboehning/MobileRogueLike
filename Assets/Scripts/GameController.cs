using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public bool isEndless;
    Text scoreBox;
    float score;

	// Use this for initialization
	void Start ()
    {
        scoreBox = GameObject.Find("TextScore").GetComponent<Text>();
        if(isEndless)
            InvokeRepeating("AddTime", 0.5f, 0.5f);

	}
	
	public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;

        scoreBox.text = "Score: " + System.Math.Round(score, 1);
    }

    public float GetScore()
    {
        return score;
    }
}

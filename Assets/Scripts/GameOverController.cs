using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{

    public Text displayText;
    public Text levelText;
    public Text scoreText;
    private GameObject winCarrier;

    void Start()
    {
        winCarrier = GameObject.Find("ResultObject");
        if (winCarrier.GetComponent<CarrierScript>().hasWon)
        {
            displayText.text = "You Win!";
            levelText.text = "Levels Clear: " + winCarrier.GetComponent<CarrierScript>().level;
            scoreText.text = "Score: " + winCarrier.GetComponent<CarrierScript>().score;
        }
        else
        {
            displayText.text = "You Lose!";
            levelText.text = "Levels Clear: " + winCarrier.GetComponent<CarrierScript>().level;
            scoreText.text = "Score: " + winCarrier.GetComponent<CarrierScript>().score;
        }
    }

    public void BtnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BtnStartOver()
    {
        Destroy(winCarrier);
        SceneManager.LoadScene("LoadMapScene");
    }
}

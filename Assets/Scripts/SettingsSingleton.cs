using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum gameDifficulty
{
    EASY,
    MEDIUM,
    HARD
};

public class SettingsSingleton : MonoBehaviour {
    // Variables to carry over
    int volume = 50;
    bool isSwipeShoot = true;
    bool isSwipeMove = true;
    gameDifficulty curDifficulty;

    // Gameobjects that set settings
    GameObject volumeSlider;
    GameObject shootTapToggle;
    GameObject shootSwipeToggle;
    GameObject moveSwipeToggle;
    GameObject moveTiltToggle;
    GameObject easyToggle;
    GameObject medToggle;
    GameObject hardToggle;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            // Main menu scene is loaded
            // Find all the settings setters
            volumeSlider = GameObject.Find("VolumeSlider");
            shootSwipeToggle = GameObject.Find("ShootSwipeToggle");
            moveSwipeToggle = GameObject.Find("MoveSwipeToggle");
            easyToggle = GameObject.Find("EasyDiffToggle");
            medToggle = GameObject.Find("MediumDiffToggle");
            hardToggle = GameObject.Find("HardDiffToggle");
        }
        
    }
    
    public void SetSettings()
    {
        if(easyToggle.GetComponent<Toggle>().isOn)
        {
            curDifficulty = gameDifficulty.EASY;
        }
        else if(medToggle.GetComponent<Toggle>().isOn)
        {
            curDifficulty = gameDifficulty.MEDIUM;
        }
        else if(hardToggle.GetComponent<Toggle>().isOn)
        {
            curDifficulty = gameDifficulty.HARD;
        }
        isSwipeMove = moveSwipeToggle.GetComponent<Toggle>().isOn;
        isSwipeShoot = shootSwipeToggle.GetComponent<Toggle>().isOn;
    }

    public void UpdateVolume()
    {
        volume = (int)volumeSlider.GetComponent<Slider>().value;
    }
}

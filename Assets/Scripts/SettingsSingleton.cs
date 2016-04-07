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
    bool isSwipeShoot = true;
    bool isSwipeMove = true;
    gameDifficulty curDifficulty;
    [HideInInspector]
    public bool isEndless;

    // Gameobjects that set settings
    GameObject volumeSlider;
    GameObject shootSwipeToggle;
    GameObject moveSwipeToggle;
    GameObject easyToggle;
    GameObject medToggle;
    GameObject hardToggle;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetMenuThings()
    {
        // Main menu scene is loaded
        // Find all the settings setters
        volumeSlider = GameObject.Find("VolumeSlider");
        shootSwipeToggle = GameObject.Find("ShootSwipeToggle");
        moveSwipeToggle = GameObject.Find("MoveSwipeToggle");
        easyToggle = GameObject.Find("EasyDiffToggle");
        medToggle = GameObject.Find("MediumDiffToggle");
        hardToggle = GameObject.Find("HardDiffToggle");
        UpdateVolume();
    }

    public void SetGameThings()
    {
        transform.position = Camera.main.transform.position;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<HeroController>().isSwipeMove = isSwipeMove;
        player.GetComponent<HeroController>().isSwipeShoot = isSwipeShoot;
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().curDifficulty = curDifficulty;
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().difficultyMultiplier = isEndless ? 4 : 2;
        GameObject.Find("GameController").GetComponent<GameController>().isEndless = isEndless;
        Debug.Log("Settings is endless: " + isEndless);
    }

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Settings").Length > 1)
            Destroy(gameObject);
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
        GetComponent<AudioSource>().volume = volumeSlider.GetComponent<Slider>().value / 100;
    }
}

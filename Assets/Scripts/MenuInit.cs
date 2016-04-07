using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuInit : MonoBehaviour {

	void Awake()
	{
        if(SceneManager.GetActiveScene().name == "MainMenu")
		    GameObject.Find("GameValueSingleton").GetComponent<SettingsSingleton>().SetMenuThings();
        else if (SceneManager.GetActiveScene().name == "GameScene")
            GameObject.Find("GameValueSingleton").GetComponent<SettingsSingleton>().SetGameThings();
	}
}

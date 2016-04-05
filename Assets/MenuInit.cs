using UnityEngine;
using System.Collections;

public class MenuInit : MonoBehaviour {

	void Awake()
	{
		GameObject.Find("GameValueSingleton").GetComponent<SettingsSingleton>().SetThings();
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

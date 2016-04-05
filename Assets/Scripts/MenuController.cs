using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    // Variables for scene management
    public GameObject[] sceneOrganizers;
    int curMenu = 0;

    // Variables for swiping
    Vector2 prevPosition;
    Vector2 curPosition;
    float touchDelta;
    int iComfort = 50;

	// Use this for initialization
	void Start ()
	{
	    sceneOrganizers[0].SetActive(true);
        sceneOrganizers[1].SetActive(false);
        sceneOrganizers[2].SetActive(false);
        sceneOrganizers[3].SetActive(false);
        sceneOrganizers[4].SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                prevPosition = Input.GetTouch(0).position;
            }
            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                curPosition = Input.GetTouch(0).position;

                touchDelta = curPosition.magnitude - prevPosition.magnitude;

                if(Mathf.Abs(touchDelta) > iComfort)
                {
                    if(touchDelta > 0)
                    {
                        if(Mathf.Abs(curPosition.x - prevPosition.x) > Mathf.Abs(curPosition.y - prevPosition.y))
                        {
                            // Right
                            // Set current menu to false
                            sceneOrganizers[curMenu].SetActive(false);

                            // Move to menu to the left of current menu
                            if (curMenu == 0)
                            {
                                curMenu = sceneOrganizers.Length - 1;
                            }
                            else
                            {
                                if (curMenu == 2)
                                {
                                    GameObject.Find("GameValueSingleton").GetComponent<SettingsSingleton>().SetSettings();
                                }
                                curMenu--;
                            }
                            // Set new menu to true
                            sceneOrganizers[curMenu].SetActive(true);
                        }
                        else
                        {
                            // Top
                        }
                    }
                    else
                    {
                        if(Mathf.Abs(curPosition.x - prevPosition.x) > Mathf.Abs(curPosition.y - prevPosition.y))
                        {
                            // Left
                            // Set current menu to false
                            sceneOrganizers[curMenu].SetActive(false);

                            // Move to menu to the right of current menu
                            if (curMenu == sceneOrganizers.Length - 1)
                            {
                                curMenu = 0;
                            }
                            else
                            {
                                if (curMenu == 2)
                                {
                                    GameObject.Find("GameValueSingleton").GetComponent<SettingsSingleton>().SetSettings();
                                }
                                curMenu++;
                            }
                            // Set new menu to true
                            sceneOrganizers[curMenu].SetActive(true);
                            
                        }
                        else
                        {
                            // Down
                        }
                    }
                }
            }

        }
	}

    public void BtnPlayEndless()
    {
        //SceneManager.LoadScene();
    }

    public void BtnPlayProgression()
    {
        //SceneManager.LoadScene();
    }

    public void BtnExitGame()
    {

    }
}

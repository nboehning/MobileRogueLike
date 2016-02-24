using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	public void BtnRespawn()
    {
        SceneManager.LoadScene("99_TestScene");
    }

    public void BtnExitGame()
    {
        Application.Quit();
    }
}

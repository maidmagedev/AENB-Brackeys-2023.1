using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    public void StartButtonOnClick()
    {
        SceneManager.LoadScene(0);
    }

    public void TutorialButtonOnClick()
    {
        SceneManager.LoadScene(1);
    }
}

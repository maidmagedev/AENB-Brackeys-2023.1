using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called by UIManager.cs to toggle the Pause Menu container gameobject as active or inactive.
    public void TogglePauseMenuView() {
        pauseUI.SetActive(GetMenuVisibility());
    }

    bool GetMenuVisibility() {
        return pauseUI.activeInHierarchy;
    }
}

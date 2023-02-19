using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public MenuState menuState = MenuState.alloff;
    [SerializeField] GameObject pauseParent;
    [SerializeField] GameObject pauseMain;
    [SerializeField] GameObject pauseSettings;
    
    public enum MenuState {
        primary,
        settings,
        alloff
    }

    // Start is called before the first frame update
    void Start()
    {
        // Ensure default UI states for game start
        menuState = MenuState.alloff;
        pauseParent.SetActive(false);
        pauseMain.SetActive(false);
        pauseSettings.SetActive(false);
    }

    // Called by UIManager.cs to toggle various Pause Menu container gameobjects as active or inactive.
    public void CyclePauseMenuView() {
        switch(menuState) {
            case MenuState.alloff:
                // Changing to Primary State
                menuState = MenuState.primary;
                pauseParent.SetActive(true);
                pauseMain.SetActive(true);
                Time.timeScale = 0;
                break;
            case MenuState.primary:
                // Changing to alloff state.
                menuState = MenuState.alloff;
                pauseParent.SetActive(false);
                pauseMain.SetActive(false);
                Time.timeScale = 1;
                break;
            case MenuState.settings:
                // Changing to Primary State.
                menuState = MenuState.primary;
                pauseSettings.SetActive(false);
                pauseMain.SetActive(true);
                break;
        }
    }

    // UI method. Called by the Settings Button in the pauseMain gameobject and the Back button in the pauseSettings obj.
    public void ToggleSettingsView() {
        if (menuState != MenuState.settings) {
            menuState = MenuState.settings;
            pauseSettings.SetActive(true);
            pauseMain.SetActive(false);
        } else {
            CyclePauseMenuView(); // should go back to MenuState.primary.
        }
    }



   
}

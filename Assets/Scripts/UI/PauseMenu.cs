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
        pauseMain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called by UIManager.cs to toggle various Pause Menu container gameobjects as active or inactive.
    public void CyclePauseMenuView() {
        switch(menuState) {
            case MenuState.alloff:
                // Changing to Primary State
                menuState = MenuState.primary;
                pauseParent.SetActive(true);
                pauseMain.SetActive(true);
                break;
            case MenuState.primary:
                // Changing to alloff state.
                menuState = MenuState.alloff;
                pauseParent.SetActive(false);
                pauseMain.SetActive(false);
                break;
            case MenuState.settings:
                // Changing to Primary State.
                menuState = MenuState.primary;
                pauseSettings.SetActive(false);
                pauseMain.SetActive(true);
                break;
        }
    }

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

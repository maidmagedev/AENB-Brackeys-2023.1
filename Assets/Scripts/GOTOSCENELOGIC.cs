using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GOTOSCENELOGIC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // THIS IS USED BY ANIMATION EVENTS, WHICH DOES NOT COUNT AS A CALL REFERENCE.
    void mainMenu () {
        SceneManager.LoadScene(0);
    }
}

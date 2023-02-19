using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting_Guide : MonoBehaviour
{
    [SerializeField] private GameObject craftPanel;
    // Start is called before the first frame update
    void Start()
    {
        craftPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void togglePanel()
    {
        craftPanel.SetActive(!craftPanel.activeSelf);
        Time.timeScale = (craftPanel.activeSelf ?  0 : 1);
    }
}

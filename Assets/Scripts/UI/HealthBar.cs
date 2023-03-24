using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthbar;
    [SerializeField] GameObject target;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = target.GetComponent<DamageableComponent>().GetHealthPercentage();
    }
}

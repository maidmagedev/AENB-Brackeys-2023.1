using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ResourceDeposit : MonoBehaviour, IKillable
{
    [SerializeField] private GameObject pickUp;
    private Light2D dmgLight;
    
    // Start is called before the first frame update
    void Start()
    {
        dmgLight = GetComponent<Light2D>();
        dmgLight.enabled = false;
    }
    
    public void Die()
    {
        Instantiate(pickUp, this.transform.position, Quaternion.identity);
        print(pickUp.name);
        Destroy(this.gameObject);
    }

    public void NotifyDamage()
    {
        StartCoroutine(DamageLightToggle());
    }
    
    IEnumerator DamageLightToggle()
    {
        dmgLight.enabled = true;
        yield return new WaitForSeconds(.5f);
        dmgLight.enabled = false;
    }
}

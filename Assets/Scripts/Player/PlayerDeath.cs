using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour, IKillable
{
    private bool PlayerDead = false;

    DamageableComponent damageComp;
    private Light2D dmgLight;

    [Header("External Death Updates")]
    [SerializeField] GameObject deathScreen; // Should be located at "UI ELEMENTS..."/"PRIMARY UI CANVAS"/"Death Screen Holder"
    Image deathOverlay;
    Color originalDeathColor;


    // Start is called before the first frame update
    void Start()
    {
        damageComp = GetComponent<DamageableComponent>();

        dmgLight = GetComponent<Light2D>();
        dmgLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            Die();
        }
    }

    public void Die()
    {
        PlayerDead = true;

        // Death Screen Logic
        deathScreen.SetActive(true);
        deathOverlay = deathScreen.transform.GetChild(0).GetComponent<Image>(); // other children will have image components too. Make sure the panel is child 0.
        originalDeathColor = deathOverlay.color;
        deathOverlay.color = new Color (deathOverlay.color.r, deathOverlay.color.g, deathOverlay.color.b, 0);

        StartCoroutine(OverlayFade());
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

    private IEnumerator OverlayFade() {
        float t = 0;
        while (t < 1) {
            deathOverlay.color = Color.Lerp(deathOverlay.color, originalDeathColor, t);
            t = t + Time.deltaTime / 2f;
            yield return new WaitForEndOfFrame();
        }
    }
}

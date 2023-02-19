using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class PlayerDeath : MonoBehaviour, IKillable
{
    private bool PlayerDead = false;

    DamageableComponent damageComp;
    private Light2D dmgLight;

    [Header("External Death Updates")]
    [SerializeField] GameObject deathScreen; // Should be located at "UI ELEMENTS..."/"PRIMARY UI CANVAS"/"Death Screen Holder"
    Image deathOverlay;
    Color originalDeathColor;

    private float deathTimer = 6f;


    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageComp = GetComponent<DamageableComponent>();

        dmgLight = GetComponent<Light2D>();
        dmgLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // press "o" to die
        if (Input.GetKeyDown(KeyCode.O)) {
            Die();
        }

        if (PlayerDead)
        {
            if (deathTimer > 1)
            {
                deathTimer -= Time.deltaTime;
                deathScreen.GetComponentInChildren<TextMeshProUGUI>().text = ("You are dead\n" + (int)deathTimer);
            }
            else
            {
                PlayerDead = false;
                deathTimer = 6;
                deathScreen.SetActive(false);
                Respawn();
                GetComponent<TopDownMovementComponent>().enabled = true;
                GetComponent<DamageableComponent>().SetMaxHealth(100);
                GetComponent<DamageableComponent>().SetDeathBool(false);
            }
        }
    }

    public void Die()
    {
        rb.velocity = Vector2.zero;
        PlayerDead = true;
        GetComponent<TopDownMovementComponent>().enabled = false;
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
    
    // On death: Display countdown timer for 5 seconds and respawn player at 0, 0, 0
    private void Respawn()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    public bool getPlayerDeath()
    {
        return PlayerDead;
    }
}

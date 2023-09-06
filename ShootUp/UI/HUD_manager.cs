using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD_manager : MonoBehaviour
{
    public TMP_Text health;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.text = PlayerControl.currentHealth.ToString();
        healthBar.fillAmount = (float)PlayerControl.currentHealth / (float)PlayerControl.maxHealth;
    }
}

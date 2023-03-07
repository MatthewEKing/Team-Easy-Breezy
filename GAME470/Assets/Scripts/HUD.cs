using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    [SerializeField] Text scrapText;
    [SerializeField] Text selectedTurretText;

    [SerializeField] Slider baseHealthSlider;
    [SerializeField] Gradient baseHealthGradient;
    [SerializeField] Text baseHealthText;
    

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        baseHealthSlider.maxValue = Base.instance.maxHealth;
    }

    void FixedUpdate()
    {
        UpdateHUD();
    }

    void UpdateHUD()
    {
        if (TurretSelect.instance.selectedTurret == null) { selectedTurretText.text = "Turret: None"; }
        else { selectedTurretText.text = "Turret: " + TurretSelect.instance.selectedTurret.name; }
        
        scrapText.text = "Scrap: " + GameManager.instance.totalScrap;
        baseHealthText.text = "Base Health: " + Base.instance.health + "/" + Base.instance.maxHealth;

        baseHealthSlider.GetComponentInChildren<Image>().color = baseHealthGradient.Evaluate(baseHealthSlider.normalizedValue);
        baseHealthSlider.value = Base.instance.health;
    }
}

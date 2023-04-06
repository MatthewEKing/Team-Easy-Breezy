using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    [SerializeField] Text scrapText;

    [Header("Select Turret Objs")]
    [SerializeField] Image selectObj;
    [SerializeField] Image basicTurretImage, flameTurretImage, teslaCoilImage;
    //[SerializeField] Text selectedTurretText;

    [Header("Base")]
    [SerializeField] Slider baseHealthSlider;
    [SerializeField] Gradient baseHealthGradient;
    [SerializeField] Text baseHealthText;

    [Header("Pause Screen")]
    [SerializeField] GameObject pauseOverlay;
    [SerializeField] Text currentWaveText;

    [Header("Phases")]
    [SerializeField] Text phaseText;
    public Animator phaseTextAnim;
    

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        phaseTextAnim = GetComponent<Animator>();
        baseHealthSlider.maxValue = Base.instance.maxHealth;
    }

    void FixedUpdate()
    {
        UpdateHUD();
    }

    void UpdateHUD()
    {
        //if (TurretSelect.instance.selectedTurret == null) { selectedTurretText.text = "Turret: None"; }
        //else { selectedTurretText.text = "Turret: " + TurretSelect.instance.selectedTurret.name; }
        
        scrapText.text = "Scrap: " + GameManager.instance.totalScrap;
        baseHealthText.text = "Base Health: " + Base.instance.health + "/" + Base.instance.maxHealth;

        baseHealthSlider.GetComponentInChildren<Image>().color = baseHealthGradient.Evaluate(baseHealthSlider.normalizedValue);
        baseHealthSlider.value = Base.instance.health;

        currentWaveText.text = "Wave " + GameManager.instance.currentWave;
    }

    public void PauseUnpause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Pressed Esc");
            if (pauseOverlay.activeInHierarchy)
            {
                pauseOverlay.SetActive(false);
                Time.timeScale = 1f;
                PlayerController.instance.canMove = false;
            }
            else
            {
                pauseOverlay.SetActive(true);
                Time.timeScale = 0f;
                PlayerController.instance.canMove = true;
            }
        }
    }

    public void ChangeSelectIcon(int type)
    {
        if (type == 0)
        {
            selectObj.transform.position = basicTurretImage.transform.position;
        }
        else if (type == 1)
        {
            selectObj.transform.position = flameTurretImage.transform.position;
        }
        else if (type == 2)
        {
            selectObj.transform.position = teslaCoilImage.transform.position;
        }
    }

    public void PlayPhaseTextAnimation(bool isInBuildPhase)
    {
        if (isInBuildPhase)
        {
            phaseTextAnim.SetTrigger("playPhaseTextAnim");
            phaseText.text = "BUILDING PHASE";
        }
        else
        {
            phaseTextAnim.SetTrigger("playPhaseTextAnim");
            phaseText.text = "WAVE " + GameManager.instance.currentWave;
        }
    }
}

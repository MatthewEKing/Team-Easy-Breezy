using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretSelect : MonoBehaviour
{
    public static TurretSelect instance;

    public TurretType[] turrets;
    public TurretType selectedTurret;

    private void Awake()
    {
        instance = this;
    }

    public void OnSwitchTurretLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwitchTurretLeft();
        }
    }

    public void OnSwitchTurretRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwitchTurretRight();
        }
    }

    void SwitchTurretLeft()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (selectedTurret == null)
            {
                selectedTurret = turrets[0];
                HUD.instance.ChangeSelectIcon(0);
            }
            else if (selectedTurret == turrets[0])
            {
                Debug.Log("End Of Array");
            }
            else if (selectedTurret == turrets[i])
            {
                selectedTurret = turrets[i - 1];
                HUD.instance.ChangeSelectIcon(i - 1);
                Debug.Log(selectedTurret);
                return;
            }
        }
        
    }

    void SwitchTurretRight()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            if (selectedTurret == null)
            {
                selectedTurret = turrets[turrets.Length - 1];
                HUD.instance.ChangeSelectIcon(2);
            }
            else if (selectedTurret == turrets[turrets.Length - 1])
            {
                Debug.Log("End Of Array");
            }
            else if (selectedTurret == turrets[i])
            {
                selectedTurret = turrets[i + 1];
                HUD.instance.ChangeSelectIcon(i + 1);
                Debug.Log(selectedTurret);
                return;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int totalScrap = 0;


    private void Awake()
    {
        instance = this;
    }

    public void AddScrap()
    {
        totalScrap++;
    }

    public void RemoveScrap(int amount)
    {
        totalScrap -= amount;
    }
}

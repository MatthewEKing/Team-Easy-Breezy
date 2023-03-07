using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Turret", fileName = "New Turret")]
public class TurretType : ScriptableObject
{
    public string name;
    public float fireRate;
    public int damage;
    public int scrapCost;
    public GameObject prefab;
}

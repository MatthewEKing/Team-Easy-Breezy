using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPickup : MonoBehaviour, ICollectable
{

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Time.deltaTime * Vector3.up * 100, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        if (other.tag == "Player")
        {
            Collect();
        }
    }

    public void Collect()
    {
        GameManager.instance.AddScrap();
        Debug.Log("Collected Scrap");
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    public static Base instance;

    public int maxHealth = 20;
    public int health;



    private void Awake()
    {
        instance = this;
        health = maxHealth;
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided");
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            TakeDamage(1);
            other.gameObject.GetComponent<Enemy>().Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    [SerializeField] int health;
    [SerializeField] int speed;



    NavMeshAgent agent;
    //public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(FindObjectOfType<Base>().gameObject.transform.position);
    }


    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(this.gameObject + " took " + damage + " damage.");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    [SerializeField] int health;
    [SerializeField] float speed;
    [SerializeField] float slowedSpeed;


    NavMeshAgent agent;
    //public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(FindObjectOfType<Base>().gameObject.transform.position);

        slowedSpeed = speed / 2;
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
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

    public void SlowEnemy()
    {
        speed = slowedSpeed;
    }

    public void Die()
    {
        GameManager.instance.AddScrap(2);
        Destroy(this.gameObject);
    }
}

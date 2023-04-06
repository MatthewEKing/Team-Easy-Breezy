using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("Turret Variables")]
    [SerializeField] TurretType turret;

    public List<Enemy> enemiesInRange;
    public Enemy currentTarget;
    [SerializeField] bool isShooting;
    TextMesh turretText;

    [SerializeField] bool canRotate = true;

    void Start()
    {
        turretText = GetComponentInChildren<TextMesh>();
        turretText.text = turret.name;
    }

    void Update()
    {
        if (currentTarget != null)
        {
            if (canRotate)
            {
                transform.rotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position, Vector3.up);
            }

            if (!isShooting)
            {
                StartCoroutine(ShootTarget());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            enemiesInRange.Add(other.GetComponent<Enemy>());
            if (currentTarget == null)
            {
                currentTarget = other.GetComponent<Enemy>();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            if (other.GetComponent<Enemy>() == currentTarget)
            {
                enemiesInRange.Remove(other.GetComponent<Enemy>());
                if (enemiesInRange.Count != 0)
                {
                    currentTarget = enemiesInRange[0];
                }
                else
                {
                    currentTarget = null;
                }
            }
            else
            {
                enemiesInRange.Remove(other.GetComponent<Enemy>());
            }
        }
    }

    IEnumerator ShootTarget()
    {
        isShooting = true;
        yield return new WaitForSeconds(turret.fireRate);
        Debug.Log("Turret Fired");

        if (currentTarget != null)
        {
            if (!turret.canHitMultipleTargets)
            {
                currentTarget.TakeDamage(turret.damage);

                if (turret.slowsEnemies)
                {
                    currentTarget.SlowEnemy();
                }
            }
            else
            {
                foreach (Enemy enemy in enemiesInRange)
                {
                    if (enemy != null)
                    {
                        enemy.TakeDamage(turret.damage);

                        if (turret.slowsEnemies)
                        {
                            enemy.SlowEnemy();
                        }
                    }
                }

                /*for (int i = 0; i < 3; i++)
                {
                    if (enemiesInRange.Count > 2)
                    {
                        enemiesInRange[i].TakeDamage(turret.damage);

                        if (turret.slowsEnemies)
                        {
                            enemiesInRange[i].SlowEnemy();
                        }
                    }
                    else
                    {
                        currentTarget.TakeDamage(turret.damage);

                        if (turret.slowsEnemies)
                        {
                            currentTarget.SlowEnemy();
                        }
                    }
                }*/
            }
        }

        isShooting = false;
    }
}

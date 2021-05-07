using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{
    void Start()
    {
        fEnemyHealth = 200;
        fEnemyMaxHealth = 200;
        fPatrolRaycastDistance = 1f;
        fPatrolSpeedOfEnemy = 2f;
        bPatrolRight = true;
        fLoSRaycastDistance = 5f;
        tPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fEnemyHealth > 0) {
            if (LineOfSightCheck() == true) {
                animator.SetBool("AttackPlayer", true);
                //Debug.Log("Player Sighted");
                Attack();
            } else {
                animator.SetBool("AttackPlayer", false);
                Patrol();
                //Debug.Log("Patrol");
            }
        } else {
            Vector3 v3DeathPosition = transform.position;
            DropFuel(v3DeathPosition);
            Instantiate(DeathXplosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

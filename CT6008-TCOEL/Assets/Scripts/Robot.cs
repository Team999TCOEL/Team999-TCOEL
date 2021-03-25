using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{


    void Start()
    {
        iEnemyHealth = 3;
        fPatrolRaycastDistance = 1f;
        fPatrolSpeedOfEnemy = 2f;
        bPatrolRight = true;
        fLoSRaycastDistance = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (iEnemyHealth > 0) {
            if (LineOfSightCheck() == true) {
                animator.SetBool("AttackPlayer", true);
                Debug.Log("Player Sighted");
                Attack();
            } else {
                animator.SetBool("AttackPlayer", false);
                Patrol();
                Debug.Log("Patrol");
            }
        } else {
            Vector3 v3DeathPosition = transform.position;
            DropFuel(v3DeathPosition);
            this.gameObject.SetActive(false);   
        }

    }
}

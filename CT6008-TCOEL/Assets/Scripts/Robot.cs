using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{
    void Start()
    {
        fLastHealth = fEnemyHealth;
        fEnemyHealth = 200;
        fEnemyMaxHealth = 200;
        fPatrolRaycastDistance = 1f;
        fPatrolSpeedOfEnemy = 2f;
        bPatrolRight = true;
        fLoSRaycastDistance = 5f;
        tPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (fEnemyHealth < fLastHealth) {
            TakingDamageSound.Play();
            fLastHealth = fEnemyHealth;
        } else {
            fLastHealth = fEnemyHealth;
        }

        if (fEnemyHealth > 0) {
            if (LineOfSightCheck() == true && tPlayer.GetComponent<PlayerController>().bBossFightCameraActive == false) {
                animator.SetBool("AttackPlayer", true);
                //Debug.Log("Player Sighted");
                if(AttackSound.isPlaying == false) {
                    AttackSound.Play();
				}
                Attack();
            } else {
                animator.SetBool("AttackPlayer", false);
                AttackSound.Stop();
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

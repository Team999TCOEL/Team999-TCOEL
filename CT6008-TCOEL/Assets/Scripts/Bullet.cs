using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float fBulletSpeed;
    public Rigidbody2D bulletRigidBody2D;
    private GameObject go_PlayerController;
    PlayerController playerController;
    public SMG smg;

    private float fLiveTime = 1.3f;

    void Start()
    {
        smg = GameObject.Find("MP5").GetComponent<SMG>();
        go_PlayerController = GameObject.FindGameObjectWithTag("Player");
        playerController = go_PlayerController.GetComponent<PlayerController>();
        if(playerController.bFacingRight == true) {
            bulletRigidBody2D.velocity = Vector2.right * fBulletSpeed;
        } else {
            bulletRigidBody2D.velocity = Vector2.right * -fBulletSpeed;
        }     
    }

	private void Update() {
        fLiveTime -= Time.deltaTime;
        if(fLiveTime <= 0f) {
            Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.GetComponent<Enemy>().fEnemyHealth -= smg.fDamage;
            //Vector3 direction = (transform.position - collision.transform.position).normalized;
            
            //collision.GetComponent<Rigidbody2D>().AddForce(direction * 5f);

            if(collision.GetComponent<Enemy>().bPatrolRight == false) {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1.3f, collision.transform.position.y);
            } else {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1.3f, -collision.transform.position.y);
            }



        }
        if(collision.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
	}

    private void knockBack(GameObject target, Vector3 direction, float length, float overTime) {
        direction = direction.normalized;
        StartCoroutine(knockBackCoroutine(target, direction, length, overTime));
    }

    IEnumerator knockBackCoroutine(GameObject target, Vector3 direction, float length, float overTime) {
        float timeleft = overTime;
        while (timeleft > 0) {

            if (timeleft > Time.deltaTime) {
                target.transform.Translate(direction * Time.deltaTime / overTime * length);
            } else {
                target.transform.Translate(direction * timeleft / overTime * length);
            }
            timeleft -= Time.deltaTime;

            yield return null;
        }

    }
}

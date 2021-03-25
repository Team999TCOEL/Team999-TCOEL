using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleRobotAttack : MonoBehaviour
{
    public BlackBoard blackboard;
    public Transform tPlayer;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Attack Player");

            tPlayer.gameObject.GetComponent<Renderer>().material.color = Color.red;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-15f, collision.transform.position.y);
        }
    }

	private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            //blackboard.iPlayerHealth -= 1;
        }
    }
}

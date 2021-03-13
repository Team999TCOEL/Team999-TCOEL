////////////////////////////////////////////////////////////
// File:                 <TriangleEnemy.cs>
// Author:               <Morgan Ellis>
// Date Created:         <09/02/2021>
// Brief:                <File responsible for the movements of the player such as jumping>
// Last Edited By:       <Morgan Ellis>
// Last Edited Date:     <13/03/2021>
// Last Edit Brief:      <Updting enemy with sprites and linking the animator to the animations>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleRobot : MonoBehaviour
{
	private float fSpeedOfEnemy = 2f;

	private float fDistance = 1f;

	private bool bMoveRight = true;

	private bool bPlayerSpotted;

	private float fRaycastDistance = 5f;

	public Transform tGroundDetection;

	public Transform tPlayer;

	public LayerMask platformLayerMask;

	public LayerMask playerLayerMask;

	public BlackBoard blackboard;

	public Animator LittleRobotAnimator;


	void Update() {

		if (LineOfSightCheck() == true) {
			LittleRobotAnimator.SetBool("AttackPlayer", true);
			Attack();
		} else {
			LittleRobotAnimator.SetBool("AttackPlayer", false);
			Patrol();
		}
	}

	private void Patrol() {
		GetComponent<Renderer>().material.color = Color.green;
		RaycastHit2D groundInfo = Physics2D.Raycast(tGroundDetection.position, Vector2.down, fDistance, platformLayerMask);
		Color rayColor;
		if (groundInfo.collider != null) {
			rayColor = Color.green;
		} else {
			rayColor = Color.red;
		}

		transform.Translate(Vector2.left * fSpeedOfEnemy * Time.deltaTime);

		Debug.DrawRay(tGroundDetection.position, Vector2.down, rayColor);

		if (groundInfo.collider == false) {
			if (bMoveRight == true) {
				transform.eulerAngles = new Vector3(0, -180, 0);
				bMoveRight = false;
			} else {
				transform.eulerAngles = new Vector3(0, 0, 0);
				bMoveRight = true;
			}
		} 
	}

	private bool LineOfSightCheck() {
		if (transform.rotation.y == 0) {
			RaycastHit2D rayCastInfo = Physics2D.Raycast(transform.position, Vector2.left, fRaycastDistance, playerLayerMask);

			if (rayCastInfo.collider == true) {
				bPlayerSpotted = true;
			} else {
				bPlayerSpotted = false;
			}
		} else {
			RaycastHit2D rayCastInfo = Physics2D.Raycast(transform.position, Vector2.right, fRaycastDistance, playerLayerMask);
			if (rayCastInfo.collider == true) {
				bPlayerSpotted = true;
			} else {
				bPlayerSpotted = false;
			}
		}

		return bPlayerSpotted;
	}

	private void Attack() {
		float fEnemyChaseSpeed = 6f;

		Vector2 v2PlayerPosition = new Vector2(tPlayer.position.x, tPlayer.position.y);
		Vector2 v2EnemyPosition = new Vector2(transform.position.x, transform.position.y);

		//transform.Translate((new Vector2(tPlayer.position.x, tPlayer.position.y)) * fEnemyChaseSpeed * Time.deltaTime);
		transform.position = Vector2.MoveTowards(v2EnemyPosition, v2PlayerPosition, fEnemyChaseSpeed * Time.deltaTime);

		float fDistanceBtwnEnemyAndPlayer = Vector2.Distance((new Vector2(transform.position.x, transform.position.y)), (new Vector2(tPlayer.position.x, tPlayer.position.y)));
		Debug.Log(fDistanceBtwnEnemyAndPlayer);

		if (fDistanceBtwnEnemyAndPlayer < 2f) {
			GetComponent<Renderer>().material.color = Color.red;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			blackboard.iPlayerHealth -= 1;
			tPlayer.gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
	}

	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			tPlayer.gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
	}
}

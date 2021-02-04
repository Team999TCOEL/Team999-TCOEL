using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : Node
{
	private EnemyAI ai;
	private float fSpeedOfEnemy = 2f;
	private float fDistance = 1f;

	private bool bMoveRight = true;

	private Transform tGroundDetection;

	private LayerMask platformLayerMask;

	BlackBoard blackBoard;

	public PatrolNode(EnemyAI ai, LayerMask platformLayerMask) {
		this.ai = ai;
		this.platformLayerMask = platformLayerMask;
	}

	public override NodeState Evaluate() {

		blackBoard = GameObject.FindGameObjectWithTag("BlackBoard").GetComponent<BlackBoard>();

		tGroundDetection = ai.transform.GetChild(0).transform;
		RaycastHit2D groundInfo = Physics2D.Raycast(tGroundDetection.position, Vector2.down, fDistance, platformLayerMask);
		Color rayColor;
		if (groundInfo.collider != null) {
			rayColor = Color.green;
		} else {
			rayColor = Color.red;
		}

		ai.transform.Translate(Vector2.right * fSpeedOfEnemy * Time.deltaTime);

		Debug.DrawRay(tGroundDetection.position, Vector2.down, rayColor);

		if (groundInfo.collider == false && blackBoard.bPlayerSpotted == false) {
			if(bMoveRight == true) {
				ai.transform.eulerAngles = new Vector3(0, -180, 0);
				bMoveRight = false;
			} else {
				ai.transform.eulerAngles = new Vector3(0, 0, 0);
				bMoveRight = true;
			}
			return NodeState.RUNNING;
		} else {
			return NodeState.FAILURE;
		}
	}
}

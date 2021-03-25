using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public Animator animator;

	/// <summary>
	/// Layer Masks
	/// </summary>
	public LayerMask platformLayerMask;
	public LayerMask playerLayerMask;

	/// <summary>
	/// Patrol Variables
	/// </summary>
	/// 
	[System.NonSerialized] public int iEnemyHealth;
	[System.NonSerialized] public float fPatrolRaycastDistance;
	[System.NonSerialized] public float fPatrolSpeedOfEnemy;
	[System.NonSerialized] public bool bPatrolRight;
	public Transform tGroundDetection;

	/// <summary>
	/// Line of Sight Variables
	/// </summary>
	[System.NonSerialized] public float fLoSRaycastDistance;
	[System.NonSerialized] public bool bPlayerSpotted;

	public Transform tPlayer;

	public GameObject goFuelDrop;

	void Start()
    {

    }

    void Update()
    {

    }

	public void Patrol() {
		GetComponent<Renderer>().material.color = Color.green;
		RaycastHit2D groundInfo = Physics2D.Raycast(tGroundDetection.position, Vector2.down, fPatrolRaycastDistance, platformLayerMask);
		Color rayColor;
		if (groundInfo.collider != null) {
			rayColor = Color.green;
		} else {
			rayColor = Color.red;
		}

		transform.Translate(Vector2.left * fPatrolSpeedOfEnemy * Time.deltaTime);

		Debug.DrawRay(tGroundDetection.position, Vector2.down, rayColor);

		if (groundInfo.collider == false) {
			if (bPatrolRight == true) {
				transform.eulerAngles = new Vector3(0, -180, 0);
				bPatrolRight = false;
			} else {
				transform.eulerAngles = new Vector3(0, 0, 0);
				bPatrolRight = true;
			}
		}
	}

	public bool LineOfSightCheck() {
		if (transform.rotation.y == 0) {
			RaycastHit2D rayCastInfo = Physics2D.Raycast(transform.position, Vector2.left, fLoSRaycastDistance, playerLayerMask);

			if (rayCastInfo.collider == true) {
				bPlayerSpotted = true;
			} else {
				bPlayerSpotted = false;
			}
		} else {
			RaycastHit2D rayCastInfo = Physics2D.Raycast(transform.position, Vector2.right, fLoSRaycastDistance, playerLayerMask);
			if (rayCastInfo.collider == true) {
				bPlayerSpotted = true;
			} else {
				bPlayerSpotted = false;
			}
		}

		return bPlayerSpotted;
	}

	public void Attack() {
		float fEnemyChaseSpeed = 6f;

		Vector2 v2PlayerPosition = new Vector2(tPlayer.position.x, tPlayer.position.y);
		Vector2 v2EnemyPosition = new Vector2(transform.position.x, transform.position.y);

		//transform.Translate((new Vector2(tPlayer.position.x, tPlayer.position.y)) * fEnemyChaseSpeed * Time.deltaTime);
		transform.position = Vector2.MoveTowards(v2EnemyPosition, v2PlayerPosition, fEnemyChaseSpeed * Time.deltaTime);

		float fDistanceBtwnEnemyAndPlayer = Vector2.Distance((new Vector2(transform.position.x, transform.position.y)), (new Vector2(tPlayer.position.x, tPlayer.position.y)));
		//Debug.Log(fDistanceBtwnEnemyAndPlayer);

		if (fDistanceBtwnEnemyAndPlayer < 2f) {
			GetComponent<Renderer>().material.color = Color.red;
		}
	}

	public void DropFuel(Vector3 v3FuelDropPosition) {
		Instantiate(goFuelDrop, v3FuelDropPosition, Quaternion.identity);
	}
}

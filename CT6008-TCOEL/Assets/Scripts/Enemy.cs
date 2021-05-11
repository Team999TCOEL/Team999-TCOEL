using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public Animator animator;
	public AudioSource AttackSound;
	public AudioSource TakingDamageSound;

	/// <summary>
	/// Layer Masks
	/// </summary>
	public LayerMask platformLayerMask;
	public LayerMask playerLayerMask;

	/// <summary>
	/// Patrol Variables
	/// </summary>
	/// 
	public float fEnemyHealth;
	[System.NonSerialized] public float fLastHealth;
	[System.NonSerialized] public float fEnemyMaxHealth;
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

	public Transform EnemyTransform;

	public Transform RespawnPoint;

	public ParticleSystem DeathXplosionEffect;

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
			RaycastHit2D rayCastInfoRight = Physics2D.Raycast(transform.position, Vector2.right, fLoSRaycastDistance / 2, playerLayerMask);
			if (rayCastInfo.collider == true) {
				bPlayerSpotted = true;
			} else {
				bPlayerSpotted = false;
			}

			if (rayCastInfoRight.collider == true) {
				transform.eulerAngles = new Vector3(0, -180, 0);
				bPlayerSpotted = true;
			}

		} else {
			RaycastHit2D rayCastInfo = Physics2D.Raycast(transform.position, Vector2.right, fLoSRaycastDistance, playerLayerMask);
			RaycastHit2D rayCastInfoLeft = Physics2D.Raycast(transform.position, Vector2.left, fLoSRaycastDistance / 2, playerLayerMask);

			if (rayCastInfo.collider == true) {
				bPlayerSpotted = true;
			} else {
				bPlayerSpotted = false;
			}

			if (rayCastInfoLeft.collider == true) {
				transform.eulerAngles = new Vector3(0, 0, 0);
				bPlayerSpotted = true;
			}
		}

		return bPlayerSpotted;
	}

	public void Attack() {

		float fEnemyChaseSpeed = 4.8f;
		Vector2 v2PlayerPosition = new Vector2(tPlayer.position.x, tPlayer.position.y);
		Vector2 v2EnemyPosition = new Vector2(transform.position.x, transform.position.y);

		//transform.Translate((new Vector2(tPlayer.position.x, tPlayer.position.y)) * fEnemyChaseSpeed * Time.deltaTime);
		transform.position = Vector2.MoveTowards(v2EnemyPosition, v2PlayerPosition, fEnemyChaseSpeed * Time.deltaTime);

		float fDistanceBtwnEnemyAndPlayer = Vector2.Distance((new Vector2(transform.position.x, transform.position.y)), (new Vector2(tPlayer.position.x, tPlayer.position.y)));
		//Debug.Log(fDistanceBtwnEnemyAndPlayer);
	}

	public void DropFuel(Vector3 v3FuelDropPosition) {
		for(int i = 0; i < 5; i++) {
			goFuelDrop.GetComponent<Fuel>().iFuelDropAmmount = 40;
			Instantiate(goFuelDrop, v3FuelDropPosition, Quaternion.identity);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    public BoxCollider2D BeeAggroBox;

	private bool bPlayerInAggroBox;

	public float fBeePatrolHeight;

	private float fSpeedOfEnemy;

	public BlackBoard blackboard;

	private bool bAttackEnemy;

	public AudioSource BuzzingSound;

	void Start() {
		fLastHealth = fEnemyHealth;
		fEnemyHealth = 300;
        fEnemyMaxHealth = 300;
        fPatrolRaycastDistance = 4f;
        fPatrolSpeedOfEnemy = 2f;
        bPatrolRight = true;
        fLoSRaycastDistance = 2f;
        tPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		BeeAggroBox = GetComponent<BoxCollider2D>();
		bPlayerInAggroBox = false;
		fSpeedOfEnemy = 4f;
		bAttackEnemy = true;
		blackboard = GameObject.FindGameObjectWithTag("BlackBoard").GetComponent<BlackBoard>();
	}

	void Update() {

		if (fEnemyHealth < fLastHealth) {
			TakingDamageSound.Play();
			fLastHealth = fEnemyHealth;
		} else {
			fLastHealth = fEnemyHealth;
		}


		if (fEnemyHealth > 0) {
			if (LineOfSightCheck() == true && tPlayer.GetComponent<PlayerController>().bBossFightCameraActive == false) {
				if (AttackSound.isPlaying == false) {
					AttackSound.Play();
				}
				Attack();
			} else {
				Patrol();
				AttackSound.Stop();
			}
		} else {
			Vector3 v3DeathPosition = transform.position;
			DropFuel(v3DeathPosition);
			Instantiate(DeathXplosionEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}

		float fDistanceBetweenThisAndPlayer = Vector3.Distance(transform.position, tPlayer.transform.position);

		if(fDistanceBetweenThisAndPlayer < 12f) {
			if(BuzzingSound.isPlaying == false){
				BuzzingSound.Play();
			}
			BuzzingSound.volume = Mathf.Clamp((1 * fDistanceBetweenThisAndPlayer), 0.01f, 0.065f);
		} else {
			BuzzingSound.Stop();
		}
	}

	new public void Patrol() {
		if (transform.position.y < fBeePatrolHeight) {
			transform.Translate(Vector2.up * fSpeedOfEnemy * Time.deltaTime);
			transform.Translate(Vector2.left * fSpeedOfEnemy * Time.deltaTime);
		}

		GetComponent<Renderer>().material.color = Color.green;
		RaycastHit2D groundInfo = Physics2D.Raycast(tGroundDetection.position, Vector2.up, fPatrolRaycastDistance, platformLayerMask);
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

	new public bool LineOfSightCheck() {
		if (bAttackEnemy == true) {
			RaycastHit2D rayCastInfo = Physics2D.Raycast(transform.position, Vector2.down, fLoSRaycastDistance, playerLayerMask);
			RaycastHit2D rayCastInfoLeft = Physics2D.Raycast(new Vector2(transform.position.x -2f, transform.position.y), Vector2.down, fLoSRaycastDistance, playerLayerMask);
			RaycastHit2D rayCastInfoRight = Physics2D.Raycast(new Vector2(transform.position.x + 2f, transform.position.y), Vector2.down, fLoSRaycastDistance, playerLayerMask);
			if (rayCastInfo.collider == true || rayCastInfoLeft == true || rayCastInfoRight == true) {
				bPlayerSpotted = true;
			} else {
				bPlayerSpotted = false;
			}
		} else {
			bPlayerSpotted = false;
		}
		return bPlayerSpotted;
	}

	new public void Attack() {
		float fEnemyChaseSpeed = 10f;
		Vector2 v2PlayerPosition = new Vector2(tPlayer.position.x, tPlayer.position.y);
		Vector2 v2EnemyPosition = new Vector2(transform.position.x, transform.position.y);

		transform.position = Vector2.MoveTowards(v2EnemyPosition, v2PlayerPosition, fEnemyChaseSpeed * Time.deltaTime);

		float fDistanceBtwnEnemyAndPlayer = Vector2.Distance((new Vector2(transform.position.x, transform.position.y)), (new Vector2(tPlayer.position.x, tPlayer.position.y)));
		Debug.Log(fDistanceBtwnEnemyAndPlayer);

		if (fDistanceBtwnEnemyAndPlayer < 0.5f) {

			gameObject.GetComponent<PolygonCollider2D>().enabled = false;
			GetComponent<Renderer>().material.color = Color.red;
			StartCoroutine("AttackRecharge");
		}
	}

	public IEnumerator AttackRecharge() {
		blackboard.fPlayerHealth -= 1;
		bAttackEnemy = false;
		yield return new WaitForSeconds(1.0f);
		bAttackEnemy = true;
		gameObject.GetComponent<PolygonCollider2D>().enabled = true;
		BeeAggroBox.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player" && bAttackEnemy == true) {
			Debug.Log("Player");
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			bPlayerInAggroBox = false;
			Debug.Log("NoPlayer");
		}
	}

	new public void DropFuel(Vector3 v3FuelDropPosition) {
		for (int i = 0; i < 5; i++) {
			goFuelDrop.GetComponent<Fuel>().iFuelDropAmmount = 70;
			Instantiate(goFuelDrop, v3FuelDropPosition, Quaternion.identity);
		}
	}
}

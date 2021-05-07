using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
	private float fSpeedOfEnemy = 4f;

	private float fDistance = 1f;

	public float fBeePatrolHeight;

	public bool bMoveLeft = true;

	private bool bPlayerSpotted;

	private float fRaycastDistance = 1f;

	public Transform tWallDetection;

	public Transform tPlayer;

	public LayerMask wallLayerMask;

	public LayerMask playerLayerMask;

	public BlackBoard blackboard;

	public Animator BeeAnimator;

	private BoxCollider2D BeeAggroBox;

	private bool bPlayerInAggroBox;

	void Start()
    {
		BeeAggroBox = GetComponent<BoxCollider2D>();
		bPlayerInAggroBox = false;
    }
    
    void Update()
    {
		if (bPlayerInAggroBox && BeeAggroBox.enabled == true) {
			Attack();
		} else {
			Patrol();
		}

    }

	private void Patrol() {
		if(transform.position.y < fBeePatrolHeight) {
			transform.Translate(Vector2.up * fSpeedOfEnemy * Time.deltaTime);
			transform.Translate(Vector2.left * fSpeedOfEnemy * Time.deltaTime);
		}

		transform.Translate(Vector2.left * fSpeedOfEnemy * Time.deltaTime);

		if (transform.rotation.y == 0) {
			RaycastHit2D rayCastInfo = Physics2D.Raycast(transform.position, Vector2.left, fRaycastDistance, wallLayerMask);
			Color rayColor = Color.green;
			Debug.DrawRay(transform.position, Vector2.left, rayColor);
			if (rayCastInfo.collider == true) {
				transform.eulerAngles = new Vector3(0, -180, 0);
				fSpeedOfEnemy = +fSpeedOfEnemy;
			}
		} else {
			RaycastHit2D rayCastInfo = Physics2D.Raycast(transform.position, Vector2.right, fRaycastDistance, wallLayerMask);
			Color rayColor = Color.blue;
			Debug.DrawRay(transform.position, Vector2.right, rayColor);
			if (rayCastInfo.collider == true) {
				transform.eulerAngles = new Vector3(0, 0, 0);
				fSpeedOfEnemy = -fSpeedOfEnemy;
			} 
		}
	}

	public void Attack() {
		float fEnemyChaseSpeed = 10f;
		Vector2 v2PlayerPosition = new Vector2(tPlayer.position.x, tPlayer.position.y);
		Vector2 v2EnemyPosition = new Vector2(transform.position.x, transform.position.y);

		transform.position = Vector2.MoveTowards(v2EnemyPosition, v2PlayerPosition, fEnemyChaseSpeed * Time.deltaTime);

		float fDistanceBtwnEnemyAndPlayer = Vector2.Distance((new Vector2(transform.position.x, transform.position.y)), (new Vector2(tPlayer.position.x, tPlayer.position.y)));
		Debug.Log(fDistanceBtwnEnemyAndPlayer);

		if (fDistanceBtwnEnemyAndPlayer < 0.5f) {
			BeeAggroBox.enabled = false;
			GetComponent<Renderer>().material.color = Color.red;
			StartCoroutine("AttackRecharge");
		}
	}

	public IEnumerator AttackRecharge() {
		yield return new WaitForSeconds(0.5f);
		BeeAggroBox.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			bPlayerInAggroBox = true;
			Debug.Log("Player");
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			bPlayerInAggroBox = false;
			Debug.Log("NoPlayer");
		}
	}
}

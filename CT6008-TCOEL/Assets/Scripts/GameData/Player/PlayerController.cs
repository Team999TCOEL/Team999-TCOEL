////////////////////////////////////////////////////////////
// File:                 <PlayerController.cs>
// Author:               <Morgan Ellis>
// Date Created:         <30/01/2021>
// Brief:                <File responsible for the movements of the player such as jumping>
// Last Edited By:       <Morgan Ellis
// Last Edited Date:     <13/03/2021>
// Last Edit Brief:      <Character is now able to dash and some other minor QOL changes have been made>
////////////////////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private LayerMask platformLayerMask;

    private float fMoveSpeed = 6f; // defines how fast the players moves

    private Rigidbody2D playerRigidbody2D;

    private float fDistToGround; // used to track how far off the ground the player is and determine if they are grounded

    CapsuleCollider2D capsuleCollider2D;

    public BlackBoard blackboard;

    public Camera mainCamera;

    private float fCameraMaxLookHeight;
    private float fCameraMinLookHeight;

    private bool bDashIsReady;

    private bool bFacingRight;

    private bool bCanPlayerMove;

    public Animator playerAnimator;

    public PlayerUI playerUI;

	private void Awake() {
        blackboard.iPlayerHealth = 5;
        blackboard.iMaxStamina = 100;
        blackboard.iCurrentStamina = 100;
    }

	void Start() {
        fDistToGround = GetComponent<CapsuleCollider2D>().bounds.extents.y; // gets the distance between the box collider and the ground
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();



        bDashIsReady = true;
        bCanPlayerMove = true;
    }

    void Update() {
        if(blackboard.iPlayerHealth <= 0) {

		} else {
            Movement();
            Jumping();
            MoveCamera();
              PlayerDash();
        }

        fCameraMaxLookHeight = transform.position.y + 5;
        fCameraMinLookHeight = transform.position.y - 5;

    }

    // our ground check that sees if the player is more than 0.1f off the floor
    private bool IsGrounded() {
        float fExtraHeightTest = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider2D.bounds.center, Vector2.down, fDistToGround + fExtraHeightTest, platformLayerMask);
        Color rayColor;
        if(raycastHit.collider != null) {
            rayColor = Color.green;
		} else {
            rayColor = Color.red;
		}

        Debug.DrawRay(capsuleCollider2D.bounds.center, Vector2.down * (fDistToGround + fExtraHeightTest), rayColor);

        return raycastHit.collider != null;
    }

	private void Movement() {
        float fMidAirControl = 8f;
        if (Input.GetKey(KeyCode.A) && bCanPlayerMove == true) {
            bFacingRight = false;
            if (bFacingRight == false) {
                playerAnimator.SetBool("bFacingRight", bFacingRight);
                //transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (IsGrounded()) {
                playerRigidbody2D.velocity = new Vector2(-fMoveSpeed, playerRigidbody2D.velocity.y);
            } else {
                playerRigidbody2D.velocity += new Vector2(-fMoveSpeed * fMidAirControl * Time.deltaTime, 0);
                playerRigidbody2D.velocity = new Vector2(Mathf.Clamp(playerRigidbody2D.velocity.x, -fMoveSpeed, +fMoveSpeed), playerRigidbody2D.velocity.y);
			}           
        } else if (Input.GetKey(KeyCode.D) && bCanPlayerMove == true) {
            bFacingRight = true;

            if(bFacingRight == true) {
                playerAnimator.SetBool("bFacingRight", bFacingRight);
                //transform.eulerAngles = new Vector3(0, -180, 0);
            }
            if (IsGrounded()) {
                playerRigidbody2D.velocity = new Vector2(+fMoveSpeed, playerRigidbody2D.velocity.y);
            } else {
                playerRigidbody2D.velocity += new Vector2(+fMoveSpeed * fMidAirControl * Time.deltaTime, 0);
                playerRigidbody2D.velocity = new Vector2(Mathf.Clamp(playerRigidbody2D.velocity.x, -fMoveSpeed, +fMoveSpeed), playerRigidbody2D.velocity.y);
            }
        } else {
			if (IsGrounded()) {
                playerRigidbody2D.velocity = new Vector2(0, playerRigidbody2D.velocity.y);
			}
		}
    }

	private void Jumping() {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && blackboard.iCurrentStamina >= 10) {
            playerUI.UseStamina(10);
            float fJumpVelocity = 18f;
            playerRigidbody2D.velocity = Vector2.up * fJumpVelocity;
        }
    }

    private void MoveCamera() {
        Vector3 velocity = Vector3.up;
        if (Input.GetKeyDown(KeyCode.UpArrow) && mainCamera.transform.position.y < fCameraMaxLookHeight) {
            bCanPlayerMove = false;
            Vector3 v3TargetPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 5f, mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, v3TargetPos, transform.position.y + 5);
        } else if (Input.GetKeyUp(KeyCode.UpArrow)) {
            bCanPlayerMove = true;
		}
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && mainCamera.transform.position.y > fCameraMinLookHeight) {
            bCanPlayerMove = false;
            Vector3 v3TargetPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - 5f, mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, v3TargetPos, transform.position.y + 5);
		} else if (Input.GetKeyUp(KeyCode.DownArrow)) {
            bCanPlayerMove = true;
        }
    }

    private void PlayerDash() {
		if (bDashIsReady) {
            if (Input.GetKeyDown(KeyCode.C) && bFacingRight == true && blackboard.iCurrentStamina >= 15) {
                playerUI.UseStamina(15);
                playerRigidbody2D.position = new Vector2(playerRigidbody2D.position.x + 1.5f, playerRigidbody2D.position.y);
                bDashIsReady = false;
                bCanPlayerMove = false;
                StartCoroutine("WaitForDash");
                StartCoroutine("WaitForPlayerMoveAfterDash");
            } else if(Input.GetKeyDown(KeyCode.C) && bFacingRight == false && blackboard.iCurrentStamina >= 15) {
                playerUI.UseStamina(15);
                playerRigidbody2D.position = new Vector2(playerRigidbody2D.position.x - 1.5f, playerRigidbody2D.position.y);
                bDashIsReady = false;
                bCanPlayerMove = false;
                StartCoroutine("WaitForDash");
                StartCoroutine("WaitForPlayerMoveAfterDash");
            }

        }

    }

    public IEnumerator WaitForPlayerMoveAfterDash() {
        yield return new WaitForSeconds(0.8f);
        bCanPlayerMove = true;
    }
    public IEnumerator WaitForDash() {
        yield return new WaitForSeconds(1.5f);
        bDashIsReady = true;
    }

    public void SavePlayer() {
        SaveSystem.SavePlayer(this); // saves the current settings and position of the player
	}

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer(); // loads the data of the last performed save 

        Vector2 v2PlayerPosition; // create a vector to set the players position
        v2PlayerPosition.x = data.afPlayerPositions[0]; 
        v2PlayerPosition.y = data.afPlayerPositions[1];
        transform.position = v2PlayerPosition; // set the x and y of our player to that of the x and y in the save file
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {

        }
    }
}

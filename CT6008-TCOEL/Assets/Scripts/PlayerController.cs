////////////////////////////////////////////////////////////
// File:                 <PlayerController.cs>
// Author:               <Morgan Ellis>
// Date Created:         <30/01/2021>
// Brief:                <File responsible for the movements of the player such as jumping>
// Last Edited By:       <Morgan Ellis
// Last Edited Date:     <30/01/2021>
// Last Edit Brief:      <Setting up basic movement for the player>
////////////////////////////////////////////////////////////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private LayerMask platformLayerMask;

    private float fMoveSpeed = 8f; // defines how fast the players moves

    private Rigidbody2D playerRigidbody2D;

    private float fDistToGround; // used to track how far off the ground the player is and determine if they are grounded

    CapsuleCollider2D capsuleCollider2D;

    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;

    void Start() {
        fDistToGround = GetComponent<CapsuleCollider2D>().bounds.extents.y; // gets the distance between the box collider and the ground
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        Movement();
        Jumping();
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
        if (Input.GetKey(KeyCode.A)) {
			if (IsGrounded()) {
                playerRigidbody2D.velocity = new Vector2(-fMoveSpeed, playerRigidbody2D.velocity.y);
            } else {
                playerRigidbody2D.velocity += new Vector2(-fMoveSpeed * fMidAirControl * Time.deltaTime, 0);
                playerRigidbody2D.velocity = new Vector2(Mathf.Clamp(playerRigidbody2D.velocity.x, -fMoveSpeed, +fMoveSpeed), playerRigidbody2D.velocity.y);
			}           
        } else if (Input.GetKey(KeyCode.D)) {
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
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            float fJumpVelocity = 20f;
            playerRigidbody2D.velocity = Vector2.up * fJumpVelocity;
        }
    }

    public void SavePlayer() {
        SaveSystem.SavePlayer(this); // saves the current settings and position of the player
	}

    public void LoadPlaer() {
        PlayerData data = SaveSystem.LoadPlayer(); // loads the data of the last performed save 

        Vector2 v2PlayerPosition; // create a vector to set the players position
        v2PlayerPosition.x = data.afPlayerPositions[0]; 
        v2PlayerPosition.y = data.afPlayerPositions[1];
        transform.position = v2PlayerPosition; // set the x and y of our player to that of the x and y in the save file
	}
}

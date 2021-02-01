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

    private float fJumpHeight = 5f; // defines how high the player can jump

    private float fVerticalVelocity; // float used to store the vertical velocity of the player when jumping
    private float fGravity = 14f; // float that defines how fast the player will fall back to the ground

    private float fDistToGround; // used to track how far off the ground the player is and determine if they are grounded

    BoxCollider2D boxCollider2D;

    void Start() {
        fDistToGround = GetComponent<BoxCollider2D>().bounds.extents.y; // gets the distance between the box collider and the ground
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.A)) {
            transform.position += transform.right * -fMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.position += transform.right * fMoveSpeed * Time.deltaTime;
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            Vector2 v2JumpHeight = new Vector2(0, fJumpHeight * Time.deltaTime);
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * 250 * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    // our ground check that sees if the player is more than 0.1f off the floor
    private bool IsGrounded() {
        float fExtraHeightTest = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, fDistToGround + fExtraHeightTest, platformLayerMask);
        Color rayColor;
        if(raycastHit.collider != null) {
            rayColor = Color.green;
		} else {
            rayColor = Color.red;
		}

        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (fDistToGround + fExtraHeightTest), rayColor);

        return raycastHit.collider != null;
    }
}

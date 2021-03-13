////////////////////////////////////////////////////////////
// File:                 <CameraFollow.cs>
// Author:               <Morgan Ellis>
// Date Created:         <09/02/2021>
// Brief:                <File responsible for the movements of the player such as jumping>
// Last Edited By:       <Morgan Ellis>
// Last Edited Date:     <13/03/2021>
// Last Edit Brief:      <The camera now is smoother when following enemies including dashes>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Camera playerCamera;

    // Update is called once per frame
    void Update() {
		if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) {
            if (target) {
                Vector3 point = playerCamera.WorldToViewportPoint(target.position);
                Vector3 delta = target.position - playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
        }
    }
}


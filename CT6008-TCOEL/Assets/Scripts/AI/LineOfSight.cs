using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    LayerMask playerLayerMask;

    //public Transform playerTransform;
    public Transform enemyTransform;

    private float fRaycastDistance = 5f;
    
    void Start()
    {
    }

    
    void Update()
    {

    }

    private RaycastHit2D PlayerRaycastTest(RaycastHit2D playerRaycastHit) {

        if (enemyTransform.rotation.eulerAngles.y == -180) {
            playerRaycastHit = Physics2D.Raycast(enemyTransform.position, Vector2.left, fRaycastDistance, playerLayerMask);
        }

        Color rayColor;
        if (playerRaycastHit.collider != null) {
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }

        Debug.DrawRay(enemyTransform.position, Vector2.right, rayColor);

        return playerRaycastHit;

    }
}

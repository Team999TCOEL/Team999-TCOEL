using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public float speed; // how fast the background is going to move

    public float endX; // where the background is going to end
    public float startX; // where the bakground is going to start

    private void Update() {
        transform.Translate(Vector2.right * -speed * Time.deltaTime); // move the background left towards the players give a paralax effect

        // if the background meets the end position, place it at the start to give the impresion of a seemless moving bcakground
        if (transform.position.x <= endX) {
            Vector2 pos = new Vector2(startX, transform.position.y);
            transform.position = pos;
        }
    }
}

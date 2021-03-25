using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {

            Debug.Log("Enemy Hit");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-12f, collision.transform.position.y);
        }
    }

	private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<Enemy>().iEnemyHealth -= 1;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject go_EnemyPrefab;
    public Transform RespawnTransform;

    private PlayerController playerController;
    public bool bEnemyDead;

	private void Awake() {
	}

	void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.bResetWorld == true && this.gameObject.transform.childCount < 1) {
            Respawn();
            playerController.bResetWorld = false;
        } else {
            playerController.bResetWorld = false;
        }
    }

    public void Respawn() {
        GameObject enemy = Instantiate(go_EnemyPrefab, RespawnTransform.position, RespawnTransform.rotation);
        enemy.transform.SetParent(this.gameObject.transform);
    }
}

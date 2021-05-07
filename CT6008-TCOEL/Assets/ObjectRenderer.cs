using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRenderer : MonoBehaviour
{
    List<GameObject> objectsInUse;
    public GameObject go_Player;
    List<GameObject> objectsInScene = new List<GameObject>();
    void Start()
    {

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
            if (gameObject.GetComponent<ItemWorldSpawner>() != null) {
                continue;
            } else {
                objectsInUse.Add(go);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject gameObject in objectsInScene) {
            if (gameObject.transform.position.x > (go_Player.transform.position.x + 60) || gameObject.transform.position.x < (go_Player.transform.position.x - 60)) {
                gameObject.SetActive(false);
            } else {
                gameObject.SetActive(true);
            }
        }
    }
}

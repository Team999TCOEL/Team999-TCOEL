﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.Z)) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(300f, 0.479506f, -10), 0.01f);
		}
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRise : MonoBehaviour
{

    [SerializeField] float waterRiseSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2 (0f, 0.005f*waterRiseSpeed*Time.deltaTime));
    }
}

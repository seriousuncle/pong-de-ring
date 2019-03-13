﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float startForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMove()
    {
        var vec = new Vector2(startForce, 0f);
        vec = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) * vec;
        GetComponent<Rigidbody2D>().AddForce(vec, ForceMode2D.Impulse);
    }
}
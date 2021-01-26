using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvanzarEnX : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody2D _rigidBody = null;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidBody.velocity = new Vector2(speed, 0);
    }
}

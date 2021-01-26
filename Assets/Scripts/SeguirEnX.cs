using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirEnX : MonoBehaviour
{
    public Transform target;
    private float posicionFinalX;
    // Update is called once per frame
    private void Start()
    {
        posicionFinalX = this.transform.position.x - target.position.x;
    }

    void Update ()
    {
        transform.position = new Vector3(
            target.position.x + posicionFinalX,
            transform.position.y,
            transform.position.z
            );
    }
}

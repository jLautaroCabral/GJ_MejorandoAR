using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoverCamara : MonoBehaviour
{
    [SerializeField] Transform objetivo = null;
    public Transform cam;
    public Vector3 offset;
    Vector3 obPosicion;

    void Update()
    {
        obPosicion = objetivo.position;
    }

    private void FixedUpdate()
    {
        Vector3 posicionOrigen = Camera.main.transform.position;
        Vector3 posicionFinal = obPosicion - offset;

        Camera.main.transform.position = Vector3.Lerp(posicionOrigen, posicionFinal, .05f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoNubes : MonoBehaviour
{
    float speed = 0.6f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f * speed, -1f * speed);
    }

    private void Update()
    {
        Vector3 posicionActual = transform.position;

        if (posicionActual.x <= 3.5f && posicionActual.y <= -2.8f)
            transform.position = new Vector3(0.915f, 0.202f, 0f);
    }
}

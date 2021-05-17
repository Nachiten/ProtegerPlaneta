using UnityEngine;

public class MovimientoColorPlaneta : MonoBehaviour
{
    float speed = 0.6f;

    Vector3 posicionInicial;

    /* -------------------------------------------------------------------------------- */

    void Start()
    {
        posicionInicial = transform.position;
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1f * speed, -1f * speed);
    }

    /* -------------------------------------------------------------------------------- */

    private void Update()
    {
        Vector3 posicionActual = transform.position;

        if (posicionActual.x <= -6.01f && posicionActual.y <= -6.01f)
            transform.position = posicionInicial;
    }
}

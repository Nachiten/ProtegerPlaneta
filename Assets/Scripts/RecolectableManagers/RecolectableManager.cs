using UnityEngine;

public class RecolectableManager : MonoBehaviour
{
    public GameObject guia;

    float velocidad = 0.5f;

    bool pausa = false;

    Vector2 velocidadActual;

    /* -------------------------------------------------------------------------------- */

    void OnEnable()
    {
        transform.position = new Vector3(0,0,0);

        float rotacionZ = Random.Range(0, 360);

        //Aplico rotacion correspondiente
        transform.Rotate(new Vector3(0, 0, rotacionZ));

        transform.right = guia.transform.position - transform.position;

        velocidadActual = -transform.right.normalized * velocidad;

        GetComponent<Rigidbody2D>().velocity = velocidadActual;
    }

    public void manejarPausa() 
    {
        pausa = !pausa;

        if (pausa)
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        else
            GetComponent<Rigidbody2D>().velocity = velocidadActual;

    }
}

using UnityEngine;

public class RecolectableManager : MonoBehaviour
{
    public GameObject guia;

    float velocidad = 0.5f;

    /* -------------------------------------------------------------------------------- */

    void OnEnable()
    {
        transform.position = new Vector3(0,0,0);

        float rotacionZ = Random.Range(0, 360);

        //Aplico rotacion correspondiente
        transform.Rotate(new Vector3(0, 0, rotacionZ));

        transform.right = guia.transform.position - transform.position;

        GetComponent<Rigidbody2D>().velocity = -transform.right.normalized * velocidad;
    }
}

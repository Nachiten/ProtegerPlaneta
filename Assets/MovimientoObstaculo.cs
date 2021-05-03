using UnityEngine;

public class MovimientoObstaculo : MonoBehaviour
{
    GameObject planeta;

    float speed = 3f;
    float posInicial = 7f;

    // Start is called before the first frame update
    void OnEnable()
    {
        planeta = GameObject.Find("Planeta");

        int izquierdaODerecha = Random.Range(0, 2);

        int signo = 1;

        if (izquierdaODerecha == 0)
            signo = -1;

        float posY = Random.Range(-posInicial, posInicial);

        transform.position = new Vector2(posInicial * signo, posY);

        transform.right = planeta.transform.position - transform.position;

        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("[MovimientoObstaculo] OnTriggerEnter2D");
        Debug.Log("Colisione con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Planeta")) 
        {
            Debug.Log("Perdiste un punto!!");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Jugador")) 
        {
            Debug.Log("Lo paraste!!");
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }

}

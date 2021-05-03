using UnityEngine;

public class MovimientoObstaculo : MonoBehaviour
{
    GameObject planeta;
    GameManager codigoGameManager;

    float speed = 3f;
    float posInicial = 7f;

    // Start is called before the first frame update
    void OnEnable()
    {
        planeta = GameObject.Find("Planeta");
        codigoGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Numero entre 0 y 3 (inclusive)
        int ladoAparicion = Random.Range(0, 4);

        float posY;
        float posX;

        switch (ladoAparicion) 
        {
            // Arriba
            case 0:
                posX = Random.Range(-posInicial, posInicial);
                posY = posInicial;
                break;
            // Izquierda
            case 1:
                posX = -posInicial;
                posY = Random.Range(-posInicial, posInicial);
                break;
            // Abajo
            case 2:
                posX = Random.Range(-posInicial, posInicial);
                posY = -posInicial;
                break;
            // Derecha
            default:
                posX = posInicial;
                posY = Random.Range(-posInicial, posInicial);
                break;
        }

        transform.position = new Vector2(posX, posY);

        transform.right = planeta.transform.position - transform.position;

        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("[MovimientoObstaculo] OnTriggerEnter2D");
        Debug.Log("Colisione con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Planeta")) 
        {
            Debug.Log("Perdiste el juego!!");
            codigoGameManager.perderJuego();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Jugador")) 
        {
            Debug.Log("Sumaste un punto!!");
            codigoGameManager.sumarPuntos(1);
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }

}

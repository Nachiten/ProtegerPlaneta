using UnityEngine;

public class MovimientoObstaculo : MonoBehaviour
{
    public bool noPerder = false;

    GameObject planeta;
    GameManager codigoGameManager;

    float speed = 950f;
    float posInicial = 7f;

    static int contrarioAUltimaAparicion = -1;

    void Start()
    {
        planeta = GameObject.Find("Planeta");
        codigoGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        int ladoAparicion;

        // Checkeo para no spawnear en el lado contrario al ultimo que aparecio
        do
        {
            ladoAparicion = Random.Range(0, 4);
        } 
        while (ladoAparicion == contrarioAUltimaAparicion);

        float posY;
        float posX;

        switch (ladoAparicion) 
        {
            // Arriba
            case 0:
                posX = Random.Range(-posInicial, posInicial);
                posY = posInicial;
                contrarioAUltimaAparicion = 2;
                break;
            // Izquierda
            case 1:
                posX = -posInicial;
                posY = Random.Range(-posInicial, posInicial);
                contrarioAUltimaAparicion = 3;
                break;
            // Abajo
            case 2:
                posX = Random.Range(-posInicial, posInicial);
                posY = -posInicial;
                contrarioAUltimaAparicion = 0;
                break;
            // Derecha
            default:
                posX = posInicial;
                posY = Random.Range(-posInicial, posInicial);
                contrarioAUltimaAparicion = 1;
                break;
        }

        transform.position = new Vector2(posX, posY);

        transform.right = planeta.transform.position - transform.position;

        GetComponent<Rigidbody2D>().velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("[MovimientoObstaculo] OnTriggerEnter2D");
        //Debug.Log("Colisione con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Planeta"))
        {
            if (noPerder)
            {
                Destroy(gameObject);
                return;
            }

            Debug.Log("Perdiste el juego!!");
            codigoGameManager.perderJuego();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Jugador")) 
        {
            Debug.Log("Sumaste un punto!!");
            codigoGameManager.sumarPuntos(1);
            Destroy(gameObject);
        }
    }

}

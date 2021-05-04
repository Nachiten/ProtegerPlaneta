using UnityEngine;

public class MovimientoObstaculo : MonoBehaviour
{
    public bool noPerder = false;

    GameManager codigoGameManager;
    ObstacleSpawner spawnerObstaculos;
    GameObject planeta;

    public float speed = 2.5f;
    float posInicialMax = 7f;

    static int contrarioAUltimaAparicion = -1;

    void Awake()
    {
        planeta = GameObject.Find("Planeta");

        GameObject gameManager = GameObject.Find("GameManager");

        codigoGameManager = gameManager.GetComponent<GameManager>();
        spawnerObstaculos = gameManager.GetComponent<ObstacleSpawner>();

        if (noPerder)
            Debug.LogError("[MovimientoObstaculo] NO SE PERMITE PERDER!!");
    }

    private void OnEnable()
    {
        int ladoAparicion;

        // Checkeo para no spawnear en el lado contrario al ultimo que aparecio
        do
        {
            ladoAparicion = Random.Range(0, 4);
        }
        while (ladoAparicion == contrarioAUltimaAparicion);

        float posY;
        float posX;

        // Asigno posicion random en base al lado
        switch (ladoAparicion)
        {
            // Arriba
            case 0:
                posX = Random.Range(-posInicialMax, posInicialMax);
                posY = posInicialMax;
                contrarioAUltimaAparicion = 2;
                break;
            // Izquierda
            case 1:
                posX = -posInicialMax;
                posY = Random.Range(-posInicialMax, posInicialMax);
                contrarioAUltimaAparicion = 3;
                break;
            // Abajo
            case 2:
                posX = Random.Range(-posInicialMax, posInicialMax);
                posY = -posInicialMax;
                contrarioAUltimaAparicion = 0;
                break;
            // Derecha
            default:
                posX = posInicialMax;
                posY = Random.Range(-posInicialMax, posInicialMax);
                contrarioAUltimaAparicion = 1;
                break;
        }

        // Fijo la posicion a la random asignada
        transform.position = new Vector2(posX, posY);

        // Hago que el obstaculo mire hacia el planeta
        transform.right = planeta.transform.position - transform.position;

        // Fijo velocidad
        GetComponent<Rigidbody2D>().velocity = transform.right.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planeta"))
        {
            if (noPerder) 
            {
                spawnerObstaculos.ocultarObstaculo(gameObject);
                return;
            }
                    
            Debug.Log("Perdiste el juego!!");
            codigoGameManager.perderJuego();
            
        }

        if (collision.gameObject.CompareTag("Jugador")) 
        {
            Debug.Log("Sumaste un punto!!");
            codigoGameManager.sumarPuntos(1);
            
        }

        spawnerObstaculos.ocultarObstaculo(gameObject);
    }
}

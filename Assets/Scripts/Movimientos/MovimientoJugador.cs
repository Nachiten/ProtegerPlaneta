using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    bool perdio = false;
    float speedRotacion = 250f;

    float speedDifficultyMultiplier;

    GameMode gameModeActual;

    void Awake()
    {
        gameModeActual = GameObject.Find("GameManager").GetComponent<GameManager>().obtenerGameMode();

        // Cambio velocidad de movimiento del jugador en base a dificulta
        switch (gameModeActual)
        {
            // Dificultad facil tiene mas velocidad
            case GameMode.Easy:
                speedDifficultyMultiplier = 1.3f;
                break;
            // Dificultad alta y hardcore tienen menos velocidad
            case GameMode.Difficult:
            case GameMode.Hardcore:
                speedDifficultyMultiplier = 0.7f;
                break;
            // Caso dificultad media
            default:
                speedDifficultyMultiplier = 1f;
                break;
        }
    }

    void Update()
    {
        if (perdio)
            return;

        int signoRotacion = 0;

        // Izquierda (+)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || clickeoIzquierdaPantalla()) 
        {
            signoRotacion = 1;
        }

        // Derecha (-)
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || clickeoDerechaPantalla())
        {
            signoRotacion = -1;
        }
        
        // Aplico rotacion correspondiente
        transform.Rotate(new Vector3(0, 0, signoRotacion * Time.deltaTime * speedRotacion * speedDifficultyMultiplier));

        ejecutarReloj();
    }

    float speedReloj = 0.7f;
    float timePassed = 0;
    float intervaloAparicion = 4f;
    float aumentoVelocidad = 0.05f;

    void ejecutarReloj() 
    {
        timePassed += Time.deltaTime * speedReloj;

        if (timePassed >= intervaloAparicion)
        {
            timePassed = 0;

            speedReloj += aumentoVelocidad;
            
            aumentarSpeed(aumentoVelocidad);
        }

    }

    bool clickeoDerechaPantalla() 
    {
        if (!Input.GetMouseButton(0))
            return false;

        return Input.mousePosition.x >= Screen.width / 2;
    }

    bool clickeoIzquierdaPantalla() 
    {
        if (!Input.GetMouseButton(0))
            return false;

        return Input.mousePosition.x <= Screen.width / 2;
    }

    public void aumentarSpeed(float aumento) 
    {
        speedRotacion += aumento;
    }

    public void perderJuego() 
    {
        perdio = true;
    }
}
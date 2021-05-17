using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    bool perdio = false;
    float speedRotacion = 250f;

    float speedDifficultyMultiplier;

    GameMode gameModeActual;

    float speedReloj = 0.7f;
    float timePassed = 0;
    float intervaloAparicion = 4f;
    float aumentoVelocidad = 0.05f;

    bool pausa = false;

    /* -------------------------------------------------------------------------------- */

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

    /* -------------------------------------------------------------------------------- */

    void Update()
    {
        if (perdio || pausa)
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

    /* -------------------------------------------------------------------------------- */

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

    /* -------------------------------------------------------------------------------- */

    bool clickeoDerechaPantalla() 
    {
        // Si hizo click en la parte derecha de la pantalla
        return Input.mousePosition.x >= Screen.width / 2 && Input.GetMouseButton(0);
    }

    /* -------------------------------------------------------------------------------- */

    bool clickeoIzquierdaPantalla() 
    {
        // Si hizo click en la parte izquierda de la pantalla
        return Input.mousePosition.x <= Screen.width / 2 && Input.GetMouseButton(0);
    }

    /* -------------------------------------------------------------------------------- */

    public void aumentarSpeed(float aumento)  { speedRotacion += aumento; }

    /* -------------------------------------------------------------------------------- */

    public void perderJuego()  { perdio = true; }

    public void manejarPausa() { pausa = !pausa; }
}
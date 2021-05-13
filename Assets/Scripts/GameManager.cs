using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum GameMode
{
    Easy,
    Normal,
    Difficult,
    Hardcore
}

public class GameManager : MonoBehaviour
{
    public bool noPuedePerder = false;
    int puntos = 0;
    float vidaActual = 10f;

    float vidaTotal = 10f;

    TMP_Text textoPuntos;

    Slider scrollbarVida;

    GameObject textoPerdiste, fillAreaScroll;

    Func<bool> funcionPerdiJuego = null;

    public static GameMode gameModeActual = GameMode.Normal;

    private void Awake()
    {
        Debug.Log("[GameManager] Modo de juego actual: " + gameModeActual);

        textoPuntos = GameObject.Find("Puntos").GetComponent<TMP_Text>();
        scrollbarVida = GameObject.Find("MedidorVida").GetComponent<Slider>();
        textoPerdiste = GameObject.Find("HasPerdido");
        fillAreaScroll = GameObject.Find("FillAreaVida");
    }

    private void Start()
    {
        ajustarVidaEnBaseADificultad();

        mostrarPuntos();
        mostrarVida();

        textoPerdiste.SetActive(false);

        // Asigno puntero a funcion dependiendo del modo actual de juego
        if (gameModeActual == GameMode.Hardcore)
            funcionPerdiJuego = perdiJuegoHardcore;
        else
            funcionPerdiJuego = perdiJuegoNormal;
    }

    /* ------------------------------------------------------------------------------------ */
    /* ----------------------------------- PERDER JUEGO ----------------------------------- */
    /* ------------------------------------------------------------------------------------ */

    bool perdiJuegoNormal() 
    {
        return vidaActual <= 0f;
    }

    bool perdiJuegoHardcore()
    {
        return true;
    }

    void perderJuego() 
    {
        fillAreaScroll.SetActive(false);

        if (noPuedePerder) 
            return;

        string nombrePlayerPref = "Points_" + (int)gameModeActual;

        // Si los puntos ganados son mayores a los previamente guardados
        if (puntos > PlayerPrefs.GetInt(nombrePlayerPref))
        {
            Debug.Log("[GameManager] Guardando player pref. Nombre: " + nombrePlayerPref + " | Puntos: " + puntos);

            // Seteo una nueva player pref
            PlayerPrefs.SetInt(nombrePlayerPref, puntos);
        }
        else 
        {
            Debug.Log("[GameManager] Player pref guardada anteriormente es mayor, no guardo.");
        }

        textoPerdiste.SetActive(true);
        GetComponent<ObstacleSpawner>().perderJuego();
        GetComponent<RecolectableSpawner>().perderJuego();
        GameObject.Find("Jugador").GetComponent<MovimientoJugador>().perderJuego();
    }

    /* ---------------------------------------------------------------------------------- */
    /* -------------------------------------- VIDA -------------------------------------- */
    /* ---------------------------------------------------------------------------------- */

    public void perderVida(float daño)
    {
        vidaActual = Mathf.Max(vidaActual - daño, 0f);
        mostrarVida();

        if (funcionPerdiJuego())
            perderJuego();
    }

    public void aumentarVida(float suma)
    {
        vidaActual = Mathf.Min(vidaActual + suma, vidaTotal);
        mostrarVida();
    }

    void mostrarVida() 
    {
        scrollbarVida.value = vidaActual / vidaTotal;
    }

    /* -------------------------------------------------------------------------------- */
    /* ------------------------------------ PUNTOS ------------------------------------ */
    /* -------------------------------------------------------------------------------- */

    public void sumarPuntos(int puntos)
    {
        this.puntos += puntos;
        mostrarPuntos();
    }

    void mostrarPuntos()
    {
        textoPuntos.text = puntos.ToString();
    }

    /* --------------------------------------------------------------------------------- */
    /* ------------------------------------- OTROS ------------------------------------- */
    /* --------------------------------------------------------------------------------- */

    public GameMode obtenerGameMode()
    {
        return gameModeActual;
    }

    void ajustarVidaEnBaseADificultad()
    {
        // Cambio velocidad de movimiento del jugador en base a dificultad
        switch (gameModeActual)
        {
            // Dificultad facil tiene mas vida
            case GameMode.Easy:
                vidaTotal = 13f;
                break;
            // Dificultad alta y hardcore tienen menos vida
            case GameMode.Difficult:
            case GameMode.Hardcore:
                vidaTotal = 7.5f;
                break;
            // Caso dificultad media
            default:
                vidaTotal = 10f;
                break;
        }

        vidaActual = vidaTotal;
    }
}

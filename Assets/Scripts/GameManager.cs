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

    float vidaActual = 10f, vidaTotal = 10f;

    Slider scrollbarVida;
    Image rellenoSlider;
    TMP_Text textoPuntos;
    GameObject textoPerdiste, fillAreaScroll;

    public static GameMode gameModeActual = GameMode.Normal;

    Func<bool> funcionPerdiJuego = null;

    bool perdio = false;

    /* -------------------------------------------------------------------------------- */

    private void Awake()
    {
        Debug.Log("[GameManager] Modo de juego actual: " + gameModeActual);

        textoPuntos = GameObject.Find("Puntos").GetComponent<TMP_Text>();
        scrollbarVida = GameObject.Find("MedidorVida").GetComponent<Slider>();
        textoPerdiste = GameObject.Find("HasPerdido");
        fillAreaScroll = GameObject.Find("FillAreaVida");
        rellenoSlider = GameObject.Find("RellenoSliderVida").GetComponent<Image>();
    }

    /* -------------------------------------------------------------------------------- */

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

    /* -------------------------------------------------------------------------------- */

    public void perderJuego() 
    {
        perdio = true;

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

    public void perderVida(float da�o)
    {
        vidaActual = Mathf.Max(vidaActual - da�o, 0f);
        mostrarVida();

        if (funcionPerdiJuego() && !perdio)
            perderJuego();
    }

    public void aumentarVida(float suma)
    {
        vidaActual = Mathf.Min(vidaActual + suma, vidaTotal);
        mostrarVida();
    }

    float factor = 0.4f; // 0.49f

    void mostrarVida() 
    {
        float valorVida = vidaActual / vidaTotal;

        scrollbarVida.value = valorVida;
        rellenoSlider.color = new Color(1, valorVida * factor, valorVida * factor, 1);
    }

    /* ------------------------------------------------------------------------------------ */
    /* -------------------------------------- PUNTOS -------------------------------------- */
    /* ------------------------------------------------------------------------------------ */

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

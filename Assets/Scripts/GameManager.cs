using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public int puntos = 0;
    public float vida = 10;

    TMP_Text textoPuntos;

    Slider scrollbarVida;

    GameObject textoPerdiste;
    GameObject fillAreaScroll;

    Func<bool> funcionPerdiJuego = null;

    public static GameMode gameModeActual = GameMode.Easy;

    public GameMode obtenerGameMode() {
        return gameModeActual;
    }

    private void Start()
    {
        textoPuntos = GameObject.Find("Puntos").GetComponent<TMP_Text>();
        scrollbarVida = GameObject.Find("MedidorVida").GetComponent<Slider>();
        textoPerdiste = GameObject.Find("HasPerdido");
        fillAreaScroll = GameObject.Find("FillAreaVida");

        mostrarPuntos();
        mostrarVida();

        textoPerdiste.SetActive(false);

        // Asigno puntero a funcion dependiendo del modo actual de juego
        //funcionPerdiJuego = perdiJuegoNormal;
        funcionPerdiJuego = perdiJuegoHardcore;
    }

    bool perdiJuegoNormal() 
    {
        return vida <= 0f;
    }

    bool perdiJuegoHardcore()
    {
        return true;
    }

    public void sumarPuntos(int puntos) 
    {
        this.puntos += puntos;
        mostrarPuntos();
    }

    public void perderVida(float daño) 
    {
        vida = Mathf.Max(vida - daño, 0f);
        mostrarVida();

        if (funcionPerdiJuego()) 
            perderJuego();

    }

    public void aumentarVida(float suma)
    {
        vida = Mathf.Min(vida + suma, 10);
        mostrarVida();
    }

    void perderJuego() 
    {
        fillAreaScroll.SetActive(false);

        if (noPuedePerder) 
            return;
        
            
        textoPerdiste.SetActive(true);
        GetComponent<ObstacleSpawner>().perderJuego();
        GetComponent<RecolectableSpawner>().perderJuego();
        GameObject.Find("Jugador").GetComponent<MovimientoJugador>().perderJuego();
    }

    public void cargarEscena(int index) 
    {
        SceneManager.LoadScene(index);
    }

    void mostrarVida() 
    {
        scrollbarVida.value = vida / 10;
    }

    void mostrarPuntos()
    {
        textoPuntos.text = puntos.ToString();
    }

    public void setearNoPuedePerder(bool valor) 
    {
        noPuedePerder = valor;
    }
}

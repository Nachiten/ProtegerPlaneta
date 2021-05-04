using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    int puntos = 0;
    TMP_Text textoPuntos;
    Slider scrollbarVida;
    GameObject textoPerdiste;
    GameObject fillAreaScroll;
    float vida = 10;

    private void Start()
    {
        textoPuntos = GameObject.Find("Puntos").GetComponent<TMP_Text>();
        textoPerdiste = GameObject.Find("HasPerdido");
        scrollbarVida = GameObject.Find("MedidorVida").GetComponent<Slider>();
        fillAreaScroll = GameObject.Find("FillAreaVida");

        mostrarPuntos();
        mostrarVida();

        textoPerdiste.SetActive(false);
    }

    
    public void sumarPuntos(int puntos) 
    {
        this.puntos += puntos;
        mostrarPuntos();
    }

    public void perderVida(float daño) 
    {
        vida = Mathf.Max(vida - daño, 0);
        mostrarVida();

        if (vida == 0) 
        {
            perderJuego();
        }
    }

    void perderJuego() 
    {
        fillAreaScroll.SetActive(false);
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

}

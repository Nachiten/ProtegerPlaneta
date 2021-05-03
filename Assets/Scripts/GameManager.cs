using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    int puntos = 0;
    TMP_Text textoPuntos;
    GameObject textoPerdiste;

    private void Start()
    {
        textoPuntos = GameObject.Find("Puntos").GetComponent<TMP_Text>();
        textoPerdiste = GameObject.Find("HasPerdido");

        mostrarPuntos();
        textoPerdiste.SetActive(false);
    }

    void mostrarPuntos() 
    {
        textoPuntos.text = puntos.ToString();
    }

    public void sumarPuntos(int puntos) 
    {
        this.puntos += puntos;
        mostrarPuntos();
    }

    public void perderJuego() 
    {
        textoPerdiste.SetActive(true);
        GetComponent<ObstacleSpawner>().perderJuego();
        GameObject.Find("Jugador").GetComponent<MovimientoJugador>().perderJuego();
    }

    public void cargarEscena(int index) 
    {
        SceneManager.LoadScene(index);
    }
}

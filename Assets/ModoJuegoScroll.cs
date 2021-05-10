using UnityEngine;
using TMPro;

public class ModoJuegoScroll : MonoBehaviour
{
    TMP_Text textoDescripcion, textoNombreModo;

    private void Awake()
    {
        textoDescripcion = GameObject.Find("DescripcionModo").GetComponent<TMP_Text>();
        textoNombreModo = GameObject.Find("NombreModo").GetComponent<TMP_Text>();
    }

    public void seleccionarModoJuego(int modo) 
    {
        GameMode modoJuego = (GameMode)modo;

        switch (modoJuego) 
        {
            case GameMode.Easy:
                textoNombreModo.text = "Modo Facil:";
                textoDescripcion.text = "La velocidad de los obstaculos es menor a la normal, la velocidad de movimiento del jugador y la vida son mayores.";
                break;
            case GameMode.Normal:
                textoNombreModo.text = "Modo Normal:";
                textoDescripcion.text = "La experiencia de juego por defecto.";
                break;
            case GameMode.Difficult:
                textoNombreModo.text = "Modo Dificil:";
                textoDescripcion.text = "La velocidad de los obstaculos es mayor a la normal, la velocidad de movimiento del jugador y la vida son menores.";
                break;
            case GameMode.Hardcore:
                textoNombreModo.text = "Modo Hardcore:";
                textoDescripcion.text = "Igual a dificil, pero sin vida, un solo golpe te mata.";
                break;
        }

        GameManager.gameModeActual = modoJuego;
    }
}

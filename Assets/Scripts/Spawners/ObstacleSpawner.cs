using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    bool perdio = false;

    public GameObject obstaculoFacilPrefab;
    public GameObject obstaculoMedioPrefab;
    public GameObject obstaculoDificilPrefab;

    List<Obstaculo> obstaculos;

    float speedDifficultyMultiplier;

    static GameMode gameModeActual;

    bool pausa = false;

    void Start()
    {
        gameModeActual = GameObject.Find("GameManager").GetComponent<GameManager>().obtenerGameMode();

        // Cambio velocidad de obstaculos en base a dificultad
        switch (gameModeActual) 
        {
            // Dificultad facil tiene menos velocidad
            case GameMode.Easy:
                speedDifficultyMultiplier = 0.6f;
                break;
            // Dificultad alta y hardcore generan incremento de velocidad
            case GameMode.Difficult:
            case GameMode.Hardcore:
                speedDifficultyMultiplier = 1.7f;
                break;
            // Caso dificultad media
            default:
                speedDifficultyMultiplier = 1f;
                break;
        }

        obstaculos = new List<Obstaculo>();

        // Obstaculo Facil
        obstaculos.Add(new Obstaculo(3f, 0.05f, 2.8f * speedDifficultyMultiplier, obstaculoFacilPrefab, 0.7f));
        // Obstaculo Medio
        obstaculos.Add(new Obstaculo(6f, 0.03f, 1.6f * speedDifficultyMultiplier, obstaculoMedioPrefab, 1.2f));
        // Obstaculo Dificil
        obstaculos.Add(new Obstaculo(9.5f, 0.02f, 0.9f * speedDifficultyMultiplier, obstaculoDificilPrefab, 2.3f));
    }

    void Update()
    {
        if (perdio || pausa)
            return;

        foreach (Obstaculo unObstaculo in obstaculos)
            unObstaculo.ejecutarReloj();
    }

    public void perderJuego()
    {
        perdio = true;
    }

    public void ocultarObstaculo(GameObject unObstaculo) 
    {
        foreach (Obstaculo unObstaculoObjeto in obstaculos)
            unObstaculoObjeto.ocultarObstaculo(unObstaculo);
    }

    public void manejarPausaObstaculos() 
    {
        pausa = !pausa;

        foreach (Obstaculo unObstaculo in obstaculos)
            unObstaculo.manejarPausa();
    }
}

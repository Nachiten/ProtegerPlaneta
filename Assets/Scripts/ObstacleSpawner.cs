using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    bool perdio = false;

    public GameObject obstaclePrefab;

    List<Obstaculo> obstaculos;

    void Start()
    {
        obstaculos = new List<Obstaculo>();

        // Obstaculo Facil
        obstaculos.Add(new Obstaculo(3f, 0.05f, 2.8f, obstaclePrefab, 5, new Color(1, 1, 0, 1)));

        // Obstaculo Dificil
        obstaculos.Add(new Obstaculo(6f, 0.02f, 1.9f, obstaclePrefab, 7, new Color(1, 0, 0, 1)));
    }

    void Update()
    {
        if (perdio)
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
}

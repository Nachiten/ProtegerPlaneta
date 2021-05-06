using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    bool perdio = false;

    public GameObject obstaculoFacilPrefab;
    public GameObject obstaculoMedioPrefab;
    public GameObject obstaculoDificilPrefab;

    List<Obstaculo> obstaculos;

    void Start()
    {
        obstaculos = new List<Obstaculo>();

        // Obstaculo Facil
        obstaculos.Add(new Obstaculo(3f, 0.05f, 2.8f, obstaculoFacilPrefab, 0.7f));
        // Obstaculo Medio
        obstaculos.Add(new Obstaculo(6f, 0.03f, 1.6f, obstaculoMedioPrefab, 1.2f));
        // Obstaculo Dificil
        obstaculos.Add(new Obstaculo(9.5f, 0.02f, 0.9f, obstaculoDificilPrefab, 2.3f));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    float timePassed = 0f;
    public float speed = 1f;
    float intervaloAumento = 1.2f;

    bool perdio = false;

    public GameObject obstaclePrefab;

    int contador = 1;

    float aumentoVelocidad = 1.01f;

    void Update()
    {
        if (perdio)
            return;

        timePassed += Time.deltaTime * speed;

        if (timePassed >= intervaloAumento * contador) 
        {
            speed *= aumentoVelocidad;
            GameObject.Find("Jugador").GetComponent<MovimientoJugador>().aumentarSpeed(aumentoVelocidad + 0.01f);

            Debug.Log("[ObstacleSpawner] Aumentando velocidad");
            contador++;
            Instantiate(obstaclePrefab, new Vector3(0, 10, 0), Quaternion.identity);
            Debug.Log("[ObstacleSpawner] Instanciada prefab.");
        }
            
    }

    public void perderJuego()
    {
        perdio = true;
    }
}

using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    int contador = 1;
    float timePassed = 0f,
          puntoCentralSpeed = 0.7f,
          speed = 0.7f, 
          intervaloAumento = 2.2f, 
          aumentoVelocidad = 1.03f;

    int contador2 = 1;
    float timePassed2 = 0f,
          puntoCentralSpeed2 = 0.7f,
          speed2 = 0.7f,
          intervaloAumento2 = 1.7f,
          aumentoVelocidad2 = 1.03f;

    bool perdio = false;

    public GameObject obstaclePrefab;

    void Update()
    {
        if (perdio)
            return;

        ejecutarReloj(ref timePassed, ref speed, ref intervaloAumento, ref aumentoVelocidad, ref contador, ref puntoCentralSpeed);

        ejecutarReloj(ref timePassed2, ref speed2, ref intervaloAumento2, ref aumentoVelocidad2, ref contador2, ref puntoCentralSpeed2);
    }

    void ejecutarReloj(ref float timePassed, ref float speed, ref float intervaloAumento, ref float aumentoVelocidad, ref int contador, ref float puntoCentralSpeed) 
    {
        speed = Random.Range(puntoCentralSpeed - 0.4f, puntoCentralSpeed + 0.4f);

        timePassed += Time.deltaTime * speed;

        if (timePassed >= intervaloAumento * contador)
        {
            //int spawneoDos = Random.Range(0, 5);

            //if (spawneoDos == 0)
            //    Instantiate(obstaclePrefab, new Vector3(0, 10, 0), Quaternion.identity);

            Debug.Log("Tiempo: " + timePassed.ToString("F2"));

            puntoCentralSpeed *= aumentoVelocidad;
            GameObject.Find("Jugador").GetComponent<MovimientoJugador>().aumentarSpeed(aumentoVelocidad - 0.02f);

            //Debug.Log("[ObstacleSpawner] Aumentando velocidad");
            contador++;
            Instantiate(obstaclePrefab, new Vector3(0, 10, 0), Quaternion.identity);
            //Debug.Log("[ObstacleSpawner] Instanciada prefab.");
        }

    }

    public void perderJuego()
    {
        perdio = true;
    }
}

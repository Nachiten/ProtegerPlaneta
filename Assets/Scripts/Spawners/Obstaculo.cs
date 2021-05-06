using System.Collections.Generic;
using UnityEngine;

public class Obstaculo
{
    float timePassed;
    float puntoCentralSpeed;
    float speedReloj;
    float intervaloAparicion;
    float aumentoVelocidad;
    float speedObstaculo;

    int copiasPrefabs;

    float da�o;

    List<GameObject> obstaculosSpawneados;
    List<GameObject> obstaculosOcultos;

    GameObject obstaclePrefab;
    GameObject obstaculoParent;

    private readonly object lockListas = new object();

    public Obstaculo(float intervaloAparicion, float aumentoVelocidad, float speedObstaculo,  GameObject obstaclePrefab, float da�o) 
    {
        // Valores fijos
        this.timePassed = 0f;
        this.puntoCentralSpeed = 0.7f;
        this.speedReloj = 0.7f;
        this.copiasPrefabs = 15;

        obstaculosSpawneados = new List<GameObject>();
        obstaculosOcultos = new List<GameObject>();

        obstaculoParent = GameObject.Find("Obstaculos");

        this.speedObstaculo = speedObstaculo; 
        this.obstaclePrefab = obstaclePrefab;
        this.intervaloAparicion = intervaloAparicion;
        this.aumentoVelocidad = aumentoVelocidad;
        this.da�o = da�o;

        instanciarObjetos();
    }

    void instanciarObjetos() 
    {
        for (int i = 0; i < copiasPrefabs; i++)
        {
            GameObject obstaculoInstancia = Object.Instantiate(obstaclePrefab, new Vector3(0, 0, 0), Quaternion.identity);

            obstaculoInstancia.SetActive(false);
            obstaculoInstancia.transform.parent = obstaculoParent.transform;

            MovimientoObstaculo movimientoObstaculo = obstaculoInstancia.GetComponent<MovimientoObstaculo>();

            movimientoObstaculo.speed = speedObstaculo;
            movimientoObstaculo.da�o = da�o;

            string nombrePrefab = obstaculoInstancia.name.Substring(0, obstaculoInstancia.name.Length - 7);

            obstaculoInstancia.name = nombrePrefab + " " + i;

            obstaculosOcultos.Add(obstaculoInstancia);
        }
    }

    public void ejecutarReloj()
    {
        speedReloj = Random.Range(puntoCentralSpeed - 0.4f, puntoCentralSpeed + 0.4f);

        timePassed += Time.deltaTime * speedReloj;

        if (timePassed >= intervaloAparicion)
        {
            timePassed = 0;
            puntoCentralSpeed += aumentoVelocidad;

            mostrarObstaculo();
        }
    }

    void mostrarObstaculo()
    {
        lock (lockListas)
        {
            if (obstaculosOcultos.Count == 0)
            {
                Debug.LogError("[Obstaculo] No pude instanciar ningun objeto");
                return;
            }

            // Recupero objeto
            GameObject objetoInstanciado = obstaculosOcultos[0];

            objetoInstanciado.SetActive(true);

            // Quito de lista ocultos
            obstaculosOcultos.Remove(objetoInstanciado);
            // Agrego a lista mostrados
            obstaculosSpawneados.Add(objetoInstanciado);
        }
    }

    public void ocultarObstaculo(GameObject unObstaculo)
    {
        lock (lockListas)
        {
            if (!obstaculosSpawneados.Contains(unObstaculo))
                return;

            unObstaculo.SetActive(false);

            // Quito de lista spawneados
            obstaculosSpawneados.Remove(unObstaculo);

            // Agrego a lista ocultos
            obstaculosOcultos.Add(unObstaculo);
            
        }
    }
}

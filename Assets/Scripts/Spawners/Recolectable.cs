using UnityEngine;

public class Recolectable
{
    float intervaloAparicion;
    GameObject objetoRecolectable;

    float timePassed;
    bool estoyEnCooldown;
    float tiempoAparicion;

    float randomMin; float randomMax;

    public Recolectable(GameObject objetoRecolectable, float randomMin, float randomMax) 
    {
        this.randomMin = randomMin;
        this.randomMax = randomMax;
        this.objetoRecolectable = objetoRecolectable;

        this.objetoRecolectable.SetActive(false);

        fijarIntervaloAparicion();

        timePassed = 0;
        estoyEnCooldown = false;
        tiempoAparicion = 15f;
    }

    public void correrReloj()
    {
        timePassed += Time.deltaTime;

        if (estoyEnCooldown)
        {
            if (timePassed >= tiempoAparicion)
            {
                estoyEnCooldown = false;
                timePassed = 0;
                objetoRecolectable.SetActive(false);
            }
            return;
        }

        if (timePassed >= intervaloAparicion)
        {
            estoyEnCooldown = true;
            timePassed = 0;

            objetoRecolectable.SetActive(true);

            fijarIntervaloAparicion();
        }
    }

    void fijarIntervaloAparicion()
    {
        intervaloAparicion = Random.Range(randomMin, randomMax);
    }
}

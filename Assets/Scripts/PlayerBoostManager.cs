using UnityEngine;

public class PlayerBoostManager : MonoBehaviour
{
    float timePassedTamaño = 0;
    bool boostAgarradoTamaño = false;

    float timePassedRadio = 0;
    bool boostAgarradoRadio = false;

    float duracionBoost = 7f;
     
    public float radioActual = 2;

    /* -------------------------------------------------------------------------------- */

    void Update()
    {
        if (boostAgarradoTamaño)
        {
            timePassedTamaño += Time.deltaTime;

            if (timePassedTamaño >= duracionBoost)
                terminarBoostTamaño();
        }

        if (boostAgarradoRadio)
        {
            timePassedRadio += Time.deltaTime;

            if (timePassedRadio >= duracionBoost)
                terminarBoostRadio();
        }
    }

    /* -------------------------------------------------------------------------------- */

    public void modificarTamaño(float escalaX)
    {
        timePassedTamaño = 0;
        boostAgarradoTamaño = true;

        cambiarEscalaX(escalaX);
    }

    /* -------------------------------------------------------------------------------- */

    public void modificarRadio(float radio) 
    {
        timePassedRadio = 0;
        boostAgarradoRadio = true;

        fijarRadio(radio);
    }

    /* -------------------------------------------------------------------------------- */

    void terminarBoostTamaño()
    {
        boostAgarradoTamaño = false;

        cambiarEscalaX(1f);
    }

    /* -------------------------------------------------------------------------------- */

    void terminarBoostRadio() 
    {
        boostAgarradoRadio = false;

        fijarRadio(2);
    }

    /* -------------------------------------------------------------------------------- */

    void fijarRadio(float radio) 
    {
        radioActual = radio;

        Vector3 posicionActual = transform.localPosition;

        transform.localPosition = new Vector3(radio, posicionActual.y, posicionActual.z);
    }

    /* -------------------------------------------------------------------------------- */

    void cambiarEscalaX(float escalaX)
    {
        Vector3 tamañoActual = transform.localScale;

        transform.localScale = new Vector3(escalaX, tamañoActual.y, tamañoActual.z);
    }
}

using UnityEngine;

public class PlayerBoostManager : MonoBehaviour
{
    float tiempoPasadoTamaño = 0;
    bool boostAgarradoTamaño = false;

    float tiempoPasadoRadio = 0;
    bool boostAgarradoRadio = false;

    float duracionBoost = 7.5f;
    float duracionAnimacion = 0.25f;

    public float radioActual = 2;

    /* -------------------------------------------------------------------------------- */

    void Update()
    {
        if (boostAgarradoTamaño)
        {
            tiempoPasadoTamaño += Time.deltaTime;

            if (tiempoPasadoTamaño >= duracionBoost)
                terminarBoostTamaño();
        }

        if (boostAgarradoRadio)
        {
            tiempoPasadoRadio += Time.deltaTime;

            if (tiempoPasadoRadio >= duracionBoost)
                terminarBoostRadio();
        }
    }

    /* -------------------------------------------------------------------------------- */

    public void modificarTamaño(float escala)
    {
        tiempoPasadoTamaño = 0;
        boostAgarradoTamaño = true;

        cambiarTamaño(escala);
    }

    /* -------------------------------------------------------------------------------- */

    public void modificarRadio(float radio)
    {
        tiempoPasadoRadio = 0;
        boostAgarradoRadio = true;

        cambiarRadio(radio);
    }

    /* -------------------------------------------------------------------------------- */

    void terminarBoostTamaño()
    {
        boostAgarradoTamaño = false;

        cambiarTamaño(0.68f);
    }

    /* -------------------------------------------------------------------------------- */

    void terminarBoostRadio()
    {
        boostAgarradoRadio = false;

        cambiarRadio(2.65f);
    }

    /* -------------------------------------------------------------------------------- */

    void cambiarTamaño(float escala)
    {
        LeanTween.scale(gameObject, new Vector3(escala, escala, 1), duracionAnimacion);
    }

    /* -------------------------------------------------------------------------------- */

    void cambiarRadio(float radio)
    {
        radioActual = radio;

        Vector3 posicionActual = transform.localPosition;

        LeanTween.moveLocal(gameObject, new Vector3(radio, posicionActual.y, posicionActual.z), duracionAnimacion);
    }
}


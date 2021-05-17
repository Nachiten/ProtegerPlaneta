using UnityEngine;

public class PlayerBoostManager : MonoBehaviour
{
    float tiempoPasadoTama�o = 0;
    bool boostAgarradoTama�o = false;

    float tiempoPasadoRadio = 0;
    bool boostAgarradoRadio = false;

    float duracionBoost = 7.5f;
    float duracionAnimacion = 0.25f;

    public float radioActual = 2;

    /* -------------------------------------------------------------------------------- */

    void Update()
    {
        if (boostAgarradoTama�o)
        {
            tiempoPasadoTama�o += Time.deltaTime;

            if (tiempoPasadoTama�o >= duracionBoost)
                terminarBoostTama�o();
        }

        if (boostAgarradoRadio)
        {
            tiempoPasadoRadio += Time.deltaTime;

            if (tiempoPasadoRadio >= duracionBoost)
                terminarBoostRadio();
        }
    }

    /* -------------------------------------------------------------------------------- */

    public void modificarTama�o(float escala)
    {
        tiempoPasadoTama�o = 0;
        boostAgarradoTama�o = true;

        cambiarTama�o(escala);
    }

    /* -------------------------------------------------------------------------------- */

    public void modificarRadio(float radio)
    {
        tiempoPasadoRadio = 0;
        boostAgarradoRadio = true;

        cambiarRadio(radio);
    }

    /* -------------------------------------------------------------------------------- */

    void terminarBoostTama�o()
    {
        boostAgarradoTama�o = false;

        cambiarTama�o(0.68f);
    }

    /* -------------------------------------------------------------------------------- */

    void terminarBoostRadio()
    {
        boostAgarradoRadio = false;

        cambiarRadio(2.65f);
    }

    /* -------------------------------------------------------------------------------- */

    void cambiarTama�o(float escala)
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


using UnityEngine;

public class PlayerBoostManager : MonoBehaviour
{
    float timePassedTama�o = 0;
    bool boostAgarradoTama�o = false;

    float timePassedRadio = 0;
    bool boostAgarradoRadio = false;

    float duracionBoost = 7f;
     
    public float radioActual = 2;

    /* -------------------------------------------------------------------------------- */

    void Update()
    {
        if (boostAgarradoTama�o)
        {
            timePassedTama�o += Time.deltaTime;

            if (timePassedTama�o >= duracionBoost)
                terminarBoostTama�o();
        }

        if (boostAgarradoRadio)
        {
            timePassedRadio += Time.deltaTime;

            if (timePassedRadio >= duracionBoost)
                terminarBoostRadio();
        }
    }

    /* -------------------------------------------------------------------------------- */

    public void modificarTama�o(float escalaX)
    {
        timePassedTama�o = 0;
        boostAgarradoTama�o = true;

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

    void terminarBoostTama�o()
    {
        boostAgarradoTama�o = false;

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
        Vector3 tama�oActual = transform.localScale;

        transform.localScale = new Vector3(escalaX, tama�oActual.y, tama�oActual.z);
    }
}

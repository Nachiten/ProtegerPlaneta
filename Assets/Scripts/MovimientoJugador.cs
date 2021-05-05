using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    bool perdio = false;
    float speedRotacion = 250f;

    void Update()
    {
        if (perdio)
            return;

        float rotacionAplicada = 0;

        // Izquierda (+)
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || clickeoIzquierdaPantalla()) 
        {
            rotacionAplicada = speedRotacion;
        }

        // Derecha (-)
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || clickeoDerechaPantalla())
        {
            rotacionAplicada = -speedRotacion;
        }
        
        // Aplico rotacion correspondiente
        transform.Rotate(new Vector3(0, 0, rotacionAplicada * Time.deltaTime));

        ejecutarReloj();
    }

    float speedReloj = 0.7f;
    float timePassed = 0;
    float intervaloAparicion = 4f;
    float aumentoVelocidad = 0.05f;

    void ejecutarReloj() 
    {
        timePassed += Time.deltaTime * speedReloj;

        if (timePassed >= intervaloAparicion)
        {
            timePassed = 0;

            speedReloj += aumentoVelocidad;
            
            aumentarSpeed(aumentoVelocidad);
        }

    }

    bool clickeoDerechaPantalla() 
    {
        if (!Input.GetMouseButton(0))
            return false;

        return Input.mousePosition.x >= Screen.width / 2;
    }

    bool clickeoIzquierdaPantalla() 
    {
        if (!Input.GetMouseButton(0))
            return false;

        return Input.mousePosition.x <= Screen.width / 2;
    }

    public void aumentarSpeed(float aumento) 
    {
        speedRotacion += aumento;
    }

    public void perderJuego() 
    {
        perdio = true;
    }
}
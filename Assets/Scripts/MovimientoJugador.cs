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

    #region CalcularInterseccion

    public Vector3[] IntersectionPoint(Vector3 p1, Vector3 p2, Vector3 center, float radius)
    {
        Vector3 dp = new Vector3();
        Vector3[] sect;
        float a, b, c;
        float bb4ac;
        float mu1;
        float mu2;

        //  get the distance between X and Z on the segment
        dp.x = p2.x - p1.x;
        dp.z = p2.z - p1.z;
        //   I don't get the math here
        a = dp.x * dp.x + dp.z * dp.z;
        b = 2 * (dp.x * (p1.x - center.x) + dp.z * (p1.z - center.z));
        c = center.x * center.x + center.z * center.z;
        c += p1.x * p1.x + p1.z * p1.z;
        c -= 2 * (center.x * p1.x + center.z * p1.z);
        c -= radius * radius;
        bb4ac = b * b - 4 * a * c;
        if (Mathf.Abs(a) < float.Epsilon || bb4ac < 0)
        {
            //  line does not intersect
            return new Vector3[] { Vector3.zero, Vector3.zero };
        }
        mu1 = (-b + Mathf.Sqrt(bb4ac)) / (2 * a);
        mu2 = (-b - Mathf.Sqrt(bb4ac)) / (2 * a);
        sect = new Vector3[2];
        sect[0] = new Vector3(p1.x + mu1 * (p2.x - p1.x), 0, p1.z + mu1 * (p2.z - p1.z));
        sect[1] = new Vector3(p1.x + mu2 * (p2.x - p1.x), 0, p1.z + mu2 * (p2.z - p1.z));

        return sect;
    }

    #endregion
}
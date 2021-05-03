using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    GameObject planeta;

    public float radioCirculo = 4;

    void Start()
    {
        planeta = GameObject.Find("Planeta");    
    }

    void Update()
    {
        // Obtengo posicion del mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 posicionPlaneta = planeta.transform.position;

        // Invierto posiciones y,z
        Vector3 posicionPlanetaCambiada = new Vector3(posicionPlaneta.x, posicionPlaneta.z, posicionPlaneta.y);
        Vector3 mousePosCambiada = new Vector3(mousePos.x, mousePos.z, mousePos.y);

        // Calculo la posicion donde debe estar el jugador
        Vector3[] posicionesPosiblesJugador = IntersectionPoint(mousePosCambiada, posicionPlanetaCambiada, posicionPlanetaCambiada, radioCirculo);

        // Invierto posiciones y,z nuevamente
        transform.position = new Vector3(posicionesPosiblesJugador[1].x, posicionesPosiblesJugador[1].z, posicionesPosiblesJugador[1].y);
        
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
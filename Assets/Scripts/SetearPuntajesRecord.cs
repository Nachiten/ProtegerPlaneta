using UnityEngine;
using TMPro;

public class SetearPuntajesRecord : MonoBehaviour
{
    void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            string nombrePlayerPref = "Points_" + i;

            int puntajeActual = PlayerPrefs.GetInt(nombrePlayerPref);

            TMP_Text textoRecord = GameObject.Find("TextoRecord" + i).GetComponent<TMP_Text>();

            //Debug.Log("[SetearPuntajesRecord] textoRecord: " + textoRecord);
            //Debug.Log("[SetearPuntajesRecord] puntajeActual: " + puntajeActual);

            textoRecord.text = puntajeActual.ToString();
        }
    }
}

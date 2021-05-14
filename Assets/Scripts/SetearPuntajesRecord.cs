using UnityEngine;
using TMPro;

public class SetearPuntajesRecord : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("[SetearPuntajesRecord] OnEnable");
        setearPuntajesRecord();
    }
    
    void setearPuntajesRecord() 
    {
        Debug.Log("[SetearPuntajesRecord] Seteando puntajes record...");

        for (int i = 0; i < 4; i++)
        {
            string nombrePlayerPref = "Points_" + i;

            int puntajeActual = PlayerPrefs.GetInt(nombrePlayerPref);

            //Debug.Log("[SetearPuntajesRecord] puntajeActual: " + puntajeActual + " | i: " + i);

            //GameObject objetoTexto = GameObject.Find("TextoRecord" + i);
            //objetoTexto.SetActive(true);

            TMP_Text textoRecord = GameObject.Find("TextoRecord" + i).GetComponent<TMP_Text>();

            //Debug.Log("[SetearPuntajesRecord] textoRecord: " + textoRecord);

            textoRecord.text = puntajeActual.ToString();
        }
    }
}

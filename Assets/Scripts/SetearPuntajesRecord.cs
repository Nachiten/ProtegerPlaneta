using UnityEngine;
using TMPro;

public class SetearPuntajesRecord : MonoBehaviour
{
    private void OnEnable()
    {
        setearPuntajesRecord();
    }

    /* -------------------------------------------------------------------------------- */

    void setearPuntajesRecord() 
    {
        Debug.Log("[SetearPuntajesRecord] Seteando puntajes record...");

        for (int i = 0; i < 4; i++)
        {
            string nombrePlayerPref = "Points_" + i;

            int puntajeActual = PlayerPrefs.GetInt(nombrePlayerPref);

            TMP_Text textoRecord = GameObject.Find("TextoRecord" + i).GetComponent<TMP_Text>();

            textoRecord.text = puntajeActual.ToString();
        }
    }
}

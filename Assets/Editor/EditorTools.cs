using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EditorTools : EditorWindow
{
    // Mostrar Ventana
    [MenuItem("Window/[EditorTools]")]
    public static void ShowWindow()
    {
        GetWindow<EditorTools>("EditorTools");
    }

    // --------------------------------------------------------------------------------


    int cantidadEscenas;

    private void OnEnable()
    {
        cantidadEscenas = SceneManager.sceneCountInBuildSettings;
    }

    public string stringInputPuntaje = "Inserte puntaje: ";

    int modoJuego = -1;
    int puntaje = -1;

    // Codigo de la Vetana
    void OnGUI()
    {
        if (!Application.isPlaying)
        {
            EditorGUILayout.LabelField("----------------------------------------------------------------------");
            EditorGUILayout.LabelField("------ Debes comenzar a jugar para ver las opciones de este menu.  ------");
            EditorGUILayout.LabelField("----------------------------------------------------------------------");
            return;
        }

        // --------------------------------------------------------------------------------

        EditorGUILayout.LabelField("Viajar hacia escena:");

        mostrarMenuViajarAEscena();

        // --------------------------------------------------------------------------------

        EditorGUILayout.LabelField("Perder Nivel Actual:");

        if (GUILayout.Button("Perder nivel actual"))
        {
            int indexActual = SceneManager.GetActiveScene().buildIndex;

            if (indexActual == 0)
            {
                Debug.LogError("[EditorTools] No hay ningun nivel que perder.");
            }
            else
            {
                Debug.Log("[EditorTools] Perdiendo nivel...");

                GameObject.Find("GameManager").GetComponent<GameManager>().perderJuego();
            }
        }

        // --------------------------------------------------------------------------------

        EditorGUILayout.LabelField("Fijar puntos en modo especifico:");

        modoJuego = EditorGUILayout.IntField("Modo Juego:", modoJuego);
        puntaje = EditorGUILayout.IntField("Puntaje:", puntaje);

        if (GUILayout.Button("Fijar Puntaje en Modo"))
        {
            if (!(modoJuego >= 0 && modoJuego <= 3)) 
            {
                Debug.LogError("[EditorTools] El modo juego debe ser un numero entre 0 y 3.");
                return;
            }

            if (puntaje < 0) 
            {
                Debug.LogError("[EditorTools] El puntaje debe ser positivo.");
                return;
            }

            string nombrePlayerPref = "Points_" + modoJuego;

            Debug.Log("[EditorTools] puntaje: " + puntaje);
            Debug.Log("[EditorTools] nombrePlayerPref: " + nombrePlayerPref);

            // Seteo una nueva player pref
            PlayerPrefs.SetInt(nombrePlayerPref, puntaje);

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                GameObject.Find("GameManager").GetComponent<LevelLoader>().cargarNivel(0);
            }
        }

        // --------------------------------------------------------------------------------

        EditorGUILayout.LabelField("Borrar Todas las Keys:");

        if (GUILayout.Button("BORRAR TODO"))
        {
            PlayerPrefs.DeleteAll();

            if (SceneManager.GetActiveScene().buildIndex == 12)
            {
                GameObject.Find("GameManager").GetComponent<LevelLoader>().cargarNivel(12);
            }
        }
    }

    void mostrarMenuViajarAEscena() 
    {
        for (int i = 0; i < cantidadEscenas; i++)
        {
            string nombreEscena = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            if (GUILayout.Button("Ir a escena: [" + nombreEscena + "]"))
            {
                GameObject.Find("GameManager").GetComponent<LevelLoader>().cargarNivel(i);
            }
        }
    }

    //void mostrarMenuGanarNivel(bool ganaDirecto) 
    //{
    //    for (int i = 0; i < cantidadEscenas; i++)
    //    {
    //        if (i == 0 || i == 12)
    //            continue;

    //        string nombreEscena = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

    //        if (GUILayout.Button("Ganar Nivel: [" + nombreEscena + "]") || ganaDirecto)
    //        {
    //            PlayerPrefs.SetString(i.ToString(), "Ganado");
    //            PlayerPrefs.SetFloat("Time_" + i, 25000f);
    //            PlayerPrefs.SetInt("Movements_" + i, 137);

    //            if (SceneManager.GetActiveScene().buildIndex == 12 && !ganaDirecto) 
    //            {
    //                GameObject.Find("GameManager").GetComponent<LevelLoader>().cargarNivel(12);
    //            }
    //        }
    //    }

    //    if (SceneManager.GetActiveScene().buildIndex == 12 && ganaDirecto)
    //    {
    //        GameObject.Find("GameManager").GetComponent<LevelLoader>().cargarNivel(12);
    //    }
    //}
}


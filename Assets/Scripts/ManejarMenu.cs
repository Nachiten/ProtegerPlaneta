using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ManejarMenu : MonoBehaviour
{
    #region Variables

    // Flags varios
    bool menuActivo = false, opcionesActivas = false, creditosActivos = false, puntajesRecordActivos = false;

    // Flag de ya asigne las variables
    static bool variablesAsignadas = false;

    // GameObjects
    static GameObject menu, menuCreditos, panelMenu, menuOpciones, menuPuntajesRecord;

    // Manager de las animaciones
    static LeanTweenManager tweenManager;

    static TMP_Text textoBotonComenzar;

    // Index de escena actual
    int index;

    string continuarString = "CONTINUAR", comenzarString = "COMENZAR";

    #endregion

    /* -------------------------------------------------------------------------------- */

    #region FuncionStart

    private void Awake()
    {
        if (!variablesAsignadas)
        {
            menu = GameObject.Find("Menu");
            panelMenu = GameObject.Find("PanelMenu");
            menuOpciones = GameObject.Find("MenuOpciones");
            menuCreditos = GameObject.Find("MenuCreditos");
            menuPuntajesRecord = GameObject.Find("MenuPuntajesRecord");

            textoBotonComenzar = GameObject.Find("TextoBotonComenzar").GetComponent<TMP_Text>();

            tweenManager = GameObject.Find("Canvas Menu").GetComponent<LeanTweenManager>();

            variablesAsignadas = true;
        }
    }

    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;

        // No estoy en el menu principal
        if (index != 0)
        {
            // Oculto todo de una patada pq se esta mostrando la pantalla de carga
            menu.SetActive(false);
            panelMenu.SetActive(false);
            textoBotonComenzar.text = continuarString;
        }
        // Estoy en el menu principal
        else
        {
            menu.SetActive(true);
            menuActivo = true;
            textoBotonComenzar.text = comenzarString;
        }

        menuOpciones.SetActive(false);
        menuCreditos.SetActive(false);
        menuPuntajesRecord.SetActive(false);
    }

    #endregion

    /* -------------------------------------------------------------------------------- */

    #region FuncionUpdate

    void Update()
    {
        index = SceneManager.GetActiveScene().buildIndex;

        if (index == 0) return;

        bool animacionEnEjecucion = GameObject.Find("Canvas Menu").GetComponent<LeanTweenManager>().animacionEnEjecucion;

        if (Input.GetKeyDown("escape") && !animacionEnEjecucion)
            manejarMenu();
    }

    #endregion

    /* -------------------------------------------------------------------------------- */

    public void manejarMenu() 
    {
        menuActivo = !menuActivo;

        if (menuActivo)
        {
            menu.SetActive(true);
            tweenManager.abrirMenu();
        }
        else 
            tweenManager.cerrarMenu();
        

        if (opcionesActivas) 
        {
            tweenManager.cerrarOpciones();
            opcionesActivas = false;
        }
    }

    /* -------------------------------------------------------------------------------- */

    public void manejarOpciones()
    {
        opcionesActivas = !opcionesActivas;

        if (opcionesActivas) 
            tweenManager.abrirOpciones();
        
        else
            tweenManager.cerrarOpciones();
    }

    /* -------------------------------------------------------------------------------- */
    public void manejarCreditos() 
    {
        creditosActivos = !creditosActivos;

        if (creditosActivos)
            tweenManager.abrirCreditos();
        else
            tweenManager.cerrarCreditos();
    }

    public void manejarPuntajesRecord()
    {
        puntajesRecordActivos = !puntajesRecordActivos;

        if (puntajesRecordActivos)
            tweenManager.abrirPuntajesRecord();
        else
            tweenManager.cerrarPuntajesRecord();
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeanTweenManager : MonoBehaviour
{
    #region Variables

    float tiempoAnimacionBotonesMenu = 0.2f, tiempoAnimacionPanelMenu = 0.15f, tiempoAnimacionMenus = 0.5f;

    List<GameObject> botones;

    static GameObject menu, menuPanel, menuOpciones, menuCreditos, menuPuntajesRecord, botonesInicio,
                      botonComenzar, botonOpciones, botonSalir, botonVolverInicio, botonBorrarProgreso, botonCreditos;

    public bool animacionEnEjecucion = false;

    static int indexActual = -1;
    static bool variablesSeteadas = false;

    #endregion

    /* -------------------------------------------------------------------------------- */

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /* -------------------------------------------------------------------------------- */

    // Setup que se hace una sola vez
    void Awake()
    {
        if (variablesSeteadas)
            return;

        // Objetos
        menu = GameObject.Find("Menu");
        menuPanel = GameObject.Find("PanelMenu");
        menuOpciones = GameObject.Find("MenuOpciones");
        menuCreditos = GameObject.Find("MenuCreditos");
        menuPuntajesRecord = GameObject.Find("MenuPuntajesRecord");

        // Botones
        botonSalir = GameObject.Find("Salir");
        botonComenzar = GameObject.Find("Comenzar");
        botonOpciones = GameObject.Find("Opciones");

        // Botonces condicionales
        botonVolverInicio = GameObject.Find("VolverAInicio");

        variablesSeteadas = true;
    }

    /* -------------------------------------------------------------------------------- */

    // Se llama cuando una nueva escena se carga
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        setupInicial();
    }

    /* -------------------------------------------------------------------------------- */

    // Setup que se hace en cada nueva escena cargada
    void setupInicial() 
    {
        indexActual = SceneManager.GetActiveScene().buildIndex;

        botones = new List<GameObject>();

        botones.Add(botonSalir);
        botones.Add(botonComenzar);
        botones.Add(botonOpciones);

        if (indexActual == 0)
        {
            botonCreditos = GameObject.Find("Creditos");
            botonesInicio = GameObject.Find("Botones Inicio");
            botonBorrarProgreso = GameObject.Find("BorrarProgreso");

            botones.Add(botonBorrarProgreso);
            botones.Add(botonCreditos);
            botonVolverInicio.SetActive(false);
        }
        else
        {
            botones.Add(botonVolverInicio);
        }
    }

    #region AnimacionAbrirMenu

    /* --------------------------------------------------------------------------------- */
    // ----------------------------- ANIMACION ARBRIR MENU ----------------------------- //
    /* --------------------------------------------------------------------------------- */

    public void abrirMenu()
    {
        menuPanel.SetActive(false);

        foreach (GameObject boton in botones) 
        {
            boton.SetActive(false);
        }

        animacionEnEjecucion = true;

        // Posicion inicial
        LeanTween.scale(menuPanel, new Vector3(0, 0, 1), 0f).setOnComplete(abrirPanel);
    }

    void abrirPanel()
    {
        menuPanel.SetActive(true);
        LeanTween.scale(menuPanel, new Vector3(1, 1, 1), tiempoAnimacionPanelMenu).setOnComplete(abrirBotones);
    }

    void abrirBotones()
    {
        int cantidadBotones = botones.Count;

        for (int i = 0; i < cantidadBotones; i++)
        {
            GameObject boton = botones[i];

            bool terminarAnimacion = i == cantidadBotones - 1;

            // Posiciones iniciales
            LeanTween.scale(boton, new Vector3(0, 0.2f, 1), 0f).setOnComplete(_ => abrirBotonEnX(boton, terminarAnimacion));
        }
    }

    void abrirBotonEnX(GameObject boton, bool terminarAnimacion)
    {
        boton.SetActive(true);
        LeanTween.scaleX(boton, 2.3f, tiempoAnimacionBotonesMenu).setOnComplete(_ => abrirBotonEnY(boton, terminarAnimacion));
    }

    void abrirBotonEnY(GameObject unBoton, bool terminarAnimacion)
    {
        if (terminarAnimacion)
            LeanTween.scaleY(unBoton, 3.1f, tiempoAnimacionBotonesMenu).setOnComplete(terminarAnimacionAbrir);
        else
            LeanTween.scaleY(unBoton, 3.1f, tiempoAnimacionBotonesMenu);
    }

    void terminarAnimacionAbrir()
    {
        animacionEnEjecucion = false;
    }
    
    #endregion

    #region AnimacionCerrarMenu

    /* --------------------------------------------------------------------------------- */
    // ----------------------------- ANIMACION CERRAR MENU ----------------------------- // 
    /* --------------------------------------------------------------------------------- */

    public void cerrarMenu()
    {
        animacionEnEjecucion = true;

        int cantidadBotones = botones.Count;

        for (int i = 0; i < cantidadBotones; i++)
        {
            GameObject botonActual = botones[i];

            bool cerrarMenu = i == cantidadBotones - 1;

            // Posiciones iniciales
            LeanTween.scale(botonActual, new Vector3(2.3f, 3.1f, 1), 0f).setOnComplete(_ => cerrarBotonEnY(botonActual, cerrarMenu));
        }

    }

    void cerrarBotonEnY(GameObject boton, bool cerrarMenu) {
        LeanTween.scaleY(boton, 0.2f, tiempoAnimacionBotonesMenu).setOnComplete(_ => cerrarBotonEnX(boton, cerrarMenu));
    }

    void cerrarBotonEnX(GameObject unBoton, bool cerrarMenu)
    {
        if (cerrarMenu)
            LeanTween.scaleX(unBoton, 0f, tiempoAnimacionBotonesMenu).setOnComplete(cerrarPanel);
        else
            LeanTween.scaleX(unBoton, 0f, tiempoAnimacionBotonesMenu);
    }

    void cerrarPanel()
    {
        // Posicion inicial
        LeanTween.scale(menuPanel, new Vector3(1, 1, 1), 0f);

        LeanTween.scale(menuPanel, new Vector3(0, 0, 1), tiempoAnimacionPanelMenu).setOnComplete(terminarAnimacionCerrar);
    }

    void terminarAnimacionCerrar()
    {
        animacionEnEjecucion = false;
        menu.SetActive(false);
    }

    #endregion

    #region AnimacionAbrirOpcionesMenu

    /* ----------------------------------------------------------------------------------------- */
    // ----------------------------- ANIMACION ABRIR OPCIONES MENU ----------------------------- // 
    /* ----------------------------------------------------------------------------------------- */

    float posicionAfuera = 1920;

    public void abrirOpciones()
    {
        animacionEnEjecucion = true;

        indexActual = SceneManager.GetActiveScene().buildIndex;

        if (indexActual == 0) 
        {
            LeanTween.moveLocalX(botonesInicio, 0f, 0f).setOnComplete(_ => quitarBotonesInicio(posicionAfuera));
        }

        // Posicion Inicial
        LeanTween.moveLocalX(menu, 0f, 0f).setOnComplete(_ => quitarMenu(posicionAfuera));
        LeanTween.moveLocalX(menuOpciones, -posicionAfuera, 0f).setOnComplete(ponerOpciones);
    }

    void quitarMenu(float posicion)
    {
        LeanTween.moveLocalX(menu, posicion, tiempoAnimacionMenus).setOnComplete(ocultarMenu);
    }

    void ocultarMenu()
    {
        menu.SetActive(false);
        animacionEnEjecucion = false;
    }

    void ponerOpciones()
    {
        menuOpciones.SetActive(true);
        LeanTween.moveLocalX(menuOpciones, 0f, tiempoAnimacionMenus);
    }

    void quitarBotonesInicio(float posicion)
    {
        LeanTween.moveLocalX(botonesInicio, posicion, tiempoAnimacionMenus).setOnComplete(ocultarBotonesInicio);
    }

    void ocultarBotonesInicio()
    {
        botonesInicio.SetActive(false);
        //Debug.Log("[LeanTweenManager] Termino Animacion [AbrirOpciones]");
    }

    #endregion

    #region AnimacionCerrarOpcionesMenu

    /* ------------------------------------------------------------------------------------------ */
    // ----------------------------- ANIMACION CERRAR OPCIONES MENU ----------------------------- // 
    /* ------------------------------------------------------------------------------------------ */

    public void cerrarOpciones() 
    {
        animacionEnEjecucion = true;

        indexActual = SceneManager.GetActiveScene().buildIndex;

        if (indexActual == 0)
        {
            LeanTween.moveLocalX(botonesInicio, posicionAfuera, 0f).setOnComplete(ponerBotonesInicio);
        }

        // Posicion Inicial
        LeanTween.moveLocalX(menu, posicionAfuera, 0f).setOnComplete(ponerMenu);
        LeanTween.moveLocalX(menuOpciones, 0, 0f).setOnComplete(quitarOpciones);
    }

    void ponerBotonesInicio()
    {
        botonesInicio.SetActive(true);
        LeanTween.moveLocalX(botonesInicio, 0, tiempoAnimacionMenus);
    }

    void ponerMenu() 
    {
        menu.SetActive(true);
        LeanTween.moveLocalX(menu, 0, tiempoAnimacionMenus);
    }

    void quitarOpciones() 
    {
        LeanTween.moveLocalX(menuOpciones, -posicionAfuera, tiempoAnimacionMenus).setOnComplete(ocultarOpciones);
    }

    void ocultarOpciones() 
    {
        menuOpciones.SetActive(false);
        animacionEnEjecucion = false;
    }

    #endregion

    #region AnimacionAbrirCreditos

    /* ------------------------------------------------------------------------------------ */
    // ----------------------------- ANIMACION ABRIR CREDITOS ----------------------------- // 
    /* ------------------------------------------------------------------------------------ */

    public void abrirCreditos() 
    {
        animacionEnEjecucion = true;

        indexActual = SceneManager.GetActiveScene().buildIndex;

        if (indexActual == 0)
        {
            LeanTween.moveLocalX(botonesInicio, 0f, 0f).setOnComplete(_ => quitarBotonesInicio(-posicionAfuera));
        }

        // Posicion Inicial
        LeanTween.moveLocalX(menu, 0f, 0f).setOnComplete(_ => quitarMenu(-posicionAfuera));
        LeanTween.moveLocalX(menuCreditos, posicionAfuera, 0f).setOnComplete(ponerCreditos);
    }

    void ponerCreditos() 
    {
        menuCreditos.SetActive(true);
        LeanTween.moveLocalX(menuCreditos, 0f, tiempoAnimacionMenus);
    }

    #endregion

    #region AnimacionCerrarCreditos

    /* ------------------------------------------------------------------------------------- */
    // ----------------------------- ANIMACION CERRAR CREDITOS ----------------------------- // 
    /* ------------------------------------------------------------------------------------- */

    public void cerrarCreditos() 
    {
        animacionEnEjecucion = true;

        indexActual = SceneManager.GetActiveScene().buildIndex;

        if (indexActual == 0)
        {
            LeanTween.moveLocalX(botonesInicio, -posicionAfuera, 0f).setOnComplete(ponerBotonesInicio);
        }

        // Posicion Inicial
        LeanTween.moveLocalX(menu, -posicionAfuera, 0f).setOnComplete(ponerMenu);
        LeanTween.moveLocalX(menuOpciones, 0, 0f).setOnComplete(quitarCreditos);
    }

    void quitarCreditos()
    {
        LeanTween.moveLocalX(menuCreditos, posicionAfuera, tiempoAnimacionMenus).setOnComplete(ocultarCreditos);
    }

    void ocultarCreditos()
    {
        menuCreditos.SetActive(false);
        animacionEnEjecucion = false;
    }

    #endregion

    #region AnimacionAbrirPuntajesRecord

    /* ------------------------------------------------------------------------------------------- */
    // ----------------------------- ANIMACION ABRIR PUNTAJES RECORD ----------------------------- // 
    /* ------------------------------------------------------------------------------------------- */

    public void abrirPuntajesRecord()
    {
        animacionEnEjecucion = true;

        indexActual = SceneManager.GetActiveScene().buildIndex;

        if (indexActual == 0)
        {
            LeanTween.moveLocalX(botonesInicio, 0f, 0f).setOnComplete(_ => quitarBotonesInicio(posicionAfuera));
        }

        // Posicion Inicial
        LeanTween.moveLocalX(menu, 0f, 0f).setOnComplete(_ => quitarMenu(posicionAfuera));
        LeanTween.moveLocalX(menuPuntajesRecord, -posicionAfuera, 0f).setOnComplete(ponerPuntajesRecord);
    }

    void ponerPuntajesRecord()
    {
        menuPuntajesRecord.SetActive(true);
        LeanTween.moveLocalX(menuPuntajesRecord, 0f, tiempoAnimacionMenus);
    }

    #endregion

    #region AnimacionCerrarPuntajesRecord

    /* -------------------------------------------------------------------------------------------- */
    // ----------------------------- ANIMACION CERRAR PUNTAJES RECORD ----------------------------- // 
    /* -------------------------------------------------------------------------------------------- */

    public void cerrarPuntajesRecord()
    {
        animacionEnEjecucion = true;

        indexActual = SceneManager.GetActiveScene().buildIndex;

        if (indexActual == 0)
        {
            LeanTween.moveLocalX(botonesInicio, posicionAfuera, 0f).setOnComplete(ponerBotonesInicio);
        }

        // Posicion Inicial
        LeanTween.moveLocalX(menu, posicionAfuera, 0f).setOnComplete(ponerMenu);
        LeanTween.moveLocalX(menuPuntajesRecord, 0, 0f).setOnComplete(quitarPuntajesRecord);
    }

    void quitarPuntajesRecord()
    {
        LeanTween.moveLocalX(menuPuntajesRecord, -posicionAfuera, tiempoAnimacionMenus).setOnComplete(ocultarPuntajesRecord);
    }

    void ocultarPuntajesRecord()
    {
        menuPuntajesRecord.SetActive(false);
        animacionEnEjecucion = false;
    }

    #endregion
}
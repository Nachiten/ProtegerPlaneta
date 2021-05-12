﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public int nivelACargar = 1;

    static PopUpsMenu codigoPopUpsMenu;
    static LevelLoader codigoLevelLoader;
    static ManejarMenu codigoManejarMenu;
    static SoundManager codigoSoundManager;

    //static int indexActual = -1;

    static bool variablesSeteadas = false;
    private static readonly object setearVariablesLock = new object();

    //bool yaCargada = false;
    //private static readonly object inicializacionLock = new object();

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

    // Se llama cuando una nueva escena se carga
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        setupInicial();

        //lock (inicializacionLock) 
        //{
        //    // Si es la misma escena que antes
        //    if (indexActual == scene.buildIndex)
        //    {
        //        // Si es la misma escena recargada (la primera vez)
        //        if (!yaCargada)
        //            setupInicial();
        //        // Si es la segunda o mas veces seguidas que quiere cargar
        //        else
        //            return;
        //    }
        //    // Si es una escena nueva, inicializo
        //    else
        //        setupInicial();
        //}
    }

    /* -------------------------------------------------------------------------------- */

    // Setup que se hace por unica vez
    void Awake()
    {
        lock (setearVariablesLock) 
        {
            if (variablesSeteadas)
                return;

            codigoSoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

            codigoPopUpsMenu = GameObject.Find("Pop Up").GetComponent<PopUpsMenu>();

            variablesSeteadas = true;
        }
    }

    // Setup que se hace en cada nueva escena cargada
    void setupInicial() 
    {
        //yaCargada = true;

        Debug.Log("[Buttons] SetupInicial");

        GameObject objetoGameManager = GameObject.Find("GameManager");

        codigoManejarMenu = objetoGameManager.GetComponent<ManejarMenu>();
        codigoLevelLoader = objetoGameManager.GetComponent<LevelLoader>();
    }

    #region Botones

    /* --------------------------------------------------------------------------------- */
    /* ------------------------------------ BOTONES ------------------------------------ */
    /* --------------------------------------------------------------------------------- */

    public void botonComenzar()
    {
        reproducirSonidoClickBoton();

        if (SceneManager.GetActiveScene().buildIndex == 0) 
            loadLevel(nivelACargar);
        else
            codigoManejarMenu.manejarMenu();
    }

    public void botonOpciones()
    {
        reproducirSonidoClickBoton();

        codigoManejarMenu.manejarOpciones();
    }

    public void botonSalir()
    {
        reproducirSonidoClickBoton();

        codigoPopUpsMenu.abrirPopUp(3);
    }

    public void botonVolverAInicio()
    {
        reproducirSonidoClickBoton();

        loadLevel(0);
    }

    public void botonBorrarProgreso()
    {
        reproducirSonidoClickBoton();

        codigoPopUpsMenu.abrirPopUp(0);
    }

    public void botonCreditos() 
    {
        reproducirSonidoClickBoton();

        codigoManejarMenu.manejarCreditos();
    }

    #endregion

    /* ------------------------------------------------------------------------------------ */
    /* ------------------------------------ AUXILIARES ------------------------------------ */
    /* ------------------------------------------------------------------------------------ */

    public void Salir() { codigoLevelLoader.salir(); }

    /* -------------------------------------------------------------------------------- */

    public void loadLevel(int index) { codigoLevelLoader.cargarNivel(index); }

    /* -------------------------------------------------------------------------------- */

    void reproducirSonidoClickBoton() 
    {
        codigoSoundManager.reproducirSonido(1);
    }

    /* -------------------------------------------------------------------------------- */
}
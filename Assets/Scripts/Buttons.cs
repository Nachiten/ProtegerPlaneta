using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public int nivelACargar = 1;

    static PopUpsMenu codigoPopUpsMenu;
    static LevelLoader codigoLevelLoader;
    static ManejarMenu codigoManejarMenu;
    static SoundManager codigoSoundManager;

    static bool variablesSeteadas = false;

    private static readonly object setearVariablesLock = new object();

    /* -------------------------------------------------------------------------------- */

    void Awake()
    {
        lock (setearVariablesLock) 
        { 
            if (variablesSeteadas)
                return;

            GameObject objetoGameManager = GameObject.Find("GameManager");

            codigoManejarMenu = objetoGameManager.GetComponent<ManejarMenu>();
            codigoLevelLoader = objetoGameManager.GetComponent<LevelLoader>();
            codigoSoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

            codigoPopUpsMenu = GameObject.Find("Pop Up").GetComponent<PopUpsMenu>();

            variablesSeteadas = true;
        }
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
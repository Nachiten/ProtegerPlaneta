using UnityEngine;
using UnityEngine.SceneManagement;

public class FijarCamaraCanvas : MonoBehaviour
{
    Canvas canvas;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Setup inicial
    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    /* -------------------------------------------------------------------------------- */

    // Se llama cuando una nueva escena se carga
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}

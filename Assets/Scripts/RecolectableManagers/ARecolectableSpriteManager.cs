using UnityEngine;

public abstract class ARecolectableSpriteManager : MonoBehaviour
{
    protected PlayerBoostManager playerBoostManager;

    private void Awake()
    {
        playerBoostManager = GameObject.Find("JugadorSprite").GetComponent<PlayerBoostManager>();
    }

    private void OnEnable()
    {
        float radioActual = playerBoostManager.radioActual;

        // Aplico radio actual del jugador
        fijarRadio(radioActual);
    }

    void fijarRadio(float radio)
    {
        Vector3 posicionActual = transform.localPosition;

        transform.localPosition = new Vector3(posicionActual.x, radio, posicionActual.z);
    }
}

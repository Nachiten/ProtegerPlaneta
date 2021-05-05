using UnityEngine;

public class RecolectableBoostVida : ARecolectableSpriteManager
{
    GameManager codigoGameManager;

    void Awake() 
    {
        codigoGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Jugador"))
            return;

        codigoGameManager.aumentarVida(2.2f);

        transform.parent.gameObject.SetActive(false);
    }
}
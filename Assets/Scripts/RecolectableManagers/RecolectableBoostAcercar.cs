using UnityEngine;

public class RecolectableBoostAcercar : ARecolectableSpriteManager
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Jugador"))
            return;

        playerBoostManager.modificarRadio(1.3f);

        transform.parent.gameObject.SetActive(false);

    }
}

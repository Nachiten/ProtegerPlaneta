using UnityEngine;

public class RecolectableBoostAgrandar : ARecolectableSpriteManager
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Jugador"))
            return;

        playerBoostManager.modificarTama�o(1f);

        transform.parent.gameObject.SetActive(false);
    }
}

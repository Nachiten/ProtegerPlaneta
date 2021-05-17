using UnityEngine;

public class RecolectableBuffAchicar : ARecolectableSpriteManager
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Jugador"))
            return;

        playerBoostManager.modificarTama�o(0.35f);

        transform.parent.gameObject.SetActive(false);
    }
}

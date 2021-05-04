using UnityEngine;

public class RecolectableBuffAchicar : ARecolectableSpriteManager
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Jugador"))
            return;

        playerBoostManager.modificarTamaņo(0.4f);

        transform.parent.gameObject.SetActive(false);

    }
}

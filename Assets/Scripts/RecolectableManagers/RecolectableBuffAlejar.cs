using UnityEngine;

public class RecolectableBuffAlejar : ARecolectableSpriteManager
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Jugador"))
            return;

        playerBoostManager.modificarRadio(2.7f);

        transform.parent.gameObject.SetActive(false);
    }
}

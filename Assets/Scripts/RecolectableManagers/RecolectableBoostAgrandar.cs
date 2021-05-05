using UnityEngine;

public class RecolectableBoostAgrandar : ARecolectableSpriteManager
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Jugador"))
            return;

        playerBoostManager.modificarTamaño(1.55f);

        transform.parent.gameObject.SetActive(false);
    }
}

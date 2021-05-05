using UnityEngine;

public abstract class ARecolectableSpriteManager : MonoBehaviour
{
    protected PlayerBoostManager playerBoostManager;

    private void Awake()
    {
        playerBoostManager = GameObject.Find("JugadorSprite").GetComponent<PlayerBoostManager>();

        transform.localPosition = new Vector3(0, 0, 0);
    }
}

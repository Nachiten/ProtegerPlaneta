using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolectableSpriteManager : MonoBehaviour
{
    PlayerBoostManager playerBoostManager;

    private void Awake()
    {
        playerBoostManager = GameObject.Find("JugadorSprite").GetComponent<PlayerBoostManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Jugador"))
            return;

        Debug.Log("Agarraste un boost!!");

        switch (gameObject.tag) 
        {
            case "BoostAgrandar":
                playerBoostManager.modificarTamaño(1.7f);
                break;
            case "BuffAchicar":
                playerBoostManager.modificarTamaño(0.4f);
                break;
            case "BoostAcercar":
                playerBoostManager.modificarRadio(1.3f);
                break;
            case "BuffAlejar":
                playerBoostManager.modificarRadio(2.7f);
                break;
            default:
                Debug.LogError("[BoostManager] Tag del recolectable invalida.");
                break;
        }

        transform.parent.gameObject.SetActive(false);
        
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

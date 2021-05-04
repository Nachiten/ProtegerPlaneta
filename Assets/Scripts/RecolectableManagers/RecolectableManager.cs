using UnityEngine;

public class RecolectableManager : MonoBehaviour
{
    float tiempoAnimacion = 2.6f;

    void OnEnable()
    {
        float rotacionZ = Random.Range(0, 360);

        // Aplico rotacion correspondiente
        transform.Rotate(new Vector3(0, 0, rotacionZ));
    }
    public void habilitar() 
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0).setOnComplete(animarCrecimiento);
    }

    void animarCrecimiento() 
    {
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, new Vector3(1,1,1), tiempoAnimacion);
    }
}

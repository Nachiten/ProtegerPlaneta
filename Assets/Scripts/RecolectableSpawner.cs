using System.Collections.Generic;
using UnityEngine;

public class RecolectableSpawner : MonoBehaviour
{
    List<Recolectable> recolectables;

    private void Start()
    {
        recolectables = new List<Recolectable>();

        recolectables.Add(new Recolectable(GameObject.Find("BoostAgrandar"), 3, 7));
        recolectables.Add(new Recolectable(GameObject.Find("BuffAchicar"), 10, 15));
        recolectables.Add(new Recolectable(GameObject.Find("BoostAcercar"), 5, 9));
        recolectables.Add(new Recolectable(GameObject.Find("BuffAlejar"), 15, 20));
    }

    void Update()
    {
        foreach (Recolectable unRecolectable in recolectables) 
            unRecolectable.correrReloj();
    }
}

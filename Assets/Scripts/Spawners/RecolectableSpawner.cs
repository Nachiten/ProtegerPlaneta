using System.Collections.Generic;
using UnityEngine;

public class RecolectableSpawner : MonoBehaviour
{
    bool perdio = false;

    List<Recolectable> recolectables;

    private void Start()
    {
        recolectables = new List<Recolectable>();

        recolectables.Add(new Recolectable(GameObject.Find("BoostAgrandar"), 15, 20));
        recolectables.Add(new Recolectable(GameObject.Find("BuffAchicar"), 30, 35));
        recolectables.Add(new Recolectable(GameObject.Find("BoostAcercar"), 20, 25));
        recolectables.Add(new Recolectable(GameObject.Find("BuffAlejar"), 35, 40));
    }

    void Update()
    {
        if (perdio)
            return;

        foreach (Recolectable unRecolectable in recolectables) 
            unRecolectable.correrReloj();
    }

    public void perderJuego()
    {
        perdio = true;
    }
}

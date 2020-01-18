using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//Créer des boules rouges et bleues dans un certain pourcentage automatiquement :
public class ChangeColor : MonoBehaviour
{
    public double percentageBlue = 0.8; //Pourcentage de boules bleues à faire apparaître (pourcentage rouge = 1 - pourcantage bleu), en public pour être modifié dans l'Inspector
    public double nbBlue; //Nombre de boules bleues à faire apparaître --> à mettre en PRIVATE !!!!!!!!
    public double nbRed; //Nombre de boules rouges à faire apparaître  --> à mettre en PRIVATE !!!!!!!!
    public int nbBalls = 10; //Nombre total de boules à faire apparaître

    // Start is called before the first frame update
    void Start()
    {
        // Changer de couleur le GameObject nommé "Sphere" :
        /*GameObject sphere = GameObject.Find("Sphere");
        sphere.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);*/

        // Créer 80% de boules bleues et 20% de boules rouges (A FAIRE : ajout de boules blanches !!!!!!!!!!), au total : 10 boules
        GameObject balls = GameObject.Find("Balls Test"); //GameObject parent qui contiendra toutes les balls créées
        nbBlue = percentageBlue * nbBalls;
        nbRed = nbBalls - nbBlue;
        int nbBlueCreated = 0; //nb de boules bleues crées
        float previousX = -20f; //position X de la boule précédemment créée
        for (int i=0; i<nbBalls; i++)
        {
            if (nbBlueCreated < nbBlue) //Créer une boule bleue
            {
                GameObject sphereClone = (GameObject)Instantiate(Resources.Load("Sphere"));
                sphereClone.GetComponent<Transform>().SetParent(balls.GetComponent<Transform>()); //placer la new ball dans "Balls" dans le hiérarchie
                sphereClone.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                sphereClone.GetComponent<Transform>().position = new Vector3(previousX + 1.5f, 0, 0); //décaler la boule pour qu'elle ne soit pas superposée à la précédente
            }
            else //Créer d'une boule rouge
            {
                GameObject sphereClone = (GameObject)Instantiate(Resources.Load("Sphere"));
                sphereClone.GetComponent<Transform>().SetParent(balls.GetComponent<Transform>());
                sphereClone.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                sphereClone.GetComponent<Transform>().position = new Vector3(previousX + 1.5f, 0, 0);
            }
            previousX = previousX + 1.5f;
            nbBlueCreated++;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

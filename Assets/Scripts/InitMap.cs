using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Création de la map intiale (CAD boules rouges, bleues & blanches) pour une simulation :
public class InitMap : MonoBehaviour
{
    public int nbBallsTotal = 100; //Nb. de boules total sur toute la map
    public double percentageBlue = 0.4; //Clinton init sur toute la map : Pourcentage de boules bleues à faire apparaître (pourcentage rouge = 1 - pourcantage bleu - pourcentage blanc), en public pour être modifié dans l'Inspector
    public double nbBlueTotalMap; //Nb. de boules bleues Clinton init sur toute la map //A METTRE EN PRIVATE !!!!!!
    public double percentageWhite = 0.3; //Indécis init
    public double nbWhiteTotalMap; //Nb. de boules blanches indécis init sur toute la map //A METTRE EN PRIVATE !!!!!!
    public double nbRedTotalMap; //Nb. de boules rouges Trump init sur toute la map //A METTRE EN PRIVATE !!!!!!

    private GameObject towns; //Gameobject contenant toutes les villes de la map
    private GameObject countries; //Gameobject contenant toutes les campagnes de la map




    // Start is called before the first frame update
    void Start()
    {
        //Calcul du nb. de boules bleues, blanches et rouges sur toute la map :
        nbBlueTotalMap = Math.Truncate(percentageBlue * nbBallsTotal);
        nbWhiteTotalMap = Math.Truncate(percentageWhite * nbBallsTotal);
        nbRedTotalMap = nbBallsTotal - nbWhiteTotalMap - nbBlueTotalMap;

        //A REVOIR EN AJOUTANT DES BOULES BLANCHES :
        CreateMapInit();

    }




    // Update is called once per frame
    void Update()
    {  
    }


    //Création de la map initiale avec des boules rouges, bleues et blanches dans les villes et campagnes :
    void CreateMapInit()
    {
        double nbRedRestantMap = nbRedTotalMap; //Nb. de boules rouges restant à placer sur la map
        double nbBlueRestantMap = nbBlueTotalMap; //Nb. de boules bleues restant à placer sur la map 
        double nbWhiteRestantMap = nbWhiteTotalMap; //Nb. de boules blanches restant à placer sur la map
        List<Transform> listTowns = new List<Transform>(); //liste des villes de la map
        List<Transform> listCountries = new List<Transform>(); //liste de toutes les campagnes de la map

        //===================
        //Obtenir toutes les villes et campagnes (transform.GetChild(i)) :
        //===================
        towns = GameObject.Find("Villes");
        countries = GameObject.Find("Campagnes");

        int nbTowns = towns.GetComponent<Transform>().childCount; //nb. de villes
        for (int i=0; i< nbTowns; i++)
        {
            listTowns.Add(towns.GetComponent<Transform>().GetChild(i));
        }

        int nbCountries = countries.GetComponent<Transform>().childCount; //nb. de campagnes
        //List<Transform> listCountries = new List<Transform>(); //liste de toutes les campagnes de la map //A REMETTRE SI NO PUBLIC
        for (int i=0; i<nbCountries; i++)
        {
            listCountries.Add(countries.GetComponent<Transform>().GetChild(i));
        }

        System.Random rdn = new System.Random();
        int nbRedToCreate = 0;
        int nbBlueToCreate = 0;
        int nbWhiteToCreate = 0;
        //===================
        // Villes : Créer des boules colorées autour de chaque ville dans un carré de largeur 20 selon l'axe x et z :
        //===================
        for (int i = 0; i < nbTowns; i++)
        {
            //Obtenir les coordonées de la ville :
            float xTown = listTowns[i].transform.position[0];
            float zTown = listTowns[i].transform.position[2];

            //===================
            // Boules rouges :
            //===================
            //Déterminer le nb. de boules rouges à créer (<nbRedRestantMap) :
            nbRedToCreate = rdn.Next(1, (int)nbRedRestantMap + 1);
            nbRedRestantMap = nbRedRestantMap - nbRedToCreate;

            //Créer les boules rouges dans la map :
            for (int j = 0; j < nbRedToCreate; j++)
            {
                int positionX = rdn.Next(525, 630); //Ordre de grandeur trouvé à partir de Transform.position.X des prefab building de chaque ville
                int positionZ = rdn.Next(-530, -440); //idem avec position.Z
                CreateBall(listTowns[i], Color.red, positionX, positionZ);
            }

            //===================
            // Boules bleues :
            //===================
            //Déterminer le nb. de boules bleues à créer (<nbRedRestantMap) :
            nbBlueToCreate = rdn.Next(1, (int)nbBlueRestantMap + 1);
            nbBlueRestantMap = nbBlueRestantMap - nbBlueToCreate;

            //Créer les boules bleues dans la map :
            for (int j = 0; j < nbBlueToCreate; j++)
            {
                int positionX = rdn.Next(530, 570); //Ordre de grandeur trouvé à partir de Transform.position.X des prefab building de chaque ville
                int positionZ = rdn.Next(-530, -490); //idem avec position.Z
                CreateBall(listTowns[i], Color.blue, positionX, positionZ);
            }

            //===================
            // Boules blanches :
            //===================
            //Déterminer le nb. de boules blanches à créer (<nbRedRestantMap) :
            /*if (nbWhiteRestantMap > 5) //pour éviter de créer toutes les boules blanches sur la 1ère ville
            {
                nbWhiteToCreate = rdn.Next(1, 6);
            }
            else
            {
                nbWhiteToCreate = rdn.Next(1, (int)nbWhiteRestantMap + 1);
            }*/
            nbWhiteToCreate = rdn.Next(1, (int)nbWhiteRestantMap + 1);
            nbWhiteRestantMap = nbWhiteRestantMap - nbWhiteToCreate;

            //Créer les boules blanches dans la map :
            for (int j = 0; j < nbWhiteToCreate; j++)
            {
                int positionX = rdn.Next(530, 570); //Ordre de grandeur trouvé à partir de Transform.position.X des prefab building de chaque ville
                int positionZ = rdn.Next(-530, -490); //idem avec position.Z
                CreateBall(listTowns[i], Color.white, positionX, positionZ);
            }
        }

        //===================
        // Campagnes : Créer des boules colorées autour de chaque campagne dans un carré de largeur 20 selon l'axe x et z :
        //===================
        for (int i = 0; i < nbCountries; i++)
        {
            //Obtenir les coordonées de la ville :
            float xTown = listCountries[i].transform.position[0];
            float zTown = listCountries[i].transform.position[2];

            //===================
            // Boules rouges :
            //===================
            //Déterminer le nb. de boules rouges à créer (<nbRedRestantMap) :
            if (i != nbCountries - 1)
            {
                nbRedToCreate = rdn.Next(1, (int)nbRedRestantMap + 1); //Erreur à revoir : 'minValue' cannot be greater than maxValue. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                nbRedRestantMap = nbRedRestantMap - nbRedToCreate;
            }
            else //Sur la dernière campagne, créer autant de boules colorées que nécessaire pour obtenir le %age voulu sur toute la map :
            {
                nbRedToCreate = (int)nbRedRestantMap;
            }

            //Créer les boules rouges dans la map :
            for (int j = 0; j < nbRedToCreate; j++)
            {
                //float positionX = (float)rdn.NextDouble() * ((xTown + 20) - (xTown - 20)) + (xTown - 20); //position de la boule dans un carré de largeur 20 selon l'axe x
                //float positionZ = (float)rdn.NextDouble() * ((zTown + 20) - (zTown - 20)) + (zTown - 20); //position de la boule dans un carré de largeur 20 selon l'axe z
                int positionX = rdn.Next(-635, -655); //Ordre de grandeur trouvé à partir de Transform.position.X des prefab building de chaque campagne
                int positionZ = rdn.Next(425, 470); //idem avec position.Z
                CreateBall(listCountries[i], Color.red, positionX, positionZ);
            }

            //===================
            // Boules bleues :
            //===================
            //Déterminer le nb. de boules bleues à créer (<nbRedRestantMap) :
            if (i != nbCountries - 1)
            {
                nbBlueToCreate = rdn.Next(1, (int)nbBlueRestantMap + 1);
                nbBlueRestantMap = nbBlueRestantMap - nbBlueToCreate;
            }
            else //Dernère campagne : créer toutes les boules bleues restantes
            {
                nbBlueToCreate = (int)nbBlueRestantMap;
            }
            //Créer les boules bleues dans la map :
            for (int j = 0; j < nbBlueToCreate; j++)
            {
                int positionX = rdn.Next(-635, -655); //Ordre de grandeur trouvé à partir de Transform.position.X des prefab building de chaque campagne
                int positionZ = rdn.Next(425, 470); //idem avec position.Z
                CreateBall(listCountries[i], Color.blue, positionX, positionZ);
            }

            //===================
            // Boules blanches :
            //===================
            //Déterminer le nb. de boules blanches à créer (<nbRedRestantMap) :
            if (i != nbCountries - 1)
            {
                nbWhiteToCreate = rdn.Next(1, (int)nbWhiteRestantMap + 1);
                nbWhiteRestantMap = nbWhiteRestantMap - nbWhiteToCreate;
            }
            else //Dernère campagne : créer toutes les boules blanches restantes
            {
                nbWhiteToCreate = (int)nbWhiteRestantMap;
            }
            //Créer les boules blanches dans la map :
            for (int j = 0; j < nbWhiteToCreate; j++)
            {
                int positionX = rdn.Next(-635, -655); //Ordre de grandeur trouvé à partir de Transform.position.X des prefab building de chaque campagne
                int positionZ = rdn.Next(425, 470); //idem avec position.Z
                CreateBall(listCountries[i], Color.white, positionX, positionZ);
            }
        }

    }




    //Création de boules colorées positionnées sur la map à l'emplacement (positionX, positionZ) :
    private void CreateBall(Transform list, Color color, float positionX, float positionZ)
    {
        System.Random rdn = new System.Random();

        GameObject sphereClone = (GameObject)Instantiate(Resources.Load("Sphere")); //instancier une boule
        sphereClone.GetComponent<Transform>().SetParent(list); //placer la new ball dans le GameObject de chaque ville dans la hiérarchie
        sphereClone.GetComponent<Renderer>().material.SetColor("_Color", color);

        //Positionner la boule autour de la ville :
        sphereClone.GetComponent<Transform>().localPosition = new Vector3(positionX, -940, positionZ);
        sphereClone.GetComponent<Transform>().localScale = new Vector3(10f, 10f, 10f);
    }





}

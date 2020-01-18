using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

public class StartSimu : MonoBehaviour
{
    public int nbBallsTotal = 100; //Nb. de boules total sur toute la map
    public double percentageBlue = 0.4; //Clinton init sur toute la map : Pourcentage de boules bleues à faire apparaître (pourcentage rouge = 1 - pourcantage bleu - pourcentage blanc), en public pour être modifié dans l'Inspector
    public double nbBlueTotalMap; //Nb. de boules bleues Clinton init sur toute la map //A METTRE EN PRIVATE !!!!!!
    public double percentageWhite = 0.3; //Indécis init
    public double nbWhiteTotalMap; //Nb. de boules blanches indécis init sur toute la map //A METTRE EN PRIVATE !!!!!!
    public double nbRedTotalMap; //Nb. de boules rouges Trump init sur toute la map //A METTRE EN PRIVATE !!!!!!
    private GameObject towns; //Gameobject contenant toutes les villes de la map
    private GameObject countries; //Gameobject contenant toutes les campagnes de la map

    //===================
    //      TEST
    //===================
    public List<int> listWhiteTest = new List<int>();
    public List<int> listBlueTest = new List<int>();
    public List<int> listRedTest = new List<int>();
    public string rentreBoucle = "";

    //public var choice_language : int = 1; //public int choice_language = 1;
    public int choice_language = 1;
    private GameObject btnEvent;

    private const string CHEMIN = "Test.xml";

    [Serializable]
    public class Ball
    {
        private int indiceVilleVillage;
        private Color color;
        private float positionX;
        private float positionY;
        private float positionZ;

        public int IndiceVilleVillage
        {
            get
            {
                return indiceVilleVillage;
            }

            set
            {
                indiceVilleVillage = value;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public float PositionX
        {
            get
            {
                return positionX;
            }

            set
            {
                positionX = value;
            }
        }

        public float PositionY
        {
            get
            {
                return positionY;
            }

            set
            {
                positionY = value;
            }
        }

        public float PositionZ
        {
            get
            {
                return positionZ;
            }

            set
            {
                positionZ = value;
            }
        }

        public Ball()
        {

        }

        public Ball(int i, Color c, float x, float y, float z)
        {
            this.IndiceVilleVillage = i;
            this.Color = c;
            this.PositionX = x;
            this.PositionY = y;
            this.PositionZ = z;
        }


    }

    [Serializable()]
    public class Balls : List<Ball>     //On hérite d'une liste de balles.
    {

        /// <summary>
        /// Enregistre l'état courant de la classe dans un fichier au format XML.
        /// </summary>
        /// <param name="chemin"></param>
        public void Enregistrer(string chemin)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Balls));
            StreamWriter ball = new StreamWriter(chemin);
            serializer.Serialize(ball, this);
            ball.Close();
        }

        public static Balls Charger(string chemin)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Balls));
            StreamReader lecteur = new StreamReader(chemin);
            Balls p = (Balls)deserializer.Deserialize(lecteur);
            lecteur.Close();

            return p;
        }



    }


    public Balls ballsCity = new Balls();
    public Balls ballsCountry = new Balls();



    // Start is called before the first frame update
    void Start()
    {
        //Calcul du nb. de boules bleues, blanches et rouges sur toute la map :
        nbBlueTotalMap = Math.Truncate(percentageBlue * nbBallsTotal);
        nbWhiteTotalMap = Math.Truncate(percentageWhite * nbBallsTotal);
        nbRedTotalMap = nbBallsTotal - nbWhiteTotalMap - nbBlueTotalMap;
        CreateMapInit();


        btnEvent = GameObject.Find("Button Start");
        btnEvent.GetComponent<Button>().onClick.AddListener(TaskOnClick);

        PlayerPrefs.SetInt("nbWeek", 0);

    }

    // Update is called once per frame
    void Update() { }

    //Changer de scene selon l'event :
    void TaskOnClick() //A CHANGER SELON LES VRAIS NOMS DES SCENES DES EVENTS !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        SceneManager.LoadScene("Map");
    }



    //Calculer le nombre de boules d'une couleur donnée dans chaque ville et campagne :
    private List<int> CalculateColor(double nbColorTotalMap, List<Transform> listTowns, List<Transform> listCountries, List<int> listColor)
    {
        System.Random rdn = new System.Random();
        int nbRestant = Convert.ToInt32(nbColorTotalMap); //boules blanches restantes à créer sur la map
        int nbToCreate = 0;

        for (int i = 0; i < listTowns.Count + listCountries.Count; i++)
        {
            if (i != listTowns.Count + listCountries.Count - 1) //Ce n'est pas le dernier de la liste
            {
                if (nbRestant > nbColorTotalMap / 13) //pour éviter d'avoir trop de boules blanches sur la 1ère ville et plus aucune sur les autres villes
                {
                    if (nbColorTotalMap / 13 - 10 > 0)
                    {
                        nbToCreate = rdn.Next((int)nbColorTotalMap / 13 - 10, (int)nbColorTotalMap / 13 + 10);
                    }
                    else
                    {
                        nbToCreate = rdn.Next((int)nbColorTotalMap / 13, (int)nbColorTotalMap / 13 + 10);
                    }

                }
                else if (nbRestant == 0)
                {
                    nbToCreate = 0;
                }
                else
                {
                    nbToCreate = rdn.Next(1, Convert.ToInt32(nbColorTotalMap / 13)); //nbRestant
                }
            }
            else //on place toutes les boules restantes dans la dernière campagne :
            {
                nbToCreate = nbRestant;
            }
            listColor.Add(nbToCreate);
            nbRestant = nbRestant - nbToCreate;
        }
        //listWhiteTest = listColor; //void

        return listColor;
    }


    private void FillListsBallsCountry(int indice, Color color, float positionX, float positionY, float positionZ)
    {
        Ball ball = new Ball(indice, color, positionX, positionY, positionZ);
        this.ballsCountry.Add(ball);
    }

    private void FillListsBallsCity(int indice, Color color, float positionX, float positionY, float positionZ)
    {
        Ball ball = new Ball(indice, color, positionX, positionY, positionZ);
        this.ballsCity.Add(ball);
    }


    //Création de boules colorées positionnées sur la map à l'emplacement (positionX, positionZ) :
    private void CreateBall(Transform list, Color color, float positionX, float positionY, float positionZ)
    {
        System.Random rdn = new System.Random();

        GameObject sphereClone = (GameObject)Instantiate(Resources.Load("Sphere")); //instancier une boule
        sphereClone.GetComponent<Transform>().SetParent(list); //placer la new ball dans le GameObject de chaque ville dans la hiérarchie
        sphereClone.GetComponent<Renderer>().material.SetColor("_Color", color);

        //Positionner la boule autour de la ville :
        sphereClone.GetComponent<Transform>().localPosition = new Vector3(positionX, positionY, positionZ); //y=-940
        sphereClone.GetComponent<Transform>().localScale = new Vector3(10f, 10f, 10f);
    }




    void CreateMapInit()
    {

        System.Random rdn = new System.Random();
        List<Transform> listTowns = new List<Transform>(); //liste des villes de la map
        List<Transform> listCountries = new List<Transform>(); //liste de toutes les campagnes de la map

        //===================
        // Obtenir toutes les villes et campagnes (transform.GetChild(i)) :
        //===================
        towns = GameObject.Find("Villes");
        countries = GameObject.Find("Campagnes");

        int nbTowns = towns.GetComponent<Transform>().childCount; //nb. de villes
        for (int i = 0; i < nbTowns; i++)
        {
            listTowns.Add(towns.GetComponent<Transform>().GetChild(i));
        }

        int nbCountries = countries.GetComponent<Transform>().childCount; //nb. de campagnes
        for (int i = 0; i < nbCountries; i++)
        {
            listCountries.Add(countries.GetComponent<Transform>().GetChild(i));
        }

        //===================
        // Trouver le nb de boules colorées à répartir dans toutes les villes et campagnes :
        //===================
        List<int> listNbWhite = new List<int>();
        List<int> listNbBlue = new List<int>();
        List<int> listNbRed = new List<int>();

        //Boules blanches :
        listWhiteTest = CalculateColor(nbWhiteTotalMap, listTowns, listCountries, listNbWhite);
        //Boules bleues :
        listBlueTest = CalculateColor(nbBlueTotalMap, listTowns, listCountries, listNbBlue);
        //Boules rouges :
        listRedTest = CalculateColor(nbRedTotalMap, listTowns, listCountries, listNbRed);

        //===================
        // Villes : Créer des boules colorées autour de chaque ville :
        //===================
        for (int i = 0; i < nbTowns; i++)
        {
            //Créer les boules blanches dans une ville :
            for (int j = 0; j < listNbWhite[i]; j++)
            {
                int positionX = rdn.Next(525, 630); //Ordre de grandeur trouvé à partir de Transform.position.X des prefab building de chaque ville
                int positionZ = rdn.Next(-530, -440); //idem avec position.Z
                FillListsBallsCity(i, Color.white, positionX, -940, positionZ);
            }
            //Créer les boules bleues dans une ville :
            for (int j = 0; j < listNbBlue[i]; j++)
            {
                int positionX = rdn.Next(525, 630);
                int positionZ = rdn.Next(-530, -440);
                FillListsBallsCity(i, Color.blue, positionX, -940, positionZ);
               
            }
            //Créer les boules rouges dans une ville :
            for (int j = 0; j < listNbRed[i]; j++)
            {
                int positionX = rdn.Next(525, 630);
                int positionZ = rdn.Next(-530, -440);
                FillListsBallsCity(i, Color.red, positionX, -940, positionZ);
            }
        }

        //===================
        // Campagnes : Créer des boules colorées autour de chaque campagne :
        //===================
        for (int i = 0; i < nbCountries; i++)
        {
            //Créer les boules blanches dans une campagne :
            for (int j = 0; j < listNbWhite[i + nbTowns]; j++)
            {
                rentreBoucle = "Rentre dans la boucle campagnes";
                int positionX = rdn.Next(-670, -620); //Ordre de grandeur trouvé à partir de Transform.position.X des prefab building de chaque campagne
                int positionZ = rdn.Next(400, 460); //idem avec position.Z
                FillListsBallsCountry(i, Color.white, positionX, -450, positionZ);
            }
            //Créer les boules bleues dans une campagne :
            for (int j = 0; j < listNbBlue[i + nbTowns]; j++)
            {
                rentreBoucle = "Rentre dans la boucle campagnes";
                int positionX = rdn.Next(-670, -620);
                int positionZ = rdn.Next(400, 460);
                FillListsBallsCountry(i, Color.blue, positionX, -450, positionZ);
            }


            //Créer les boules rouges dans une campagne :
            for (int j = 0; j < listNbRed[i + nbTowns]; j++)
            {
                int positionX = rdn.Next(-670, -620);
                int positionZ = rdn.Next(400, 460);
                FillListsBallsCountry(i, Color.red, positionX, -450, positionZ);
            }
        }
        
        ballsCountry.Enregistrer("BallsCountry.xml");
        ballsCity.Enregistrer("BallsCity.xml");
    }


}

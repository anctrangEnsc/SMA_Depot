using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

//Avec une liste pour les pourcentages de chaque couleur de boules
public class InitMap2 : MonoBehaviour
{

    public int NbProTrump = 0;
    public int NbProClinton = 0;
    public int NbIndecis = 0;

    public int nbBallsTotal = 100; //Nb. de boules total sur toute la map
    public double percentageBlue = 0.4; //Clinton init sur toute la map : Pourcentage de boules bleues à faire apparaître (pourcentage rouge = 1 - pourcantage bleu - pourcentage blanc), en public pour être modifié dans l'Inspector
    public double nbBlueTotalMap; //Nb. de boules bleues Clinton init sur toute la map //A METTRE EN PRIVATE !!!!!!
    public double percentageWhite = 0.3; //Indécis init
    public double nbWhiteTotalMap; //Nb. de boules blanches indécis init sur toute la map //A METTRE EN PRIVATE !!!!!!
    public double nbRedTotalMap; //Nb. de boules rouges Trump init sur toute la map //A METTRE EN PRIVATE !!!!!!

    private GameObject towns; //Gameobject contenant toutes les villes de la map
    private GameObject countries; //Gameobject contenant toutes les campagnes de la map
    private GameObject Event1;
    private GameObject Event2;
    private GameObject Event3;
    private GameObject Event4;
    private GameObject Event5;
    private GameObject Event6;
    private GameObject Event7;
    private GameObject Event8;
    private GameObject Event9;
    private GameObject Event10;




    public List<int> listWhiteTest = new List<int>();
    public List<int> listBlueTest = new List<int>();
    public List<int> listRedTest = new List<int>();
    public string rentreBoucle = "";

    //public var choice_language : int = 1; //public int choice_language = 1;
    public int choice_language = 1;
    public Balls ballsCity = new Balls();
    public Balls ballsCountry = new Balls();


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



    // Start is called before the first frame update
    void Start()
    {
        // déserialisation
        ballsCity = Balls.Charger("BallsCity.xml");// liste contenant la liste des boules des villes
        ballsCountry = Balls.Charger("BallsCountry.xml"); // liste contenant la liste des boules en campagne
                                                          //Charger et cacher le panel de la semaine et du nom de l'event :
        Event1 = GameObject.Find("Semaine1");
        Event1.SetActive(false);
        Event2 = GameObject.Find("Semaine3");
        Event2.SetActive(false);
        Event3 = GameObject.Find("Semaine5");
        Event3.SetActive(false);
        Event4 = GameObject.Find("Semaine7");
        Event4.SetActive(false);
        Event5 = GameObject.Find("Semaine9");
        Event5.SetActive(false);
        Event6 = GameObject.Find("Semaine11");
        Event6.SetActive(false);
        Event7 = GameObject.Find("Semaine13");
        Event7.SetActive(false);
        Event8 = GameObject.Find("Semaine15");
        Event8.SetActive(false);
        Event9 = GameObject.Find("Semaine17");
        Event9.SetActive(false);
        Event10 = GameObject.Find("Semaine19");
        Event10.SetActive(false);
        CreateMapInit();
        TraitementIdeesPolitiques();

        File.Delete("BallsCity.xml");
        File.Delete("BallsCountry.xml");


        /*Compteurs de populations*/
        foreach (Ball b in this.ballsCity)
        {
            if (b.Color == Color.white)
                this.NbIndecis++;
            else if (b.Color == Color.blue)
                this.NbProClinton++;
            else
                this.NbProTrump++;
        }

        foreach (Ball b in this.ballsCountry)
        {
            if (b.Color == Color.white)
                this.NbIndecis++;
            else if (b.Color == Color.blue)
                this.NbProClinton++;
            else
                this.NbProTrump++;
        }
        GestionEvenement();

        ballsCountry.Enregistrer("BallsCountry.xml");
        ballsCity.Enregistrer("BallsCity.xml");

        //Affichage %age de boules selon chaque partie :
        GameObject txtClinton = GameObject.Find("Nb Clinton");
        GameObject txtTrump = GameObject.Find("Nb Trump");
        GameObject txtIndecis = GameObject.Find("Nb Indecis");

        double percClinton = (NbProClinton * 100) / nbBallsTotal; //Pourcentage de pro-Clinton
        double percTrump = (NbProTrump * 100) / nbBallsTotal;
        double percIndecis = (NbIndecis * 100) / nbBallsTotal;
        percIndecis = percIndecis + 100 - (percClinton + percTrump + percIndecis);

        txtClinton.GetComponent<Text>().text = Convert.ToString(percClinton) + "%";
        txtTrump.GetComponent<Text>().text = Convert.ToString(percTrump) + "%";
        txtIndecis.GetComponent<Text>().text = Convert.ToString(percIndecis) + "%";



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

    private void ConferenceClinton(int indice, bool village)
    {
        System.Random r = new System.Random();
        if (village)
        {
            foreach (Ball b in this.ballsCountry)
            {
                int value = r.Next(0, 100);
                if (b.IndiceVilleVillage == indice)
                {
                    if (b.Color == Color.white)
                    {
                        if (value > 75)
                        {
                            if (b.Color == Color.white)
                                b.Color = Color.blue;
                            else if (b.Color == Color.red)
                                b.Color = Color.white;
                        }
                    }
                    else
                    {
                        if (value > 85)
                        {
                            if (b.Color == Color.white)
                                b.Color = Color.blue;
                            else if (b.Color == Color.red)
                                b.Color = Color.white;
                        }
                    }
                    
                }
            }
        }
        else
        {
            foreach (Ball b in this.ballsCity)
            {
                int value = r.Next(0, 100);
                if (b.IndiceVilleVillage == indice)
                {
                    if (b.Color == Color.white)
                    {
                        if (value > 75)
                        {
                            if (b.Color == Color.white)
                                b.Color = Color.blue;
                            else if (b.Color == Color.red)
                                b.Color = Color.white;
                        }
                    }
                    else
                    {
                        if (value > 85)
                        {
                            if (b.Color == Color.white)
                                b.Color = Color.blue;
                            else if (b.Color == Color.red)
                                b.Color = Color.white;
                        }
                    }
                }
            }
        }

    }
    private void ConferenceTrump(int indice, bool village)
    {
        System.Random r = new System.Random();
        if (village)
        {
            foreach (Ball b in this.ballsCountry)
            {
                int value = r.Next(0, 100);
                if (b.IndiceVilleVillage == indice)
                {
                    if (b.Color == Color.white)
                    {
                        if (value > 75)
                        {
                                b.Color = Color.red;
                           
                        }

                    }

                    else
                    {
                        if (value > 85)
                        {
                        
                                b.Color = Color.white;
                        }
                    }
                    
                }
            }
        }
        else
        {
            foreach (Ball b in this.ballsCity)
            {
                int value = r.Next(0, 100);
                if (b.IndiceVilleVillage == indice)
                {
                    if (b.Color == Color.white)
                    {
                        if (value > 75)
                        {
                            b.Color = Color.red;

                        }

                    }

                    else
                    {
                        if (value > 85)
                        {

                            b.Color = Color.white;
                        }
                    }
                }
            }
        }

    }
    private void CrimeRacial(int indice, bool village)
    {
        System.Random r = new System.Random();
        if (village)
        {
            foreach (Ball b in this.ballsCountry)
            {
                int value = r.Next(0, 100);
                if (b.IndiceVilleVillage == indice)
                {
                    if (b.Color == Color.white)
                    {
                        if (value > 50)
                        {

                            b.Color = Color.blue;
                        }

                    }

                    else
                    {
                        if (value > 75)
                        {

                            b.Color = Color.blue;
                        }
                    }
                    
                }
            }
        }
        else
        {
            foreach (Ball b in this.ballsCity)
            {
                int value = r.Next(0, 100);
                if (b.IndiceVilleVillage == indice)
                {
                    if (b.Color == Color.white)
                    {
                        if (value > 50)
                        {

                            b.Color = Color.blue;
                        }

                    }

                    else
                    {
                        if (value > 75)
                        {

                            b.Color = Color.blue;
                        }
                    }
                }
            }
        }
    }
    private void Attentat(int indice, bool village)
    {
        System.Random r = new System.Random();
        if (village)
        {
            foreach (Ball b in this.ballsCountry)
            {

                int value = r.Next(0, 100);
                if (b.IndiceVilleVillage == indice)
                {
                    if (b.Color == Color.white)
                    {
                        if (value > 50)
                        {
                            b.Color = Color.red;
                        }

                    }
                    else
                    {
                        if (value > 75)
                        {
                            b.Color = Color.red;
                        }

                    }
                    
                }
            }
        }
        else
        {
            foreach (Ball b in this.ballsCity)
            {

                int value = r.Next(0, 100);
                if (b.IndiceVilleVillage == indice)
                {
                    if (b.Color == Color.white)
                    {
                        if (value > 50)
                        {
                            b.Color = Color.red;
                        }

                    }
                    else
                    {
                        if (value > 75)
                        {
                            b.Color = Color.red;
                        }

                    }
                }
            }
        }

    }

    private void GestionEvenement()
    {

        // Faire correspondre événement et ville/village
        if (PlayerPrefs.GetInt("nbWeek") == 1)
        {
            Event1.SetActive(true);
            ConferenceClinton(0, false);

        }

        else if (PlayerPrefs.GetInt("nbWeek") == 2)
            Event1.SetActive(false);
        else if (PlayerPrefs.GetInt("nbWeek") == 3)
        {
            Event2.SetActive(true);
            ConferenceTrump(1, false);
        }

        else if (PlayerPrefs.GetInt("nbWeek") == 4)
            Event2.SetActive(false);
        else if (PlayerPrefs.GetInt("nbWeek") == 5)
        {
            Event3.SetActive(true);
            ConferenceClinton(2, false);
        }
        else if (PlayerPrefs.GetInt("nbWeek") == 6)
            Event3.SetActive(false);
        else if (PlayerPrefs.GetInt("nbWeek") == 7)
        {
            CrimeRacial(3, false);
            Event4.SetActive(true);

        }
        else if (PlayerPrefs.GetInt("nbWeek") == 8)
            Event4.SetActive(false);
        else if (PlayerPrefs.GetInt("nbWeek") == 9)
        {
            ConferenceTrump(0, true);
            Event5.SetActive(true);

        }
        else if (PlayerPrefs.GetInt("nbWeek") == 10)
            Event5.SetActive(false);
        else if (PlayerPrefs.GetInt("nbWeek") == 11)
        {
            Event6.SetActive(true);
            Attentat(1, true);
        }

        else if (PlayerPrefs.GetInt("nbWeek") == 12)
            Event6.SetActive(false);
        else if (PlayerPrefs.GetInt("nbWeek") == 13)
        {
            Event7.SetActive(true);
            ConferenceClinton(2, true);
        }
        else if (PlayerPrefs.GetInt("nbWeek") == 14)
            Event7.SetActive(false);
        else if (PlayerPrefs.GetInt("nbWeek") == 15)
        {
            Event8.SetActive(true);
            ConferenceTrump(3, true);

        }
        else if (PlayerPrefs.GetInt("nbWeek") == 16)
            Event8.SetActive(false);
        else if (PlayerPrefs.GetInt("nbWeek") == 17)
        {
            Event9.SetActive(true);
            ConferenceClinton(4, true);
        }
        else if (PlayerPrefs.GetInt("nbWeek") == 18)
            Event9.SetActive(false);
        else if (PlayerPrefs.GetInt("nbWeek") == 19)
        {
            Event10.SetActive(true);
            ConferenceTrump(5, true);
        }
        else if (PlayerPrefs.GetInt("nbWeek") == 20)
            Event10.SetActive(false);




    }



    private void TraitementIdeesPolitiques()
    {
        System.Random random = new System.Random();
        int persuasion = 0;
        foreach (Ball b in this.ballsCity)
        {
            // +-4
            foreach (Ball b2 in this.ballsCity)
            {
                if (b.IndiceVilleVillage == b2.IndiceVilleVillage && b.PositionX <= b2.PositionX + 3 && b.PositionX >= b2.PositionX - 3 &&
                    b.PositionZ <= b2.PositionZ + 3 && b.PositionZ >= b2.PositionZ - 3)
                {
                    // traitement des pourcentages              
                    persuasion = random.Next(1, 100);
                    if (persuasion > 75 && b.Color != Color.white)
                    {
                        if (b.Color == Color.blue)
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.blue;
                            else if (b2.Color == Color.red)
                                b2.Color = Color.white;
                        }
                        else
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.red;
                            else if (b2.Color == Color.blue)
                                b2.Color = Color.white;
                        }
                    }
                }
                if (b.IndiceVilleVillage == b2.IndiceVilleVillage && b.PositionX <= b2.PositionX + 6 && b.PositionX >= b2.PositionX - 6 &&
                    b.PositionZ <= b2.PositionZ + 6 && b.PositionZ >= b2.PositionZ - 6)
                {
                    // traitement des pourcentages              
                    persuasion = random.Next(1, 100);
                    if (persuasion > 88 && b.Color != Color.white)
                    {
                        if (b.Color == Color.blue)
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.blue;
                            else if (b2.Color == Color.red)
                                b2.Color = Color.white;
                        }

                        else
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.red;
                            else if (b2.Color == Color.blue)
                                b2.Color = Color.white;
                        }

                    }
                }

                if (b.IndiceVilleVillage == b2.IndiceVilleVillage && b.PositionX <= b2.PositionX + 10 && b.PositionX >= b2.PositionX - 10 &&
                   b.PositionZ <= b2.PositionZ + 10 && b.PositionZ >= b2.PositionZ - 10)
                {
                    // traitement des pourcentages              
                    persuasion = random.Next(1, 100);
                    if (persuasion > 95 && b.Color != Color.white)
                    {
                        if (b.Color == Color.blue)
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.blue;
                            else if (b2.Color == Color.red)
                                b2.Color = Color.white;
                        }

                        else
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.red;
                            else if (b2.Color == Color.blue)
                                b2.Color = Color.white;
                        }

                    }
                }
            }
        }

        foreach (Ball b in this.ballsCountry)
        {
            foreach (Ball b2 in this.ballsCountry)
            {
                if (b.IndiceVilleVillage == b2.IndiceVilleVillage && b.PositionX <= b2.PositionX + 3 && b.PositionX >= b2.PositionX - 3 &&
                      b.PositionZ <= b2.PositionZ + 3 && b.PositionZ >= b2.PositionZ - 3)
                {
                    // traitement des pourcentages              
                    persuasion = random.Next(1, 100);
                    if (persuasion > 75 && b.Color != Color.white)
                    {
                        if (b.Color == Color.blue)
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.blue;
                            else if (b2.Color == Color.red)
                                b2.Color = Color.white;
                        }
                        else
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.red;
                            else if (b2.Color == Color.blue)
                                b2.Color = Color.white;
                        }
                    }
                }
                if (b.IndiceVilleVillage == b2.IndiceVilleVillage && b.PositionX <= b2.PositionX + 6 && b.PositionX >= b2.PositionX - 6 &&
                    b.PositionZ <= b2.PositionZ + 6 && b.PositionZ >= b2.PositionZ - 6)
                {
                    // traitement des pourcentages              
                    persuasion = random.Next(1, 100);
                    if (persuasion > 88 && b.Color != Color.white)
                    {
                        if (b.Color == Color.blue)
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.blue;
                            else if (b2.Color == Color.red)
                                b2.Color = Color.white;
                        }

                        else
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.red;
                            else if (b2.Color == Color.blue)
                                b2.Color = Color.white;
                        }

                    }
                }

                if (b.IndiceVilleVillage == b2.IndiceVilleVillage && b.PositionX <= b2.PositionX + 10 && b.PositionX >= b2.PositionX - 10 &&
                   b.PositionZ <= b2.PositionZ + 10 && b.PositionZ >= b2.PositionZ - 10)
                {
                    // traitement des pourcentages              
                    persuasion = random.Next(1, 100);
                    if (persuasion > 95 && b.Color != Color.white)
                    {
                        if (b.Color == Color.blue)
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.blue;
                            else if (b2.Color == Color.red)
                                b2.Color = Color.white;
                        }

                        else
                        {
                            if (b2.Color == Color.white)
                                b2.Color = Color.red;
                            else if (b2.Color == Color.blue)
                                b2.Color = Color.white;
                        }

                    }
                }
            }
        }
        CreateMapInit();
    }

    // Update is called once per frame
    void Update() { }

    //Création de la map initiale avec des boules rouges, bleues et blanches dans les villes et campagnes :
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
        // Villes : Créer des boules colorées autour de chaque ville :
        //===================
        for (int i = 0; i < nbTowns; i++)
        {
            foreach (Ball b in this.ballsCity)
            {
                if (b.IndiceVilleVillage == i)
                    CreateBall(listTowns[i], b.Color, b.PositionX, b.PositionY, b.PositionZ);
            }


        }

        //===================
        // Campagnes : Créer des boules colorées autour de chaque campagne :
        //===================
        for (int i = 0; i < nbCountries; i++)
        {

            foreach (Ball b in this.ballsCountry)
            {
                if (b.IndiceVilleVillage == i)
                    CreateBall(listCountries[i], b.Color, b.PositionX, b.PositionY, b.PositionZ);
            }

        }
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

}
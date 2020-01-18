using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Changer de Scene :
public class ChangeScene : MonoBehaviour
{
    private GameObject panelSemaine;
    private GameObject btnEvent;
    private Text txtEvent;

    public int nbWeek;  


    // Start is called before the first frame update
    void Start()
    {
        //Charger et cacher le panel de la semaine et du nom de l'event :
        panelSemaine = GameObject.Find("Panel Semaine");
        panelSemaine.SetActive(false);

        //Afficher le panel au bout de quelques secondes :
        StartCoroutine(ShowWeekBtn());

        //Evaluer le nb de semaine :
        nbWeek = PlayerPrefs.GetInt("nbWeek");
        nbWeek++;
        PlayerPrefs.SetInt("nbWeek", nbWeek);
    }


    // Update is called once per frame
    void Update() {}

    //Attendre 3 secondes avant d'afficher le panel avec le n° de la semaine et le btn pour changer de scène selon l'event (Coroutine) :
    IEnumerator ShowWeekBtn()
    {
        yield return new WaitForSeconds(3);

        //Afficher de nouveau le panel de la semaine :
        panelSemaine.SetActive(true);

        GameObject txt = GameObject.Find("Txt Event");
        txtEvent = txt.GetComponent<Text>();
        btnEvent = GameObject.Find("Btn Event");
        Button btnEventClick = btnEvent.GetComponent<Button>();

        //Changer l'affichage du n° de la semaine :
        GameObject txtSemaine = GameObject.Find("Txt Semaine");
        txtSemaine.GetComponent<Text>().text = "Semaine " + nbWeek + " :";



        //Décider des events selon les semaines :
        if (nbWeek == 1 || nbWeek == 5 || nbWeek==13 || nbWeek == 17)
        {
            txtEvent.text="Conférence Clinton";
        }
        else if (nbWeek == 3 || nbWeek == 9 || nbWeek == 15 || nbWeek == 19)
        {
            txtEvent.text = "Conférence Trump";
        }
      
        else if (nbWeek == 7)
        {
            txtEvent.text = "Crime racial";
        }
        else if (nbWeek == 11)
        {
            txtEvent.text = "Attentat Islamiste";
        }
        else
        {
            txtEvent.text = "Aucun event";
        }
        btnEventClick.onClick.AddListener(TaskOnClick);

    }

    //Changer de scene selon l'event :
    void TaskOnClick() //A CHANGER SELON LES VRAIS NOMS DES SCENES DES EVENTS !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        if (txtEvent.text == "Aucun event")
        {
            SceneManager.LoadScene("Map");
        }
        else
        {
            SceneManager.LoadScene("terrainVille");
        }
    }

}

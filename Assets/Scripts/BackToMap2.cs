using System.Collections;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Revenir à la map :
public class BackToMap2 : MonoBehaviour
{
    
    private Camera cameraClinton;
    private Camera cameraTrump;
    private Camera cameraAttentat;
    private Camera cameraCrime;

    void Start()
    {
        GameObject cam1 = GameObject.Find("Camera Conf Clinton");
        cameraClinton = cam1.GetComponent<Camera>();
        GameObject cam2 = GameObject.Find("Camera Conf Trump");
        cameraTrump = cam2.GetComponent<Camera>();
        GameObject cam3 = GameObject.Find("Camera Attentat");
        cameraAttentat = cam3.GetComponent<Camera>();
        GameObject cam4 = GameObject.Find("CameraCrimeRacial");
        cameraCrime = cam4.GetComponent<Camera>();

        //Revenir à la map au bout de quelques secondes :
        StartCoroutine(ShowWeekBtn());


        //Choisir la caméra selon l'event de la semaine en cours :
        int nbWeek = PlayerPrefs.GetInt("nbWeek");
        if (nbWeek == 1 || nbWeek == 5 || nbWeek == 13 || nbWeek == 17) //Conférence Clinton
        {          
            cameraClinton.enabled = true;
            cameraTrump.enabled = false;
            cameraAttentat.enabled = false;
            cameraCrime.enabled = false;
        }
        else if (nbWeek == 3 || nbWeek == 9 || nbWeek == 15 || nbWeek == 19) //Conférence Trump
        {
            cameraClinton.enabled = false;
            cameraTrump.enabled = true;
            cameraAttentat.enabled = false;
            cameraCrime.enabled = false;
        }

        else if (nbWeek == 7) //Crime racial
        {
            cameraClinton.enabled = false;
            cameraTrump.enabled = false;
            cameraAttentat.enabled = false;
            cameraCrime.enabled = true;
        }
        else //Attentat
        {
            cameraClinton.enabled = false;
            cameraTrump.enabled = false;
            cameraAttentat.enabled = true;
            cameraCrime.enabled = false;
        }
    }

    void Update(){}


    //Attendre 3 secondes avant d'afficher le panel avec le n° de la semaine et le btn pour changer de scène selon l'event (Coroutine) :
    IEnumerator ShowWeekBtn()
    {
        yield return new WaitForSeconds(10);

        //Revenir à la map :
        SceneManager.LoadScene("Map");


    }




}

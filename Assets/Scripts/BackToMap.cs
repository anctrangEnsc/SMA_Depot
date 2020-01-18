using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Lorsque l'on est sur une scène d'un event, on revient à la map principale
public class BackToMap : MonoBehaviour
{
    private Button btnBackToMap;

    // Start is called before the first frame update
    void Start()
    {
        GameObject btn = GameObject.Find("Btn Back to Map");
        btnBackToMap = btn.GetComponent<Button>();
        btnBackToMap.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update() {}

    //Revenir à la map principale :
    void TaskOnClick()
    {
        SceneManager.LoadScene("Map");
    }


}

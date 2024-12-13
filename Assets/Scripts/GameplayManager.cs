using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject Control1;
    public GameObject Control2;
    public GameObject Rules;

    public void CloseGameplay(){
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    } 

    public void GoToControl1(){
        Control1.SetActive(true);
        Control2.SetActive(false);
        Rules.SetActive(false);
    }

    public void GoToControl2(){
        Control1.SetActive(false);
        Control2.SetActive(true);
        Rules.SetActive(false);
    }

    public void GoToRules(){
        Control1.SetActive(false);
        Control2.SetActive(false);
        Rules.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

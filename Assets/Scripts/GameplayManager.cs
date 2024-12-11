using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private GameObject Control1;

    private GameObject Control2;
    private GameObject Rules;
    // Start is called before the first frame update
    void Start()
    {
        Control1 = transform.Find("control1").gameObject;
        Control2 = transform.Find("control2").gameObject;
        Rules = transform.Find("rules").gameObject;
    }

    public void CloseGameplay(){
        Control1.SetActive(false);
        Control2.SetActive(false);
        Rules.SetActive(false);
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

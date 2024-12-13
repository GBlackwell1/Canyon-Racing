using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Time;
    public GameObject HighScore;
    private TextMeshProUGUI TimeLabel;
    public string level;
    public void Start() {
        TimeLabel = HighScore.GetComponent<TextMeshProUGUI>();
        Time.SetActive(false);
        HighScore.SetActive(false);
    }
    public void OnPointerEnter (PointerEventData eventData){
        if (PlayerPrefs.HasKey($"{level} time"))
        {
            float speed = PlayerPrefs.GetFloat($"{level} time");
            TimeLabel.text = speed.ToString("F2") + "secs";
        }
        else{
            TimeLabel.text = "-:--";
        }
        Time.SetActive(true);
        HighScore.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        Time.SetActive(false);
        HighScore.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sun : MonoBehaviour
{
    public GameObject lose;
    public GameObject hud;
    public TextMeshProUGUI TEXT;


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            lose.SetActive(true);
            GameManager.gameFinished = true;
            TEXT.SetText("TOO HOT");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public GameObject main;
    public GameObject howToPlay;
    
    public void StartB()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitB()
    {
        Application.Quit();
    }
    public void HowToPlay()
    {
        main.SetActive(false);
        howToPlay.SetActive(true);
    }
    public void Back()
    {
        main.SetActive(true);
        howToPlay.SetActive(false);
    }
    public void MenuB()
    {
        SceneManager.LoadScene(0);
    }

}

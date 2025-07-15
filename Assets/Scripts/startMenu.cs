using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    public GameObject start;
    public GameObject how;
    public GameObject[] hows;
    public GameObject level;
    private int index = 0;
    private void Start()
    {
        start.SetActive(true);
        how.SetActive(false);
        level.SetActive(false);
    }
    // Start is called before the first frame update
    public void playGame()
    {
        start.SetActive(false);
        level.SetActive(true);
        how.SetActive(false);
    }

    public void quitGamme()
    {
        Application.Quit();
    }

    public void howTo()
    {
        start.SetActive(false);
        how.SetActive(true);
        hows[0].SetActive(true);
        hows[1].SetActive(false);
        hows[2].SetActive(false);
    }

    public void next()
    {
        if (index <= 1)
        {
            hows[index].SetActive(false);
            hows[index + 1].SetActive(true);
            ++index;
        }
    }

    public void previous()
    {
        if (index > 0)
        {
            hows[index].SetActive(false);
            hows[index - 1].SetActive(true);
            index--;
        }
    }


    public void homeScreen()
    {   
        start.SetActive(true);
        how.SetActive(false);
        level.SetActive(false);
        index = 0;
    }

    public static void loadlvl2()
    {
        SceneManager.LoadScene("level2");
    }
    public static void loadlvl1()
    {
        SceneManager.LoadScene("level1");
    }
}

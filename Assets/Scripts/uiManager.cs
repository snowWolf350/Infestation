using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{

    public static uiManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public  GameObject pauseMenu;
    public  GameObject gameMenu;

    private void Start()
    {
        instance.pauseMenu.SetActive(false);
        instance.gameMenu.SetActive(true);
    }

    public static void pause()
    {
        instance.pauseMenu.SetActive(true);
        instance.gameMenu.SetActive(false);
    }
    public static void unpause()
    {
        instance.pauseMenu.SetActive(false);
        instance.gameMenu.SetActive(true);
    }

    public static void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void homeScreen()
    {
        SceneManager.LoadScene("start");
    }
}

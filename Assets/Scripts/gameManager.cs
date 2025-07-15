using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public static Vector3 startPos;
    public static Vector3 endPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public int lives = 3;
    public TextMeshProUGUI livesText;

    private string world;
    public TextMeshProUGUI worldText;

    private int score;
    public TextMeshProUGUI scoreText;

    private int time = 180;
    public TextMeshProUGUI timeText;

    public TextMeshProUGUI coinText;
    private int coins;

    public bool gameOver = false;
    public bool isalive = true;

    public GameObject gameOverScreen;

    public GameObject levelClearScreen;

    public GameObject gameoverScreen;

    private void Start()
    {
        StartCoroutine(countdown());
        gameOverScreen.SetActive(false);
        levelClearScreen.SetActive(false);
    }
    IEnumerator countdown()
    {
        while (!gameOver && time >0)
        {
            time -= 1;
            timeText.text = time.ToString();
            yield return new WaitForSeconds(1);
        }
        if (time == 0)
        {
            instance.gameOverScreen.SetActive(true);
        }
    }
    public static void updateLives()
    {
        if (instance.lives >1)
        {
            instance.lives--;
            instance.livesText.text = instance.lives.ToString();
        }
        else
        {
            instance.gameOver = true;
            instance.gameOverScreen.SetActive(true);
            instance.isalive = false;
        }
    
        
    }

    public static void updateScore(int i)
    {
        instance.score += i;
        instance.scoreText.text = instance.score.ToString();
    }

    public static void setWorld(string c)
    {
        instance.world =c;
        instance.worldText.text = instance.world;
    }

    public static void updateCoin()
    {
        instance.coins++;
        instance.coinText.text = instance.coins.ToString();
    }

    public static void loadlvl2()
    {
        SceneManager.LoadScene("level2");
        setWorld("0-1");
    }
    public static void loadlvl1()
    {
        SceneManager.LoadScene("level1");
        setWorld("0-0");
    }

    public void levelClear()
    {
        instance.gameOver = true;
        StartCoroutine(instance.scoreCalc());
   

        
        
    }

    private IEnumerator scoreCalc()
    {
        while (time > 0)
        {
            score++;
            scoreText.text = score.ToString();
            time--;
            timeText.text = time.ToString();
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(2);
        instance.levelClearScreen.SetActive(true);
    }
  
}

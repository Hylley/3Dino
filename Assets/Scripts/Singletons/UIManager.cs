using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [HideInInspector]public bool gameOver = false;

    [Header("Score")]
    public float score;
    public float scoreIcrement = 5f;
    public Text scoreDisplay;
    public Text highScoreDisplay;

    [Header("Game Over")]
    public GameObject gameOverScreen;
    public KeyCode restartKey = KeyCode.Space;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(PlayerPrefs.HasKey("hiscore"))
        {
            highScoreDisplay.text = "HI " + PlayerPrefs.GetFloat("hiscore").ToString("F0").PadLeft(7, '0');
        }
    }

    void Update()
    {
        if(gameOver & Input.GetKeyDown(restartKey))
        {
            Restart();
            gameOver = false;
        }

        score += scoreIcrement * Time.deltaTime;
        scoreDisplay.text = score.ToString("F0").PadLeft(7, '0');
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void SaveScore()
    {
        if(!PlayerPrefs.HasKey("hiscore"))
        {
            PlayerPrefs.SetFloat("hiscore", score);

            return;
        }

        if(score > PlayerPrefs.GetFloat("hiscore"))
        {
            PlayerPrefs.SetFloat("hiscore", score);
        }
    }
}

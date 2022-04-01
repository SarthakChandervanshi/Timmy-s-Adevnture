using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class GUImanager : MonoBehaviour
{
    public GameObject PauseMenu;
    //public static bool enemyIsDead;
    //public bool antiEnemyIsDead;
    //public static bool enemyBatIsDead;
    public TextMeshProUGUI tmpro_score;
    public TextMeshProUGUI tmpro_coins;
    public static int score;
    public static int coins;

    public static class AppHelper
    {
#if UNITY_WEBPLAYER
     public static string webplayerQuitURL = "http://google.com";
#endif
        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
        }
    }

    void Awake()
    {
        PlayerPrefs.SetInt("levelAt", 7);
        //PlayerPrefs.SetInt("Coins", 0);
    }

    void Start()
    {
        // Debug.Log(PlayerPrefs.GetInt("levelAt"));
        if (PlayerPrefs.GetInt("Score") == null)
        {
            score = 0;
        }
        else
        {
            score = PlayerPrefs.GetInt("Score");
        }

        if(PlayerPrefs.GetInt("Coins") == null)
        {
            coins = 0;
        }
        else
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
    }

    void Update()
    {
        tmpro_score.text = "SCORE:" + score.ToString();
        tmpro_coins.text = "COINS:" + coins.ToString();
    }


    public void Play()
    {
        //PlayerPrefs.SetInt("levelAt", 2);
      SceneManager.LoadScene(PlayerPrefs.GetInt("levelAt"));
    }

    public void TakeOnLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("level");
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("Coins", 0);
    }
    public void Exit()
    {
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Health");
        AppHelper.Quit();
    }
    public void OnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }

    void keluar()
    {
      Application.Quit();
    }

    public void Setting()
    {
      SceneManager.LoadScene("Setting");
    }

    public void OnCredit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credit");
    }
}

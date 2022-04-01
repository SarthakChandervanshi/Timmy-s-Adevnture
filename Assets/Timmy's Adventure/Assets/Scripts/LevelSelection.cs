using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
  public Button[] lvlButtons;
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetInt("levelAt", 2);
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 2 > PlayerPrefs.GetInt("levelAt"))
            {
                lvlButtons[i].interactable = false;
            }
        }

        Debug.Log(PlayerPrefs.GetInt("levelAt"));
    }

    public void levelToLoad(string level)
    {
      SceneManager.LoadScene(level);
    }

}

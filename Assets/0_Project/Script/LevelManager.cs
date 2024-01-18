using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public GameObject mainMenu, gameChoose, game1, game2;
    public int game1Level, game2Level;
    private void Start()
    {
        game1Level = PlayerPrefs.GetInt("game1Level", 0);
        game2Level = PlayerPrefs.GetInt("game2Level", 0);
    }

    public void PlayButton()
    {
        AudioManager.instance.Play("Button");
        mainMenu.SetActive(false);
        gameChoose.SetActive(true);
    }

    public void Game1Button()
    {
        AudioManager.instance.Play("Button");
        gameChoose.SetActive(false);
        game1.transform.GetChild(game1Level).gameObject.SetActive(true);
    }

    public void Game2Button()
    {
        AudioManager.instance.Play("Button");
        gameChoose.SetActive(false);
        game2.transform.GetChild(game2Level).gameObject.SetActive(true);
    }

    public void ExitButton()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }

    public void LoadNextLevel(bool isGame1)
    {
        if (isGame1)
        {
            game1Level++;
            PlayerPrefs.SetInt("game1Level", game1Level);
            game1.transform.GetChild(game1Level - 1).gameObject.SetActive(false);
            game1.transform.GetChild(game1Level).gameObject.SetActive(true);
        }
        else
        {
            game2Level++;
            PlayerPrefs.SetInt("game2Level", game2Level);
            game2.transform.GetChild(game2Level - 1).gameObject.SetActive(false);
            game2.transform.GetChild(game2Level).gameObject.SetActive(true);
        }
        UIManager.instance.CloseWinPanel();
    }
}

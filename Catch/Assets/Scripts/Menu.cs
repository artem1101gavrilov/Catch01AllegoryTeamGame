using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Game;
    public GameObject Topics;

    public void ClickGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickTopics()
    {
        Game.SetActive(false);
        Topics.SetActive(true);
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public void ClickBack()
    {
        Game.SetActive(true);
        Topics.SetActive(false);
    }
}
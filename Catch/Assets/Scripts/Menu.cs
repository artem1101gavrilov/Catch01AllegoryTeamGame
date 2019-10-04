using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Game;
    public GameObject Topics;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ClickGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickTopics()
    {
        Game.active = false;
        Topics.active = true;
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public void ClickBack()
    {
        Game.active = true;
        Topics.active = false;
    }
}
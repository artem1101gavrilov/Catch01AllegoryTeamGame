using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Game;
    public GameObject Skins;
    public GameObject Graphic;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ClickGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickSkins()
    {
        Game.active = false;
        Skins.active = true;
    }

    public void ClickGraphic()
    {
        Game.active = false;
        Graphic.active = true;
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    public void ClickBack()
    {
        Game.active = true;
        Skins.active = false;
        Graphic.active = false;
    }
}
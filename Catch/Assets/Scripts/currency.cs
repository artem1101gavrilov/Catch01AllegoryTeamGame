using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currency : MonoBehaviour
{
    public int coins = 0;
    public Text coin;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        string coins_text = coins.ToString();
        coin.text = "Коины: " + coins_text;
    }
}

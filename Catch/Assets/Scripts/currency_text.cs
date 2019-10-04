using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currency_text : MonoBehaviour
{
    public Text coin;
    private currency cur;
    // Start is called before the first frame update
    void Start()
    {
        cur = GameObject.Find("currency").GetComponent<currency>();
    }

    // Update is called once per frame
    void Update()
    {
        int coins = cur.coins;
        string coins_text = coins.ToString();
        coin.text = "Коины: " + coins_text;
    }
}

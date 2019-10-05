using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currency_text : MonoBehaviour
{
    Text coin;
    private currency cur;
    // Start is called before the first frame update
    void Start()
    {
		coin = GetComponent<Text>();
        cur = GameObject.Find("currency").GetComponent<currency>();
        UpdateCoinText();
    }
	
	public void UpdateCoinText()
	{
		coin.text = cur.coins.ToString();
	}
}

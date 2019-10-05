using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currency : MonoBehaviour
{
    public int coins = 0;
	static currency _currency;
	
	void Awake()
	{
		coins = PlayerPrefs.GetInt("coins", 0);
	}
	
    void Start()
    {
		if(_currency == null)
		{
			_currency = this;
			DontDestroyOnLoad(this);
		}
        else
		{
			Destroy(gameObject);
		}
    }
}

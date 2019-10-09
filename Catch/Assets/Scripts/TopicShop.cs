using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopicShop : MonoBehaviour
{
    private currency coin;
    private Topic topics;
	public currency_text _currency_text;
	
    void Start()
    {
        topics = GameObject.Find("Topic").GetComponent<Topic>();
		coin = GameObject.Find("currency").GetComponent<currency>();
		
		SetColorHex();
    }

	void SetColorHex()
	{
		for(int i = 0; i < 4; ++i)
		{
			transform.GetChild(i).GetChild(0).GetComponent<Image>().color = topics.boughtAllTopics[i] ? new Color(1, 1, 0, 1) : new Color(1, 0, 0, 1);
			if(topics.boughtAllTopics[i]) transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
		}
		transform.GetChild(topics.topic - 1).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
	}
	
    public void ClickTopic1()
    {
        if (coin.coins > 4 && !topics.boughtAllTopics[0])
        {
            coin.coins -= 5;
            topics.topic = 1;
			PlayerPrefs.SetInt("coins", coin.coins);
			topics.boughtAllTopics[0] = true;
			topics.SaveTopics();
			PlayerPrefs.SetInt("topic", 1);
			transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
			_currency_text.UpdateCoinText();
        }
		else if(topics.boughtAllTopics[0])
		{
			topics.topic = 1;
			PlayerPrefs.SetInt("topic", 1);
		}
		SetColorHex();
    }

    public void ClickTopic2()
    {
        if (coin.coins > 9 && !topics.boughtAllTopics[1])
        {
            coin.coins -= 10;
            topics.topic = 2;
			PlayerPrefs.SetInt("coins", coin.coins);
			topics.boughtAllTopics[1] = true;
			topics.SaveTopics();
			PlayerPrefs.SetInt("topic", 2);
			transform.GetChild(2).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
			_currency_text.UpdateCoinText();
        }
		else if(topics.boughtAllTopics[1])
		{
			topics.topic = 2;
			PlayerPrefs.SetInt("topic", 2);
		}
		SetColorHex();
    }

    public void ClickTopic3()
    {
        if (coin.coins > 19 && !topics.boughtAllTopics[2])
        {
            coin.coins -= 20;
            topics.topic = 3;
			PlayerPrefs.SetInt("coins", coin.coins);
			topics.boughtAllTopics[2] = true;
			topics.SaveTopics();
			PlayerPrefs.SetInt("topic", 3);
			transform.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
			_currency_text.UpdateCoinText();
        }
		else if(topics.boughtAllTopics[2])
		{
			topics.topic = 3;
			PlayerPrefs.SetInt("topic", 3);
		}
		SetColorHex();
    }

    public void ClickTopic4()
    {
        topics.topic = 4;
		PlayerPrefs.SetInt("topic", 4);
		SetColorHex();
    }
}

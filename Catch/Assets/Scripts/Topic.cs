using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topic : MonoBehaviour
{
    public int topic = 4; //текущий
	static Topic _Topic;
	private string _boughtAllTopics; //Все купленные топики (сохранение/загрузка)
	public bool [] boughtAllTopics; //Все купленные топики
	
    void Awake()
    {
		if(_Topic == null)
		{
			_Topic = this;
			DontDestroyOnLoad(this);
			LoadTopics();
		}
        else
		{
			Destroy(gameObject);
		}
    }
	
	void LoadTopics()
	{
		_boughtAllTopics = PlayerPrefs.GetString("topics", "0001");
		boughtAllTopics = new bool[_boughtAllTopics.Length];
		for(int i = 0; i < _boughtAllTopics.Length; ++i)
		{
			boughtAllTopics[i] = _boughtAllTopics[i] == '1';
		}
		topic = PlayerPrefs.GetInt("topic", 4);
	}
	
	public void SaveTopics()
	{
		_boughtAllTopics = "";
		for(int i = 0; i < boughtAllTopics.Length; ++i)
		{
			_boughtAllTopics += boughtAllTopics[i] ? "1" : "0";
		}
		PlayerPrefs.SetString("topics", _boughtAllTopics);
	}
}

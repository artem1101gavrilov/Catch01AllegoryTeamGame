using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopicShop : MonoBehaviour
{
    public currency coin;
    public Topic topics;
    // Start is called before the first frame update
    void Start()
    {
        topics = GameObject.Find("Topic").GetComponent<Topic>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ClickTopic1()
    {
        if (coin.coins > 4)
        {
            coin.coins -= 5;
            topics.topic = 1;
        }
    }

    public void ClickTopic2()
    {
        if (coin.coins > 9)
        {
            coin.coins -= 10;
            topics.topic = 2;
        }
    }

    public void ClickTopic3()
    {
        if (coin.coins > 19)
        {
            coin.coins -= 20;
            topics.topic = 3;
        }
    }

    public void ClickTopic4()
    {
        topics.topic = 4;
    }
}

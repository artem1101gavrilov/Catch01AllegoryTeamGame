//using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    //BannerView bannerV;
    //InterstitialAd AdGame;

    private string app = "";
    private string Banner = "";
    private string AD = "";

    /* Что означают цифры в поле
     * 0 - пустота
     * 1 - персонаж
     * 2 - блок
     */
    int[,] Pole = new int[11, 11]; // поле игры
    int[,] ArrayHodov = new int[11, 11]; // поле для алгоритма поиска возможных ходов.
    int PosPandaX = 0, PosPandaY = 0; //позиция панды
    public GameObject CanvasGame;
    bool isFinish;

    bool FirstHod;

    public GameObject Restart;
    public AudioClip impact;
    AudioSource audioSource;

    private void Awake()
    {
        //MobileAds.Initialize(app);
    }

    // Use this for initialization
    void Start () {
        FirstHod = false;

        /*bannerV = new BannerView(Banner, AdSize.Banner, AdPosition.Top);
        AdRequest requestBanner = new AdRequest.Builder().Build();
        bannerV.LoadAd(requestBanner);

        AdGame = new InterstitialAd(AD);
        AdRequest requestAD = new AdRequest.Builder().Build();
        AdGame.LoadAd(requestAD);*/


        audioSource = GetComponent<AudioSource>();

        isFinish = false;
        //Pole = new int[11, 11];
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                Pole[i, j] = 0;
            }
        }
        Pole[5, 5] = 1;
        PosPandaX = 5;
        PosPandaY = 5;
        for (int i = 0; i < 11; i++)
        {
            int x = Random.Range(0, 11);
            int y = Random.Range(0, 11);
            if (Pole[x, y] == 0) Pole[x, y] = 2;
        }
        for (int j = 0; j < 11; j++)
        {
            for (int i = 0; i < 11; i++)
            {
                if (j % 2 == 0)
                {
                    //Instantiate(hex, new Vector3(0.9f * i - 6f, 0.7f * j - 4f, 0), new Quaternion(), transform.GetChild(0).transform);
                    //Для 1280 на 800
                    //GameObject h = Instantiate(Resources.Load("Hex"), new Vector3(0.7f * i - 6f, 0.57f * j - 4f, 0), Quaternion.identity, transform.GetChild(0).transform) as GameObject;
                    GameObject h = Instantiate(Resources.Load("Hex"), new Vector3(0.835f * i - 4.5f, - 0.9f - (0.7f * j - 4f), 0), Quaternion.identity, transform.GetChild(0).transform) as GameObject;
                    h.name = (11 * i + j).ToString();
                    h.transform.GetChild(0).gameObject.SetActive(false);
                    h.transform.GetChild(1).gameObject.SetActive(false);
                    if (Pole[i, j] == 1) h.transform.GetChild(0).gameObject.SetActive(true);
                    if (Pole[i, j] == 2) h.transform.GetChild(1).gameObject.SetActive(true);
                    h.GetComponent<Button>().onClick.AddListener(delegate { ClichHex(h); });
                    //h.transform.SetParent(transform.GetChild(0).transform, false);
                }
                else
                {
                    //Для 1280 на 800
                    //GameObject h = Instantiate(Resources.Load("Hex"), new Vector3(0.7f * i - 6f + 0.35f, 0.57f * j - 4f, 0), Quaternion.identity, transform.GetChild(0).transform) as GameObject;
                    GameObject h = Instantiate(Resources.Load("Hex"), new Vector3(0.835f * i - 4.5f + 0.395f, - 0.9f - (0.7f * j - 4f), 0), Quaternion.identity, transform.GetChild(0).transform) as GameObject;
                    h.name = (11 * i + j).ToString();
                    h.transform.GetChild(0).gameObject.SetActive(false);
                    h.transform.GetChild(1).gameObject.SetActive(false);
                    if (Pole[i, j] == 1) h.transform.GetChild(0).gameObject.SetActive(true);
                    if (Pole[i, j] == 2) h.transform.GetChild(1).gameObject.SetActive(true);
                    h.GetComponent<Button>().onClick.AddListener(delegate { ClichHex(h); });
                    //h.transform.SetParent(transform.GetChild(0).transform, false);
                }
            }
        }


        //transform.GetChild(1).transform.position = transform.GetChild(0).transform.GetChild(11 * (PosPandaX) + PosPandaY).transform.GetChild(0).transform.position;
        //transform.GetChild(1).transform.position = new Vector2(transform.GetChild(1).transform.position.x,
        //                                                        transform.GetChild(1).transform.position.y + 0.3f);
    }

    public void ClichHex(GameObject h)
    {
        if (!isFinish)
        {
            int x = int.Parse(h.name) / 11;
            int y = int.Parse(h.name) % 11;
            if (Pole[x, y] == 0)
            {
                audioSource.PlayOneShot(impact, 1F);
                h.transform.GetChild(1).gameObject.SetActive(true);
                Pole[x, y] = 2;
                HodPanda();
            }
        }
    }

    void HodPanda()
    {
        //Если панда находится в центре
        if (!FirstHod)
        {
            FirstHodPanda();
            FirstHod = true;
        }
        else
        {
            ArraySearch();
            AlgoritmHodPanda();
            //Рандом
            //if (PosPandaY % 2 == 1) FirstHodPanda();
            //else ExampleHodPAnda();
        }
    }

    void FirstHodPanda()
    {
        bool isHod = true;
        while (isHod)
        {
            int rand = Random.Range(0, 6);
            switch (rand)
            {
                case 0:
                    //налево
                    if (PosPandaX > 0)
                    {
                        if (Pole[PosPandaX - 1, PosPandaY] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX - 1, PosPandaY] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX - 1) + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaX--;
                            isHod = false;
                        }
                    }
                    break;
                case 1:
                    //направо
                    if (PosPandaX < 11)
                    {
                        if (Pole[PosPandaX + 1, PosPandaY] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX + 1, PosPandaY] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX + 1) + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaX++;
                            isHod = false;
                        }
                    }
                    break;
                case 2:
                    //вверх
                    if (PosPandaY < 11)
                    {
                        if (Pole[PosPandaX, PosPandaY + 1] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX, PosPandaY + 1] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX) + PosPandaY + 1).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaY++;
                            isHod = false;
                        }
                    }
                    break;
                case 3:
                    //Вниз
                    if (PosPandaY > 0)
                    {
                        if (Pole[PosPandaX, PosPandaY - 1] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX, PosPandaY - 1] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX) + PosPandaY - 1).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaY--;
                            isHod = false;
                        }
                    }
                    break;
                case 4:
                    //
                    if (PosPandaX < 11 && PosPandaY < 11)
                    {
                        if (Pole[PosPandaX + 1, PosPandaY + 1] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX + 1, PosPandaY + 1] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX + 1) + PosPandaY + 1).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaY++;
                            PosPandaX++;
                            isHod = false;
                        }
                    }
                    break;
                case 5:
                    if (PosPandaX < 11 && PosPandaY > 0)
                    {
                        if (Pole[PosPandaX + 1, PosPandaY - 1] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX + 1, PosPandaY - 1] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX + 1) + PosPandaY - 1).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaY--;
                            PosPandaX++;
                            isHod = false;
                        }
                    }
                    break;
            }
            //transform.GetChild(1).transform.position = transform.GetChild(0).transform.GetChild(11 * (PosPandaX) + PosPandaY).transform.GetChild(0).transform.position;
            //transform.GetChild(1).transform.position = new Vector2(transform.GetChild(1).transform.position.x,
             //                                                       transform.GetChild(1).transform.position.y + 0.3f);
        }
    }

    void ExampleHodPAnda()
    {
        bool isHod = true;
        while (isHod)
        {
            int rand = Random.Range(0, 6);
            switch (rand)
            {
                case 0:
                    //налево
                    if (PosPandaX > 0)
                    {
                        if (Pole[PosPandaX - 1, PosPandaY] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX - 1, PosPandaY] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX - 1) + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaX--;
                            isHod = false;
                        }
                    }
                    break;
                case 1:
                    //направо
                    if (PosPandaX < 11)
                    {
                        if (Pole[PosPandaX + 1, PosPandaY] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX + 1, PosPandaY] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX + 1) + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaX++;
                            isHod = false;
                        }
                    }
                    break;
                case 2:
                    //вверх
                    if (PosPandaY < 11)
                    {
                        if (Pole[PosPandaX, PosPandaY + 1] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX, PosPandaY + 1] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX) + PosPandaY + 1).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaY++;
                            isHod = false;
                        }
                    }
                    break;
                case 3:
                    //Вниз
                    if (PosPandaY > 0)
                    {
                        if (Pole[PosPandaX, PosPandaY - 1] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX, PosPandaY - 1] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX) + PosPandaY - 1).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaY--;
                            isHod = false;
                        }
                    }
                    break;
                case 4:
                    //
                    if (PosPandaX > 0 && PosPandaY > 0)
                    {
                        if (Pole[PosPandaX - 1, PosPandaY - 1] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX - 1, PosPandaY - 1] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX - 1) + PosPandaY - 1).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaY--;
                            PosPandaX--;
                            isHod = false;
                        }
                    }
                    break;
                case 5:
                    if (PosPandaX > 0 && PosPandaY < 11)
                    {
                        if (Pole[PosPandaX - 1, PosPandaY + 1] == 0)
                        {
                            Pole[PosPandaX, PosPandaY] = 0;
                            Pole[PosPandaX - 1, PosPandaY + 1] = 1;
                            transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                            transform.GetChild(0).transform.FindChild((11 * (PosPandaX - 1) + PosPandaY + 1).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                            PosPandaY++;
                            PosPandaX--;
                            isHod = false;
                        }
                    }
                    break;
            }
        }
        //transform.GetChild(1).transform.position = transform.GetChild(0).transform.GetChild(11 * (PosPandaX) + PosPandaY).transform.GetChild(0).transform.position;
        //transform.GetChild(1).transform.position = new Vector2(transform.GetChild(1).transform.position.x,
                                                                //transform.GetChild(1).transform.position.y + 0.3f);
    }

    //Составление массива ходов
    void ArraySearch()
    {
        bool isCircle = true; // условие цикла

        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (Pole[i, j] == 2) ArrayHodov[i, j] = -1;
                else ArrayHodov[i, j] = -2; //не обследована
            }
        }
        ArrayHodov[PosPandaX, PosPandaY] = 0;
        int currentHod = 0;
        bool isNewCurrent = false;
        while (isCircle)
        {
            isNewCurrent = false;
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if(ArrayHodov[i, j] == currentHod)
                    {
                        if (i > 0)
                        {
                            if (Pole[i - 1, j] == 0 && ArrayHodov[i - 1, j] == -2)
                            {
                                ArrayHodov[i - 1, j] = currentHod + 1;
                                isNewCurrent = true;
                            }
                        }
                        if (i < 10)
                        {
                            if (Pole[i + 1, j] == 0 && ArrayHodov[i + 1, j] == -2)
                            {
                                ArrayHodov[i + 1, j] = currentHod + 1;
                                isNewCurrent = true;
                            }
                        }
                        if (j > 0)
                        {
                            if (Pole[i, j - 1] == 0 && ArrayHodov[i, j - 1] == -2)
                            {
                                ArrayHodov[i, j - 1] = currentHod + 1;
                                isNewCurrent = true;
                            }
                        }
                        if (j < 10)
                        {
                            if (Pole[i, j + 1] == 0 && ArrayHodov[i, j + 1] == -2)
                            {
                                ArrayHodov[i, j + 1] = currentHod + 1;
                                isNewCurrent = true;
                            }
                        }
                        if (j%2 == 0)
                        {
                            if(i > 0 && j > 0)
                            {
                                if (Pole[i - 1, j - 1] == 0 && ArrayHodov[i - 1, j - 1] == -2)
                                {
                                    ArrayHodov[i - 1, j - 1] = currentHod + 1;
                                    isNewCurrent = true;
                                }
                            }
                            if (i > 0 && j < 10)
                            {
                                if (Pole[i - 1, j + 1] == 0 && ArrayHodov[i - 1, j + 1] == -2)
                                {
                                    ArrayHodov[i - 1, j + 1] = currentHod + 1;
                                    isNewCurrent = true;
                                }
                            }
                        }
                        else
                        {
                            if (i < 10 && j < 10)
                            {
                                if (Pole[i + 1, j + 1] == 0 && ArrayHodov[i + 1, j + 1] == -2)
                                {
                                    ArrayHodov[i + 1, j + 1] = currentHod + 1;
                                    isNewCurrent = true;
                                }
                            }
                            if (i < 10 && j > 0)
                            {
                                if (Pole[i + 1, j - 1] == 0 && ArrayHodov[i + 1, j - 1] == -2)
                                {
                                    ArrayHodov[i + 1, j - 1] = currentHod + 1;
                                    isNewCurrent = true;
                                }
                            }
                        }
                    }
                }
            }
            if (isNewCurrent) currentHod++;
            else isCircle = false;
        }
        if (currentHod == 0)
        {
            //if(AdGame.IsLoaded()) AdGame.Show();
            isFinish = true;
            Restart.transform.GetChild(0).gameObject.SetActive(true);
            Restart.SetActive(true);
        }
    }

    void AlgoritmHodPanda()
    {
        if (!isFinish)
        {
            int maxHod = -2;
            int MaxHodX = 0, MaxHodY = 0;
            for (int i = 0; i < 11; i++)
            {
                if (ArrayHodov[i, 0] > -1)
                {
                    if ((maxHod == -2) || (ArrayHodov[i, 0] < maxHod))
                    {
                        maxHod = ArrayHodov[i, 0];
                        MaxHodX = i;
                        MaxHodY = 0;
                    }
                }
                if (ArrayHodov[i, 10] > -1)
                {
                    if ((maxHod == -2) || (ArrayHodov[i, 10] < maxHod))
                    {
                        maxHod = ArrayHodov[i, 10];
                        MaxHodX = i;
                        MaxHodY = 10;
                    }
                }
                if (ArrayHodov[0, i] > -1)
                {
                    if ((maxHod == -2) || (ArrayHodov[0, i] < maxHod))
                    {
                        maxHod = ArrayHodov[0, i];
                        MaxHodX = 0;
                        MaxHodY = i;
                    }
                }
                if (ArrayHodov[10, i] > -1)
                {
                    if ((maxHod == -2) || (ArrayHodov[10, i] < maxHod))
                    {
                        maxHod = ArrayHodov[10, i];
                        MaxHodX = 10;
                        MaxHodY = i;
                    }
                }
            }
            //Если нет возможностей для ухода панды, то берем рандом
            if (maxHod == -2)
            {
                if (PosPandaY % 2 == 1) FirstHodPanda();
                else ExampleHodPAnda();
            }
            else if(maxHod == 1)
            {
                //AdGame.Show();
                Pole[PosPandaX, PosPandaY] = 0;
                Pole[MaxHodX, MaxHodY] = 1;
                transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).transform.FindChild((11 * (MaxHodX) + MaxHodY).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                //transform.GetChild(1).transform.position = transform.GetChild(0).transform.GetChild(11 * (MaxHodX) + MaxHodY).transform.GetChild(0).transform.position;
                //transform.GetChild(1).transform.position = new Vector2(transform.GetChild(1).transform.position.x,
                //                                                        transform.GetChild(1).transform.position.y + 0.3f);
                isFinish = true;
                Restart.transform.GetChild(1).gameObject.SetActive(true);
                Restart.SetActive(true);
            }
            else
            {
                while(maxHod != 1)
                {
                    int rand = Random.Range(0, 6);
                    switch (rand)
                    {
                        case 0:
                            if (MaxHodX > 0)
                            {
                                if (ArrayHodov[MaxHodX - 1, MaxHodY] == (maxHod - 1))
                                {
                                    MaxHodX--;
                                    maxHod--;
                                }
                            }
                            break;
                        case 1:
                            if (MaxHodX < 10)
                            {
                                if (ArrayHodov[MaxHodX + 1, MaxHodY] == (maxHod - 1))
                                {
                                    MaxHodX++;
                                    maxHod--;
                                }
                            }
                            break;
                        case 2:
                            if (MaxHodY > 0)
                            {
                                if (ArrayHodov[MaxHodX, MaxHodY - 1] == (maxHod - 1))
                                {
                                    MaxHodY--;
                                    maxHod--;
                                }
                            }
                            break;
                        case 3:
                            if (MaxHodY < 10)
                            {
                                if (ArrayHodov[MaxHodX, MaxHodY + 1] == (maxHod - 1))
                                {
                                    MaxHodY++;
                                    maxHod--;
                                }
                            }
                            break;
                        case 4:
                            if(MaxHodY % 2 == 0)
                            {
                                if (MaxHodX > 0 && MaxHodY > 0)
                                {
                                    if (ArrayHodov[MaxHodX - 1, MaxHodY - 1] == (maxHod - 1))
                                    {
                                        MaxHodX--;
                                        MaxHodY--;
                                        maxHod--;
                                    }
                                }
                            }
                            else
                            {
                                if (MaxHodX < 10 && MaxHodY < 10)
                                {
                                    if (ArrayHodov[MaxHodX + 1, MaxHodY + 1] == (maxHod - 1))
                                    {
                                        MaxHodX++;
                                        MaxHodY++;
                                        maxHod--;
                                    }
                                }
                            }
                            break;
                        case 5:
                            if (MaxHodY % 2 == 0)
                            {
                                if (MaxHodX > 0 && MaxHodY < 10)
                                {
                                    if (ArrayHodov[MaxHodX - 1, MaxHodY + 1] == (maxHod - 1))
                                    {
                                        MaxHodX--;
                                        MaxHodY++;
                                        maxHod--;
                                    }
                                }
                            }
                            else
                            {
                                if (MaxHodX < 10 && MaxHodY > 0)
                                {
                                    if (ArrayHodov[MaxHodX + 1, MaxHodY - 1] == (maxHod - 1))
                                    {
                                        MaxHodX++;
                                        MaxHodY--;
                                        maxHod--;
                                    }
                                }
                            }
                            break;
                    }
                }
                Pole[PosPandaX, PosPandaY] = 0;
                Pole[MaxHodX, MaxHodY] = 1;
                transform.GetChild(0).transform.FindChild((11 * PosPandaX + PosPandaY).ToString()).transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).transform.FindChild((11 * (MaxHodX) + MaxHodY).ToString()).transform.GetChild(0).gameObject.SetActive(true);
                //transform.GetChild(1).transform.position = transform.GetChild(0).transform.GetChild(11 * (MaxHodX) + MaxHodY).transform.GetChild(0).transform.position;
                //transform.GetChild(1).transform.position = new Vector2(transform.GetChild(1).transform.position.x,
                //                                                        transform.GetChild(1).transform.position.y + 0.3f);
                PosPandaX = MaxHodX;
                PosPandaY = MaxHodY;
            }
        }
    }
    
    public void RestarGame()
    {
        SceneManager.LoadScene(0);
    }
}

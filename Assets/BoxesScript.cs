using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BoxesScript : MonoBehaviour
{
    public const int LEVEL_COUNT = 100;
    public Texture OpenLevel;
    public Texture LockLevel;
    public Texture OneStar;
    public Texture TwoStar;
    public Texture ThreeStar;
    public GameObject[] Boxes;
    private Scrollbar scrollbar;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Level1"))
        {
            PlayerPrefs.SetInt("Level1", 0);
            for (int i = 2; i <= LEVEL_COUNT; i++)
            {
                PlayerPrefs.SetInt("Level" + i, -1);
            }
        }
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        ShowBoxes();

    }
    public class ApplicationModel
    {
        public static int LoadLevel = -1;
        public static string LastBlockMoved = "";
        public static int steps = 0;
    }
    public static void LoadLevel(int number)
    {
        ApplicationModel.LoadLevel = number;
        ApplicationModel.steps = 0;
        ApplicationModel.LastBlockMoved = "";
        Application.LoadLevel("GameScene");
    }
    public void LoadLvlv(int number)
    {
        ApplicationModel.LoadLevel = number;
        Application.LoadLevel("GameScene");
    }
    public void ShowBuyMessage(int number)
    {
        var obj = GameObject.Find("LevelMenu").GetComponent<ShowMessageScript>();
        obj.Message.SetActive(true);
        obj.LevelNumber = number;

    }
    public float move = 0;
    public void ShowLevels(float vector)
    {
        if (move == 0)
        {
            if (vector > 0 && vector < 0.81)
            {
                move = vector;
            }
            else if (vector < 0)
            {
                move = vector;
            }
        }

    }
    void Update()
    {
        if (move > 0.01f)
        {
            move -= 0.02f;
            if (scrollbar.value <= 0.94f)
            scrollbar.value += 0.02f;
        }
        else if (move < -0.01f)
        {
            move += 0.02f;
            scrollbar.value -= 0.02f;
        }
        else
        {
            move = 0;
        }
    }
    public void ShowBoxes()
    {
        int count = 1;
        for (int i = 0; i < Boxes.Length; i++)
        {
            for (int j = 1; j <= 15 && count <= LEVEL_COUNT; j++)
            {
                int stats = -1;
                var box = Boxes[i].transform.FindChild("Box (" + j + ")");
                if (PlayerPrefs.HasKey("Level" + (count)))
                {
                    stats = PlayerPrefs.GetInt("Level" + (count));

                    switch (stats)
                    {
                        case -1:
                            box.GetComponent<RawImage>().texture = LockLevel;
                            break;
                        case 0:
                            box.GetComponent<RawImage>().texture = OpenLevel;
                            break;
                        case 1:
                            box.GetComponent<RawImage>().texture = OneStar;
                            break;
                        case 2:
                            box.GetComponent<RawImage>().texture = TwoStar;
                            break;
                        case 3:
                            box.GetComponent<RawImage>().texture = ThreeStar;
                            break;
                    }
                }
                box.FindChild("Text").GetComponent<Text>().text = (count).ToString();
                box.GetComponent<Button>().onClick.RemoveAllListeners();
                var count1 = count;
                if (stats != -1)
                {
                    box.GetComponent<Button>().onClick.AddListener(() => LoadLvlv(count1));
                }
                else
                {
                    box.GetComponent<Button>().onClick.AddListener(() => ShowBuyMessage(count1));
                }
                count++;
            }
        }
    }
}

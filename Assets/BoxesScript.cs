using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BoxesScript : MonoBehaviour
{
    public Texture BoxTextureClosed;
    public Texture BoxTextureOpen;
    // Use this for initialization
    void Start()
    {
        ShowBoxes();
    }
    public class ApplicationModel
    {
        static public int LoadLevel = 1;
        public static string LastBlockMoved = "";
        public static int steps =0;
    }
    public void LoadLvlv(int number)
    {
        ApplicationModel.LoadLevel = number;
        Application.LoadLevel("GameScene");
    }

    public void ShowBoxes()
    {
        int levels = 1;
        if (PlayerPrefs.HasKey("Levels"))
        {
            levels = PlayerPrefs.GetInt("Levels");
        }
        else
        {
            PlayerPrefs.SetInt("Levels", 1);
        }
        int count = 1;
        for (int i = 1; i <= 7; i++)
        {
            for (int j = 1; j <= 15 && count <= 100; j++)
            {
                var box = GameObject.Find("Boxes (" + i + ")").transform.FindChild("Box (" + j + ")");
                box.FindChild("Text").GetComponent<Text>().text = (count).ToString();
                box.GetComponent<Button>().onClick.RemoveAllListeners();
                var count1 = count;
                box.GetComponent<Button>().onClick.AddListener(()=> LoadLvlv(count1));
                count++;
            }
        }
    }
}

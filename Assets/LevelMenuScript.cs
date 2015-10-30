using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;

public class LevelMenuScript : MonoBehaviour {
    public Texture OpenLevel;
    public Texture LockLevel;
    public Texture OneStar;
    public Texture TwoStar;
    public Texture ThreeStar;
    public float VerticalSpace;
    public float HorisontalSpace;
    public float BlockSpace;
    bool inited = false;
    List<GameObject> buttons;
    void Init()
    {
        string level = "Level1";
        while (PlayerPrefs.HasKey(level))
        {
            var item = GameObject.Find("Boxes (" + (PlayerPrefs.GetInt(level) % 15 == 0 ? (PlayerPrefs.GetInt(level) / 15) :
                (PlayerPrefs.GetInt(level) / 15 + 1)) +
                ")").transform.FindChild("Box (" + (PlayerPrefs.GetInt(level) - 15 * (PlayerPrefs.GetInt(level)) / 15) +
                ")").GetComponent<RawImage>();
            var stats = PlayerPrefs.GetInt(level);
            if (stats == -1)
            {
                item.texture = LockLevel;
            }
            else if (stats == 0)
            {
                item.texture = OpenLevel;
            }
            else if (stats == 1)
            {
                item.texture = OneStar;
            }
            else if (stats == 2)
            {
                item.texture = TwoStar;
            }
            else if (stats == 3)
            {
                item.texture = ThreeStar;
            }
            level = "Level" + (int.Parse(level.Remove(0, 5)) + 1).ToString();
        }
    }
	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Level1"))
        {
            PlayerPrefs.SetInt("Level1", 0);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (!inited)
        {
            Init();
            inited = true;
        }
    }
}

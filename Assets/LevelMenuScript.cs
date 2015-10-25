using UnityEngine;
using System.Collections.Generic;

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
        GameObject first = GameObject.Find("LevelIcon");
        buttons = new List<GameObject>();
        GameObject newObj;
        buttons.Add(first);
        for (int i = 2; i <= 100; i++)
        {
            // width
            if ((i - 1) % 3 != 0)
            {
                newObj = Instantiate(buttons[buttons.Count - 1]);
                newObj.transform.parent = this.transform;
                newObj.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>().text = i.ToString();
                newObj.GetComponent<RectTransform>().position = buttons[buttons.Count - 1].GetComponent<RectTransform>().position;
                newObj.GetComponent<RectTransform>().position = new Vector3(newObj.GetComponent<RectTransform>().position.x + HorisontalSpace, newObj.GetComponent<RectTransform>().position.y, newObj.GetComponent<RectTransform>().position.z);
                buttons.Add(newObj);
            }
            else if ((i - 1) % 12 != 0)
            {
                // in new line
                newObj = Instantiate(buttons[buttons.Count - 3]);
                newObj.transform.parent = this.transform;
                newObj.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>().text = i.ToString();
                newObj.GetComponent<RectTransform>().position = buttons[buttons.Count - 3].GetComponent<RectTransform>().position;
                newObj.GetComponent<RectTransform>().position = new Vector3(newObj.GetComponent<RectTransform>().position.x, newObj.GetComponent<RectTransform>().position.y - VerticalSpace, newObj.GetComponent<RectTransform>().position.z);
                buttons.Add(newObj);
            }
            else
            {
                newObj = Instantiate(buttons[buttons.Count - 10]);
                newObj.transform.parent = this.transform;
                newObj.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>().text = i.ToString();
                newObj.GetComponent<RectTransform>().position = buttons[buttons.Count - 10].GetComponent<RectTransform>().position;
                newObj.GetComponent<RectTransform>().position = new Vector3(newObj.GetComponent<RectTransform>().position.x + BlockSpace, newObj.GetComponent<RectTransform>().position.y, newObj.GetComponent<RectTransform>().position.z);
                buttons.Add(newObj);
            }

        }
        buttons[0].GetComponent<UnityEngine.UI.RawImage>().texture = OneStar;
        buttons[1].GetComponent<UnityEngine.UI.RawImage>().texture = TwoStar;
        buttons[2].GetComponent<UnityEngine.UI.RawImage>().texture = ThreeStar;
        buttons[3].GetComponent<UnityEngine.UI.RawImage>().texture = OpenLevel;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!inited)
        {
            //Init();
            inited = true;
        }
    }
}

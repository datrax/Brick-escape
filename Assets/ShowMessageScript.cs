using UnityEngine;
using System.Collections;

public class ShowMessageScript : MonoBehaviour {
    public GameObject Message;
    private int levelNumber;
    public int LevelNumber
    {
        get
        {
            return levelNumber;
        }
        set
        {
            levelNumber = value;
            Message.transform.FindChild("Level").GetComponent<UnityEngine.UI.Text>().text = "level " + value;
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;

public class LevelMenuScript : MonoBehaviour {

    public float VerticalSpace;
    public float HorisontalSpace;
    public float BlockSpace;
    bool inited = false;
    List<GameObject> buttons;
    void Init()
    {
        
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

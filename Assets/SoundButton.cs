using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {
    public Sprite SoundOff;
    public Sprite SoundOn;
    public bool Sound;
    public void MouseClick()
    {
        Sound = !Sound;
        PlayerPrefs.SetInt("Sound", Sound ? 1 : 0);
        GetComponent<Image>().sprite = Sound ? SoundOn : SoundOff;
    }
	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
            Sound = true;
        }
        else
        {
            Sound = PlayerPrefs.GetInt("Sound") == 1 ?  Sound = true : Sound = false;
            GetComponent<Image>().sprite = Sound ? SoundOn : SoundOff;
        }
	}
	
}

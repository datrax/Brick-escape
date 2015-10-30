using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Assets.Scripts;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using ThreadPriority = System.Threading.ThreadPriority;

public class SolveThePuzzle : MonoBehaviour
{
    public void ReduceHintCount()
    {
        if (PlayerPrefs.HasKey("Hints"))
        {
            var hints = GameObject.Find("Solver").transform.GetChild(0).GetComponent<Text>();
            hints.text = (int.Parse(hints.text) - 1).ToString();
            PlayerPrefs.SetInt("Hints", int.Parse(hints.text));
        }
    }
    // Use this for initialization
    void Start()
    {
        GameObject.Find("LoadAnimation").GetComponent<SpriteRenderer>().enabled = (false);
    }

    void OnApplicationQuit()
    {
        if (t != null)
        {
            t.Abort();
        }
    }

    public bool solved = false;
    public bool solving = false;
    int time = 0;
    // Update is called once per frame
    void Update()
    {
        if (solving && time == 1)
        {

            GameObject.Find("LoadAnimation").transform.Rotate(0, 0, 25.7f);
            time = 0;
        }
        else
        {
            time = 1;
        }
        if (solved)
        {
            solved = false;
            KeepSolving();
            GameObject.Find("LoadAnimation").GetComponent<SpriteRenderer>().enabled = (false);
        }
    }

    private List<string> steps = new List<string>();
    public int stepNumber = 0;
    private Thread t;
    public void Solve()
    {
        if(solving)return;
        if (PlayerPrefs.GetInt("Hints") >= 1)
        {
            solving = true;
            steps = Keeper.solvers[BoxesScript.ApplicationModel.LoadLevel - 1];
            BoxesScript.ApplicationModel.steps = 0;
            GameObject.Find("Step").GetComponent<Text>().text = "moves : " + BoxesScript.ApplicationModel.steps;
            SetOldBlocksNotActive();
            GameObject.Find("Canvas").GetComponent<Initializer>().LoadLevel(BoxesScript.ApplicationModel.LoadLevel);
            KeepSolving();
            ReduceHintCount();
        }

    }



    public void KeepSolving()
    {
        var t = steps[stepNumber];
        var objs = GameObject.FindGameObjectsWithTag("Block");
        foreach (var ob in objs)
        {
            if (ob.GetComponent<BlockScript>().codeName == t.Substring(0, 4))
            {
                int x = Int32.Parse(t.Substring(6, 1));
                int y = Int32.Parse(t.Substring(7, 1));
                ob.GetComponent<BlockScript>().StartMovingTo(x, y);
                stepNumber++;
                return;
            }
        }


    }
    private void SetOldBlocksNotActive()
    {
        var objs = GameObject.FindGameObjectsWithTag("Block");
        for (var i = 0; i < objs.Length; i++)
        {

            objs[i].gameObject.SetActive(false);
        }
    }
   
}

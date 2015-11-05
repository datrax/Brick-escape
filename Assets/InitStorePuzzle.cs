using UnityEngine;
using System.Collections;
using Soomla.Store;
using PuzzleStore;

    public class InitStorePuzzle : MonoBehaviour
    {

    // Use this for initialization
    void Start()
    {
        SoomlaStore.Initialize(new PuzzleStoreAssets());//Передаем при инициализации наш объект с данными о паках
        print("INITED00");
#if UNITY_ANDROID
            SoomlaStore.StartIabServiceInBg(); //Обязательно для Android, без этого ничего работать не будет
#endif
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

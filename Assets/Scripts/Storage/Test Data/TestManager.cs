using Elementary.Scripts.Data.Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    private bool _isFirstTimeStart;
    private SOTestData _saveData;
    private int current;

    private void Awake()
    {
        HandleLoadData();
        Debug.Log(_saveData.TestValue);
        Debug.Log(_saveData.CurrentTestValue);
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            _saveData.TestValue +=5;
            Debug.Log(_saveData.TestValue);
            SaveData();
        }
    }



    private void HandleLoadData()
    {
        var loadedData = DataManager.GetWithJson<SOTestData>();
        if (loadedData == null)
        {
            _isFirstTimeStart = true;
            _saveData = new SOTestData()
            {
                TestValue = current,
            };

            SaveData();
        }
        else
            _saveData = loadedData;
    }

    private void SaveData()
    {
        DataManager.SaveWithJson(_saveData);
    }

}

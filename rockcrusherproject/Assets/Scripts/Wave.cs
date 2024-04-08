using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public bool ComboEnabled => comboEnabled;

    [SerializeField] private bool comboEnabled = false;

    public int ReturnRocksCountInWave()
    {
        int rocksCount = 0;

        foreach(Transform childTransform in transform)
        {
            if(childTransform.gameObject.GetComponent<Rock>()!=null)rocksCount++;
        }

        return rocksCount;
    }
}

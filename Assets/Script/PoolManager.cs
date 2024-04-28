using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    void Awake()
    {
        pools=new List<GameObject>[prefabs.Length];

        for(int index=0; index<pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

        Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf) //비활성화 되있으면
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        
        if(select==null) //활성화 되있으면
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

}

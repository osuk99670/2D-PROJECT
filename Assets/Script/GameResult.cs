using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult: MonoBehaviour
{
    public GameObject[] titles;
    
    public void Over()
    {
        titles[0].SetActive(true);
    }
}

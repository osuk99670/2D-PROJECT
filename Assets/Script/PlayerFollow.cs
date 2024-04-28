using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        rectTransform.position=Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position); 
    }
}

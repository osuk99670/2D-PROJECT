using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
public class Hud : MonoBehaviour
{
    public enum InfoType { exp,level,kill,time,health}
    public InfoType type;

    Text text;
    Slider slider;

    void Awake()
    {
        text = GetComponent<Text>();
        slider= GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch(type)
        {
            case InfoType.exp:
                float currentExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                slider.value = currentExp / maxExp;
                break;

            case InfoType.level:
                text.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;

            case InfoType.kill:
                text.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;

            case InfoType.time:
                int min = Mathf.FloorToInt(GameManager.instance.gameTime/60);
                int sec= Mathf.FloorToInt(GameManager.instance.gameTime%60);
                text.text = string.Format("{0:D2}:{1:D2}", min,sec);
                break;

            case InfoType.health:
                float currentHp = GameManager.instance.health;
                float maxHP = GameManager.instance.maxHealth;
                slider.value = currentHp / maxHP;
                break;
        }
    }
}

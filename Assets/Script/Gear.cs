using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;  //아이템 타입
    public float rate; //

    public void Init(ItemData data) //초기화
    {
        name = "Gear" + data.itemId; 
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        type = data.type;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch(type)
        {
            case ItemData.ItemType.Glove://장갑
                RateUp(); //무기 속도 상승
                break;

            case ItemData.ItemType.Shoe: //신발
                SpeedUp(); //플레이어 속도상승
                break;


        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons) 
        {
            switch(weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                default:
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate; //플레이어 속도 증가
    }

}

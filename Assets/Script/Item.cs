using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public ItemData data;  //아이템 데이터
    public int level; //레벨
    public Weapon weapon; //무기;
    public Gear gear;  //기어

    Image image; //이미지
    Text itemLevel; //아이템 레벨
    Text itemName; //아이템 이름
    Text itemDesc; //아이템 설명

    void Awake()
    {
        image = GetComponentsInChildren<Image>()[1];
        image.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        itemLevel = texts[0];
        itemName = texts[1];
        itemDesc = texts[2];
        itemName.text = data.itemName;
    }

    void OnEnable()
    {
        itemLevel.text = "Lv." + (level + 1);

        switch (data.type)
        {
            case ItemData.ItemType.Sword:
            case ItemData.ItemType.Fire:
                itemDesc.text = string.Format(data.itemDesc, data.damages[level]*100, data.counts[level]);
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                itemDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;

            case ItemData.ItemType.Shiled:
                itemDesc.text = string.Format(data.itemDesc, data.damages[level], data.sizes[level]);
                break;

            default:
                itemDesc.text = string.Format(data.itemDesc);
                break;

        }

     
    }

    public void OnClick() //클릭
    {
        switch (data.type)
        {
            case ItemData.ItemType.Sword:
            case ItemData.ItemType.Fire:
                if (level == 0) 
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.damage;
                    int nextCount = 0;
                    nextDamage += data.damage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount,0);

                }
                level++;
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;

            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;

            case ItemData.ItemType.Shiled:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.damages[level];
                    float nextSize = data.sizes[level];
                    weapon.LevelUp(nextDamage, 0, nextSize); //크기 증가

                }
                level++;
                break;
        }


        if(level==data.damages.Length) 
        {
            GetComponent<Button>().interactable = false; //최대 레벨 도달하면 버튼 비활성화
        }

    }

}

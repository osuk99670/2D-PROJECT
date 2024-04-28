using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;  //������ Ÿ��
    public float rate; //

    public void Init(ItemData data) //�ʱ�ȭ
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
            case ItemData.ItemType.Glove://�尩
                RateUp(); //���� �ӵ� ���
                break;

            case ItemData.ItemType.Shoe: //�Ź�
                SpeedUp(); //�÷��̾� �ӵ����
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
        GameManager.instance.player.speed = speed + speed * rate; //�÷��̾� �ӵ� ����
    }

}

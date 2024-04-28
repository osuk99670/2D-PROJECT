using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="Scriptable Object/ItemData")] //��ũ���ͺ�
public class ItemData : ScriptableObject
{
    public enum ItemType { Sword,Fire,Glove,Shoe,Heal,Shiled,Light} //������ Ÿ��

    [Header("# Main Info")] //����
    public ItemType type;
    public int itemId; //������ ���̵�
    public string itemName; //������ �̸�
    [TextArea]
    public string itemDesc; //������ ����
    public Sprite itemIcon; //������ ������

    [Header("# Level Data")] //���� ������
    public float damage; //������
    public int count; //����
    public float size; //ũ��
    public float[] damages; //������
    public int[] counts; //������
    public float[] sizes; //ũ���

    [Header("# Weapon")] //���� 
    public GameObject projectile;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="Scriptable Object/ItemData")] //스크립터블
public class ItemData : ScriptableObject
{
    public enum ItemType { Sword,Fire,Glove,Shoe,Heal,Shiled,Light} //아이템 타입

    [Header("# Main Info")] //정보
    public ItemType type;
    public int itemId; //아이템 아이디
    public string itemName; //아이템 이름
    [TextArea]
    public string itemDesc; //아이템 설명
    public Sprite itemIcon; //아이템 아이콘

    [Header("# Level Data")] //레벨 데이터
    public float damage; //데미지
    public int count; //개수
    public float size; //크기
    public float[] damages; //데미지
    public int[] counts; //개수들
    public float[] sizes; //크기들

    [Header("# Weapon")] //무기 
    public GameObject projectile;
}

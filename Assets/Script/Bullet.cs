using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; //데미지
    public int per; //관통력
    public float size;

    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage,int per,Vector3 dir) //무기 초기화
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rigid.velocity = dir * 15f;
        }

    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Enemy") || per == -100)
        {
            return;
        }

        per--;

        if (per<0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }

    }

    void OnTriggerExit2D(Collider2D collision)  //플레이어 자식으로 설정한 영역 밖으로 나가면 비활성
    {
        if(!collision.CompareTag("Area")||per==-1) 
        {
            return;
        }

        gameObject.SetActive(false);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiled : MonoBehaviour
{
    public float damage;
    public float size;
    Rigidbody2D rigid;
    public void Init(float damage, float size) //무기 초기화
    {
        this.damage=damage;
        this.size=size;
        transform.localScale+=new Vector3(size, size, size);

    }
    void Update()
    {
        transform.position = GameManager.instance.player.transform.position;
        transform.Translate(new Vector3(0, -0.7f, 0));
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }
    }
}

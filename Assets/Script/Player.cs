using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Vector2 input;
    public float speed; //���ǵ�
    public Scanner scanner; //�� Ž�� ��ĳ��

    Rigidbody2D rigid; //������ٵ�
    SpriteRenderer sprite; //��������Ʈ
    Animator animator; //�ִϸ�����
    
    void Awake()
    {
       rigid = GetComponent<Rigidbody2D>();
       sprite = GetComponent<SpriteRenderer>();
       animator = GetComponent<Animator>();
       scanner = GetComponent<Scanner>();
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        Vector2 next = input * speed * Time.deltaTime; //���� ��ġ
        rigid.MovePosition(rigid.position + next); //�̵�
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        animator.SetFloat("Run", input.magnitude); //�ִϸ����� Ȱ��
        if(input.x!=0)
        {
            sprite.flipX = input.x < 0; //��������Ʈ �¿� ��ȯ
        }
    }

    void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }

        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health<0)
        {
            for(int index=1; index<transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            Debug.Log("adssasdsad");
            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}

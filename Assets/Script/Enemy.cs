using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; //���ǵ�
    public float health; //ü��
    public float maxHealth; //�ִ�ü��
    public RuntimeAnimatorController[] controller; //�ִϸ����� ��Ʈ�ѷ�
    public Rigidbody2D target; //Ÿ�� ������ٵ�
    bool isLive; //����ִ��� �׾����� üũ

    Rigidbody2D rigid; //�߷�
    SpriteRenderer sprite; //��������Ʈ
    Animator animator; //�ִϸ�����
    Collider2D collide; //�ݶ��̴�
    WaitForFixedUpdate wait; //���������� ��ٸ�
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collide = GetComponent<Collider2D>();
        wait=new WaitForFixedUpdate();
    }
  
    void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }
        if(!isLive||animator.GetCurrentAnimatorStateInfo(0).IsName("hit")) //�׾��ų� �Ǵ� �ִϸ����� ���°� �ǰݸ���̸� ����
        {
            return;
        }
        Vector2 dir = target.position - rigid.position; //����
        Vector2 next = dir.normalized * speed * Time.deltaTime; //�̵��� ����
        rigid.MovePosition(rigid.position + next); //�̵�
        rigid.velocity = Vector2.zero; //�ӵ� 0���� ����
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        if (!isLive)
        {
            return;
        }
        sprite.flipX = target.position.x < rigid.position.x; //������Ʈ �� ��������Ʈ ��ġ ��ȯ
    }

    void OnEnable() //��ũ��Ʈ Ȱ��ȭ 
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); //
        isLive = true; //�������
        collide.enabled = true; //�ݶ��̴� Ȱ��ȭ
        rigid.simulated = true; //������ٵ� Ȱ��ȭ
        sprite.sortingOrder = 2; //��������Ʈ���� 2�� 
        animator.SetBool("Dead", false); //�ִϸ����� Ȱ��ȭ
        health = maxHealth; //ü�� ����
    }

    public void Init(SpawnData data) //�ʱ�ȭ
    {
        animator.runtimeAnimatorController = controller[data.spriteType]; //���ϸ�����
        speed = data.speed; //���ǵ�
        maxHealth = data.health; //ü��
        health = data.health; //ü��
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Bullet")||!isLive) //�Ѿ��� �ƴϰų� �Ǵ� ������� ������ ����
        {
            return;
        }
      
        health -= collision.GetComponent<Bullet>().damage; //ü�� ����
        StartCoroutine("KnockBack"); //�˹� �߻�
      
        if(health>0)
        {
            animator.SetTrigger("Hit"); //�ִϸ����� ��Ʈ���
        }
        else
        {
            isLive = false; //����
            collide.enabled = false; //�ݶ��̴� ��Ȱ��
            rigid.simulated = false; //������ٵ� ��Ȱ��
            sprite.sortingOrder = 1; //��������Ʈ �Ⱥ��̰� �ؾ��ϹǷ� ����
            animator.SetBool("Dead",true); //�ִϸ����� ���� ���
            GameManager.instance.kill++; //���ӸŴ��� ų�� ����
            GameManager.instance.GetExp(); //���ӸŴ��� ����ġ ����
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Shiled") || !isLive) //�Ѿ��� �ƴϰų� �Ǵ� ������� ������ ����
        {
            return;
        }
        Debug.Log("dedede");
        health -= collision.GetComponent<Shiled>().damage; //ü�� ����


        if (health > 0)
        {
            animator.SetTrigger("Hit"); //�ִϸ����� ��Ʈ���
        }
        else
        {
            isLive = false; //����
            collide.enabled = false; //�ݶ��̴� ��Ȱ��
            rigid.simulated = false; //������ٵ� ��Ȱ��
            sprite.sortingOrder = 1; //��������Ʈ �Ⱥ��̰� �ؾ��ϹǷ� ����
            animator.SetBool("Dead", true); //�ִϸ����� ���� ���
            GameManager.instance.kill++; //���ӸŴ��� ų�� ����
            GameManager.instance.GetExp(); //���ӸŴ��� ����ġ ����
        }
    }

    IEnumerator KnockBack() //�˹�
    {
        yield return wait; //���� ���� �����ӱ��� ������
        Vector3 playerPos = GameManager.instance.player.transform.position;//�÷��̾� ��ġ 
        Vector3 dir = transform.position - playerPos; //����
        rigid.AddForce(dir.normalized * 2, ForceMode2D.Impulse); //�������� ������

    }

    void Die() 
    {
        gameObject.SetActive(false); //������ ������Ʈ ��Ȱ��ȭ
    }
}

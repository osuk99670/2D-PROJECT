using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; //스피드
    public float health; //체력
    public float maxHealth; //최대체력
    public RuntimeAnimatorController[] controller; //애니메이터 컨트롤러
    public Rigidbody2D target; //타겟 리지드바디
    bool isLive; //살아있는지 죽었는지 체크

    Rigidbody2D rigid; //중력
    SpriteRenderer sprite; //스프라이트
    Animator animator; //애니메이터
    Collider2D collide; //콜라이더
    WaitForFixedUpdate wait; //물리프레임 기다림
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
        if(!isLive||animator.GetCurrentAnimatorStateInfo(0).IsName("hit")) //죽었거나 또는 애니메이터 상태가 피격모션이면 리턴
        {
            return;
        }
        Vector2 dir = target.position - rigid.position; //방향
        Vector2 next = dir.normalized * speed * Time.deltaTime; //이동할 방향
        rigid.MovePosition(rigid.position + next); //이동
        rigid.velocity = Vector2.zero; //속도 0으로 세팅
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
        sprite.flipX = target.position.x < rigid.position.x; //업데이트 후 스프라이트 위치 변환
    }

    void OnEnable() //스크립트 활성화 
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); //
        isLive = true; //살아있음
        collide.enabled = true; //콜라이더 활성화
        rigid.simulated = true; //리지드바디 활성화
        sprite.sortingOrder = 2; //스프라이트오더 2로 
        animator.SetBool("Dead", false); //애니메이터 활성화
        health = maxHealth; //체력 세팅
    }

    public void Init(SpawnData data) //초기화
    {
        animator.runtimeAnimatorController = controller[data.spriteType]; //에니메이터
        speed = data.speed; //스피드
        maxHealth = data.health; //체력
        health = data.health; //체력
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Bullet")||!isLive) //총알이 아니거나 또는 살아있지 않으면 리턴
        {
            return;
        }
      
        health -= collision.GetComponent<Bullet>().damage; //체력 감소
        StartCoroutine("KnockBack"); //넉백 발생
      
        if(health>0)
        {
            animator.SetTrigger("Hit"); //애니메이터 히트모션
        }
        else
        {
            isLive = false; //죽음
            collide.enabled = false; //콜라이더 비활성
            rigid.simulated = false; //리지드바디 비활성
            sprite.sortingOrder = 1; //스프라이트 안보이게 해야하므로 감소
            animator.SetBool("Dead",true); //애니메이터 죽음 모션
            GameManager.instance.kill++; //게임매니저 킬수 증가
            GameManager.instance.GetExp(); //게임매니저 경험치 증가
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Shiled") || !isLive) //총알이 아니거나 또는 살아있지 않으면 리턴
        {
            return;
        }
        Debug.Log("dedede");
        health -= collision.GetComponent<Shiled>().damage; //체력 감소


        if (health > 0)
        {
            animator.SetTrigger("Hit"); //애니메이터 히트모션
        }
        else
        {
            isLive = false; //죽음
            collide.enabled = false; //콜라이더 비활성
            rigid.simulated = false; //리지드바디 비활성
            sprite.sortingOrder = 1; //스프라이트 안보이게 해야하므로 감소
            animator.SetBool("Dead", true); //애니메이터 죽음 모션
            GameManager.instance.kill++; //게임매니저 킬수 증가
            GameManager.instance.GetExp(); //게임매니저 경험치 증가
        }
    }

    IEnumerator KnockBack() //넉백
    {
        yield return wait; //다음 물리 프레임까지 딜레이
        Vector3 playerPos = GameManager.instance.player.transform.position;//플레이어 위치 
        Vector3 dir = transform.position - playerPos; //방향
        rigid.AddForce(dir.normalized * 2, ForceMode2D.Impulse); //방향으로 힘을줌

    }

    void Die() 
    {
        gameObject.SetActive(false); //죽으면 오브젝트 비활성화
    }
}

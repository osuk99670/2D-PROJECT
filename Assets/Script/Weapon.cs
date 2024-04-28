using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; //무기 아아디
    public int prefabId; //프리펩 아이디
    public float damage; //데미지
    public int count; //개수
    public float size;
    public float speed; //속도
    
    float timer; //시간
    Player player; //플레이어
    CapsuleCollider2D collide;

    void Awake()
    {
        player = GameManager.instance.player; //게임매니저에서 스태틱으로 선언한 플레이어 정보 가져오기
        collide = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime); //회전무기이면 계속 회전
                break;

            case 5:
                break;

            case 6:
                timer += Time.deltaTime;

                if(timer==4)
                {
                    Lighting();
                }
                break;
            default:
                timer += Time.deltaTime; //시간 증가

                if(timer>speed) //시간이 스피드보다 높으면 리셋
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    public void LevelUp(float damage,int count,float size) //레벨업
    {
        this.damage = damage;
        this.count += count;
        this.size = size;

        if (id == 0) 
        {
            Batch(); 
        }
        if(id==5)
        {
            RangeAttack();
        }
    }

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon" + data.itemId; //오브젝트 이름 지정
        transform.parent = player.transform; //웨폰 부모는 플레이어로 지정
        transform.localPosition = Vector3.zero; //웨폰 로컬위치 0으로 지정

        id = data.itemId; //아이디
        damage = data.damage; //데미지
        count = data.count; //개수
        size = data.size;

        for(int index=0; index<GameManager.instance.poolManager.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.poolManager.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch(id)
        {
            case 0:
                speed = 150;
                Batch();
                break;

            case 5:
                speed = 150;
                Transform shield = GameManager.instance.poolManager.Get(prefabId).transform; //풀 매니저에서 무기의 위치를 가져옴
                shield.parent = transform;
                shield.position = transform.position;
                shield.GetComponent<Shiled>().Init(damage, size); //무기 초기화
                break;

            default:
                speed = 0.3f;
                break;
        }
    }

    void Batch()
    {
        for(int index=0; index<count; index++)
        {
            Transform bullet;
            
            if(index<transform.childCount)
            {
                bullet=transform.GetChild(index); //기존 사용하는 무기를 재활용
            }
            else
            {
                bullet = GameManager.instance.poolManager.Get(prefabId).transform; //풀 매니저에서 근접무기 가져옴
                bullet.parent = transform; //웨폰을 부모로 설정
            }
            

            bullet.localPosition= Vector3.zero; //레벨업 하면 근접무기의 개수가 늘어나므로 포지션을 0으로 다시 세팅
            bullet.localRotation = Quaternion.identity; //회전 없음

            Vector3 rot = Vector3.forward * 360 * index / count;
            bullet.Rotate(rot); //회전
            bullet.Translate(bullet.up * 2.0f,Space.World); //무기위치를 플레이어 1.5만큼 떨어진 거리에 배치
            bullet.GetComponent<Bullet>().Init(damage, -100,Vector3.zero); //무기 초기화
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget) //적 스캔 
        {
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position; //적 포지션
        Vector3 dir = targetPos - transform.position; //적과 나의 거리
        dir = dir.normalized; //방향 

        Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform; //풀 매니저에서 무기의 위치를 가져옴
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.down, dir); //타겟 방향으로 발사
        bullet.GetComponent<Bullet>().Init(damage, count, dir); //무기 초기화
    }

    void Lighting()
    {
        if (!player.scanner.nearestTarget) //적 스캔 
        {
            return;
        }

        Transform lighting = GameManager.instance.poolManager.Get(prefabId).transform;
        GameObject[] enemys = GameManager.instance.poolManager.Get(0).gameObject.transform.GetComponentsInChildren<GameObject>();

        int count = 3;
        foreach(GameObject enemy in enemys) 
        {
            if (enemy.activeSelf)
            {
                lighting.position = enemy.transform.position;
            }
            if (count == 0)
            {
                break;
            }
            --count;
        }
        
    }

    void RangeAttack()
    {
        Shiled shiled = GetComponentInChildren<Shiled>();
        shiled.transform.localScale += new Vector3(size, size, size);
        shiled.damage += damage;
        shiled.size += size;
    }
}

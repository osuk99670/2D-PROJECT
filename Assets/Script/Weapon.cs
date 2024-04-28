using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id; //���� �ƾƵ�
    public int prefabId; //������ ���̵�
    public float damage; //������
    public int count; //����
    public float size;
    public float speed; //�ӵ�
    
    float timer; //�ð�
    Player player; //�÷��̾�
    CapsuleCollider2D collide;

    void Awake()
    {
        player = GameManager.instance.player; //���ӸŴ������� ����ƽ���� ������ �÷��̾� ���� ��������
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
                transform.Rotate(Vector3.back * speed * Time.deltaTime); //ȸ�������̸� ��� ȸ��
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
                timer += Time.deltaTime; //�ð� ����

                if(timer>speed) //�ð��� ���ǵ庸�� ������ ����
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    public void LevelUp(float damage,int count,float size) //������
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
        name = "Weapon" + data.itemId; //������Ʈ �̸� ����
        transform.parent = player.transform; //���� �θ�� �÷��̾�� ����
        transform.localPosition = Vector3.zero; //���� ������ġ 0���� ����

        id = data.itemId; //���̵�
        damage = data.damage; //������
        count = data.count; //����
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
                Transform shield = GameManager.instance.poolManager.Get(prefabId).transform; //Ǯ �Ŵ������� ������ ��ġ�� ������
                shield.parent = transform;
                shield.position = transform.position;
                shield.GetComponent<Shiled>().Init(damage, size); //���� �ʱ�ȭ
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
                bullet=transform.GetChild(index); //���� ����ϴ� ���⸦ ��Ȱ��
            }
            else
            {
                bullet = GameManager.instance.poolManager.Get(prefabId).transform; //Ǯ �Ŵ������� �������� ������
                bullet.parent = transform; //������ �θ�� ����
            }
            

            bullet.localPosition= Vector3.zero; //������ �ϸ� ���������� ������ �þ�Ƿ� �������� 0���� �ٽ� ����
            bullet.localRotation = Quaternion.identity; //ȸ�� ����

            Vector3 rot = Vector3.forward * 360 * index / count;
            bullet.Rotate(rot); //ȸ��
            bullet.Translate(bullet.up * 2.0f,Space.World); //������ġ�� �÷��̾� 1.5��ŭ ������ �Ÿ��� ��ġ
            bullet.GetComponent<Bullet>().Init(damage, -100,Vector3.zero); //���� �ʱ�ȭ
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget) //�� ��ĵ 
        {
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position; //�� ������
        Vector3 dir = targetPos - transform.position; //���� ���� �Ÿ�
        dir = dir.normalized; //���� 

        Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform; //Ǯ �Ŵ������� ������ ��ġ�� ������
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.down, dir); //Ÿ�� �������� �߻�
        bullet.GetComponent<Bullet>().Init(damage, count, dir); //���� �ʱ�ȭ
    }

    void Lighting()
    {
        if (!player.scanner.nearestTarget) //�� ��ĵ 
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

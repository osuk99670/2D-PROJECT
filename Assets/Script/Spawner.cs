using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint; //���� ���� ��ġ
    public SpawnData[] spawnData;

    int level;
    float timer;
    void Awake()
    {
        spawnPoint =GetComponentsInChildren<Transform>();
       
    }
    void Update()
    {
        if(!GameManager.instance.isLive)
        {
            return;
        }
        timer += Time.deltaTime;
        level =Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime /10f ),spawnData.Length-1); //�ð� ������ ���� ��������

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy=GameManager.instance.poolManager.Get(0); //Ǯ �Ŵ������� ���� ��������
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("#Game Control")] //���� ��Ʈ�ѷ�
    public bool isLive;
    public float gameTime; 
    public float maxGameTime = 6 * 10f;

    [Header("#Player Info")] //�÷��̾� ����
    public int level; //����
    public int kill; //ų��
    public int exp; //����ġ
    public float health; //ü��
    public float maxHealth; //�ִ�ü��
    public int[] nextExp = { 10, 40, 70, 110, 160, 200, 250, 300, 400, 500 }; //����ġ �迭
    
    [Header("#Game Object")] //���� ������Ʈ
    public PoolManager poolManager; //Ǯ �Ŵ���
    public LevelUp uiLevelUp;
    public Player player; //�÷��̾�
    public GameResult ui;
    public GameObject cleaer;
    void Awake() 
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(0);
        AudioManager.instance.PlayBgm(true);
        GameResume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRountine());
    }

    IEnumerator GameOverRountine()
    {
        isLive = false;
        cleaer.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ui.gameObject.SetActive(true);
        ui.Over();
        GameStop();
    }

    public void GameReStart()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if(!isLive)
        {
            return;
        }
        gameTime += Time.deltaTime; //���ӽð� ����

        if (gameTime>maxGameTime)
        {
            gameTime = maxGameTime;
            GameOver();
        }
    }

    public void GetExp() //����ġ ȹ��
    {
        if(!isLive)
        {
            return;
        }

        exp++; 
        if (exp == nextExp[Mathf.Min(level,nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void GameStop() //����
    {
        isLive = false;
        Time.timeScale = 0; //����Ƽ�� �ð��ӵ�
    }

    public void GameResume() //�簳
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

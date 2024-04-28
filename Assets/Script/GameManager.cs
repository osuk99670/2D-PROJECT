using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("#Game Control")] //게임 컨트롤러
    public bool isLive;
    public float gameTime; 
    public float maxGameTime = 6 * 10f;

    [Header("#Player Info")] //플레이어 정보
    public int level; //레벨
    public int kill; //킬수
    public int exp; //경험치
    public float health; //체력
    public float maxHealth; //최대체력
    public int[] nextExp = { 10, 40, 70, 110, 160, 200, 250, 300, 400, 500 }; //경험치 배열
    
    [Header("#Game Object")] //게임 오브젝트
    public PoolManager poolManager; //풀 매니저
    public LevelUp uiLevelUp;
    public Player player; //플레이어
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
        gameTime += Time.deltaTime; //게임시간 증가

        if (gameTime>maxGameTime)
        {
            gameTime = maxGameTime;
            GameOver();
        }
    }

    public void GetExp() //겸험치 획득
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

    public void GameStop() //정지
    {
        isLive = false;
        Time.timeScale = 0; //유니티의 시간속도
    }

    public void GameResume() //재개
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

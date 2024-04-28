using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D collide; //콜라이더

    void Awake()
    {
        collide = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area")) //플레이어 자식 영역 아니면 리턴
        {
            return;
        }

        Vector3 playerPos = GameManager.instance.player.transform.position; //플레이어 포지션
        Vector3 myPos = transform.position; // 포지션
   
        switch (transform.tag)
        {
            case "Ground": //플레이어 자식으로 설정한 영역 벗어나면 맵 위치 조정
                float diffX = Mathf.Abs(playerPos.x - myPos.x);
                float diffY = Mathf.Abs(playerPos.y - myPos.y);

                Vector3 playerDir = GameManager.instance.player.input;
                float dirX = playerDir.x < 0 ? -1 : 1;
                float dirY = playerDir.y < 0 ? -1 : 1;


                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                else
                {
                    transform.Translate(Vector3.right * dirX * 40);
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            case "Enemy": //플레이 자식 설정한  영역 벗어나면 적 위치 조정
                if (collide.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 rand = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
                    transform.Translate(rand+dist*2);
                }
                break;
        }

    }
}

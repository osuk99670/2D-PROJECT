using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D collide; //�ݶ��̴�

    void Awake()
    {
        collide = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area")) //�÷��̾� �ڽ� ���� �ƴϸ� ����
        {
            return;
        }

        Vector3 playerPos = GameManager.instance.player.transform.position; //�÷��̾� ������
        Vector3 myPos = transform.position; // ������
   
        switch (transform.tag)
        {
            case "Ground": //�÷��̾� �ڽ����� ������ ���� ����� �� ��ġ ����
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

            case "Enemy": //�÷��� �ڽ� ������  ���� ����� �� ��ġ ����
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

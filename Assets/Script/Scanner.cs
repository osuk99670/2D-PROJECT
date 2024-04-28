using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; //스캔범위
    public LayerMask target; //레이어마스크 타겟
    public RaycastHit2D[] ray; // 레이캐스트 
    public Transform nearestTarget; //가까운 타겟

    void FixedUpdate()
    {
        ray = Physics2D.CircleCastAll(transform.position,scanRange,Vector2.zero,0,target); //레이캐스트 서클(내 위치,스캔 범위,방향(제로),거리,타겟)
        nearestTarget=GetNearest(); //적 좌표 업데이트
    }

    Transform GetNearest() //좌표 획득
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D target in ray) //범위 체크
        {
            Vector3 myPos = transform.position; //내 위치
            Vector3 targetPos = target.transform.position; //타겟 포지션
            float curDiff = Vector3.Distance(myPos, targetPos); //거리

            if(curDiff<diff) 
            {
                diff = curDiff;
                result = target.transform;
                
            }
        }


        return result;
    }
}

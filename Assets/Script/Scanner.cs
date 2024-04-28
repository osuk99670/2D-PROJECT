using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange; //��ĵ����
    public LayerMask target; //���̾��ũ Ÿ��
    public RaycastHit2D[] ray; // ����ĳ��Ʈ 
    public Transform nearestTarget; //����� Ÿ��

    void FixedUpdate()
    {
        ray = Physics2D.CircleCastAll(transform.position,scanRange,Vector2.zero,0,target); //����ĳ��Ʈ ��Ŭ(�� ��ġ,��ĵ ����,����(����),�Ÿ�,Ÿ��)
        nearestTarget=GetNearest(); //�� ��ǥ ������Ʈ
    }

    Transform GetNearest() //��ǥ ȹ��
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D target in ray) //���� üũ
        {
            Vector3 myPos = transform.position; //�� ��ġ
            Vector3 targetPos = target.transform.position; //Ÿ�� ������
            float curDiff = Vector3.Distance(myPos, targetPos); //�Ÿ�

            if(curDiff<diff) 
            {
                diff = curDiff;
                result = target.transform;
                
            }
        }


        return result;
    }
}

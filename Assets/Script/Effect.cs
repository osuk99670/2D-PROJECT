using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    ParticleSystem effect;
    int count;
    void Awake()
    {
        effect = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        count = 0;
    }
    void Update()
    {
        gameObject.transform.position = GameManager.instance.player.transform.position;
        if (!GameManager.instance.isLive&&count!=0&&GameManager.instance.health>0)
        {
            effect.Play();
            count = 0;
        }
        else if(GameManager.instance.isLive&&count == 0||GameManager.instance.gameTime==GameManager.instance.maxGameTime)
        {
            effect.Stop();
            effect.Clear();
            ++count;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerManager : MonoBehaviour
{
    NavMeshAgent agent;
    Animator moveAnimator;
    float attackDistance = 1f;
    float attackCD = 0f;
    CharacterData characterInfo;
    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        moveAnimator = this.gameObject.GetComponent<Animator>();
        characterInfo = this.gameObject.GetComponent<CharacterData>();
        attackCD = characterInfo.AttackInterval;
    }
    // Start is called before the first frame update
    void Start()
    {
        MouseManager.Instance.ClickGround += MovePlayer;
        MouseManager.Instance.ClickMonster += AttackMonster; 
    }

    private void AttackMonster(GameObject enemy)
    {
        this.transform.LookAt(enemy.transform);
        StartCoroutine(AttackMonsterIE(enemy));
    }
    IEnumerator AttackMonsterIE(GameObject enemy)
    {
        agent.isStopped = false;
        while (Vector3.Distance(this.transform.position,enemy.transform.position) >  characterInfo.AttackRange)
        {
            agent.destination = enemy.transform.position;
            yield return null;
        }
        agent.isStopped = true;
        //¹¥»÷µÐÈË(²¥·Å¶¯»­)
        if (attackCD <= 0)
        {

            agent.destination = this.transform.position;
            characterInfo.isCriticalStrike = UnityEngine.Random.value < characterInfo.CriticalStrikeRate ? true : false;
            moveAnimator.SetTrigger("Attack");
            moveAnimator.SetBool("CriticalStrike", characterInfo.isCriticalStrike);



            attackCD = characterInfo.AttackInterval;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        SwitchAnimations();
        attackCD -= Time.deltaTime;
    }
    void MovePlayer(Vector3  targetPos)
    {
        agent.isStopped = false;
        agent.destination = targetPos;
    }
    void SwitchAnimations()
    {
        moveAnimator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
}

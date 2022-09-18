using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum MonsterState
{
    GUARD, PATROL, CHASE ,DIE
};
[RequireComponent(typeof(NavMeshAgent))]
public class MonsterManager : MonoBehaviour
{
    bool isGarud = false;
    Animator animator;
    public MonsterState curretnState;
    NavMeshAgent agent;
    bool isFindPlayer, isChase, isWalk;
    GameObject followOBj;
    Vector3 initialPosition;
    float speed;
    /// <summary>¹¥»÷·¶Î§</summary>
    float attackRange = 1f;
    /// <summary>Ñ²Âß·¶Î§</summary>
    public  float patrolRange = 8;
    /// <summary>Ñ²Âß·¶Î§</summary>
    public float patrolCD = 3f;
    private Vector3 partrolPosition;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        speed = agent.speed;
        initialPosition = this.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!isGarud)
        {
            GetNewWay();
            curretnState = MonsterState.PATROL;
        }
        else
        {
            curretnState = MonsterState.GUARD;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SwitchState();
        SwitchAnimation();
    }

    void SwitchState()
    {

        if (IsFindPlayer())
        {
            curretnState = MonsterState.CHASE;
            //isFindPlayer = false;
            //isChase = true;
        }

        switch (curretnState)
        {
            case MonsterState.GUARD:
                break;
            case MonsterState.PATROL:

                
                if (Vector3.Distance(partrolPosition,transform.position) > agent.stoppingDistance)
                {
                    isWalk = true;
                    agent.destination = partrolPosition;
                    
                }
                else{
                    if (patrolCD > 0)
                    {
                        isWalk = false;
                        patrolCD -= Time.deltaTime;
                        agent.destination = this.transform.position;
                    }
                    else
                    {
                        GetNewWay();
                        patrolCD = 3f;
                    }
                    
                   
                }
                break;
            case MonsterState.CHASE:
                {
                    if (!IsFindPlayer())
                    {

                        isFindPlayer = true;
                        isChase = false;
                        agent.destination = this.transform.position;
                        if (patrolCD > 0)
                        {
                            patrolCD -= Time.deltaTime;
                        }
                        else
                        {
                            if (isGarud)
                            {
                                curretnState = MonsterState.GUARD;
                            }
                            else
                            {
                                curretnState = MonsterState.PATROL;
                            }
                        }

                    }
                    else
                    {

                        if (Vector3.Distance(this.transform.position, followOBj.transform.position) > attackRange + agent.radius)
                        {
                            agent.speed = speed * 0.5f;
                            isFindPlayer = true;
                            isChase = true;
                            agent.destination = followOBj.transform.position;

                            Debug.LogError("patrol player");
                        }
                        else
                        {
                            // isFindPlayer = false;
                            agent.destination = this.transform.position;
                            isChase = false;



                        }

                        //TODO:ÅÐ¶ÏÊÇ·ñÔÚ¹¥»÷·¶Î§Àà
                        //ÅÐ¶ÏÊÇ·ñ±©»÷
                        //½øÐÐ¹¥»÷
                        //CdÅÐ¶Ï
                        Attack();
                    }

                }
                break;
            case MonsterState.DIE:
                break;
        } 
        
    }
    private void Attack()
    {

    }
    //ÏÔÊ¾¼àÊÓ·¶Î§
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, 10f);
    }
    private void SwitchAnimation()
    {
        animator.SetBool("FindPlayer",isFindPlayer);
        animator.SetBool("Chase", isChase);
        animator.SetBool("Walk", isWalk);
    }
    bool IsFindPlayer()
    {
        var obj = Physics.OverlapSphere(this.transform.position, 10f);
        foreach (var item in obj)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                followOBj = item.gameObject;
                return true;
            }
        }
        return false;
    }
    private void GetNewWay()
    {
        float x = Random.Range(-patrolRange, patrolRange);
        float z = Random.Range(-patrolRange, patrolRange);
        Vector3 randomPosition = new Vector3(initialPosition.x + x,this.transform.position.y,initialPosition.z + z);
        NavMeshHit hit;
        partrolPosition =  NavMesh.SamplePosition(randomPosition, out hit, patrolRange, 1) ? hit.position : this.transform.position;
    }
}

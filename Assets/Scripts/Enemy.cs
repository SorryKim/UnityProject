using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{   
    // 변수 선언
    public LayerMask whatIsTarget;
    private NavMeshAgent navMeshAgent;
    private LivingEntity targetEntity;

    public float damage = 10f;
    public float timeBetAttack = 1f;
    private float lastAttackTime;

    
    // 추적할 대상을 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            // 추적대상 존재
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // 추적대상 존재X
            return false;
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 경로 업데이트
    private IEnumerator UpdatePath()
    {   
        // 살아있는 경우 무한반복
        while (!dead)
        {

            // 타겟이 존재하는 경우
            if (hasTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            // 타겟 존재X
            else
            {
                navMeshAgent.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);

                for(int i =0; i < colliders.Length; i++)
                {

                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    if(livingEntity != null && !livingEntity.dead)
                    {
                        targetEntity = livingEntity;
                        break;
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.25f);
    }
}

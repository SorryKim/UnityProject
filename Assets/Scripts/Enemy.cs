using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{   
    // ���� ����
    public LayerMask whatIsTarget;
    private NavMeshAgent navMeshAgent;
    private LivingEntity targetEntity;

    public float damage = 10f;
    public float timeBetAttack = 1f;
    private float lastAttackTime;

    
    // ������ ����� �˷��ִ� ������Ƽ
    private bool hasTarget
    {
        get
        {
            // ������� ����
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            // ������� ����X
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

    // ��� ������Ʈ
    private IEnumerator UpdatePath()
    {   
        // ����ִ� ��� ���ѹݺ�
        while (!dead)
        {

            // Ÿ���� �����ϴ� ���
            if (hasTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);
            }
            // Ÿ�� ����X
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

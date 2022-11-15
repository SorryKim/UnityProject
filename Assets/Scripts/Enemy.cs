using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    // 변수 선언
    public float speed;
    private Rigidbody2D rigidbody;
    private Transform target;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        // move
        Move();
    }


    private void Move()
    {
        // 적과 타겟의 방향 설정
        float dirX = target.position.x - transform.position.x;
        float dirY = target.position.y - transform.position.y;
        dirX = dirX < 0 ? -1 : 1;
        dirY = dirY < 0 ? -1 : 1;
        // Move
        transform.Translate(new Vector2(dirX, dirY) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

}

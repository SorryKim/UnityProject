using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : LivingEntity
{
    // Player에서 사용할 변수 선언
    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public float speed = 5f;
    float h, v;
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // 키입력 받음
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // Run 애니메이션 적용
        anim(new Vector2 (h, v));

        // 좌우반전여부
        if(h != 0)
        {
            spriteRenderer.flipX = h < 0 ? true : false;
        }
        // Move
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    void anim(Vector2 vector)
    {
        animator.SetFloat("Speed", vector.magnitude);
    }

    // 적과의 충돌처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            OnDamage(10);
        }
    }
}

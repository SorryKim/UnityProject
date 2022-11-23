using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : LivingEntity
{
    // Player���� ����� ���� ����
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
        // Ű�Է� ����
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // Run �ִϸ��̼� ����
        anim(new Vector2 (h, v));

        // �¿��������
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

    // ������ �浹ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            OnDamage(10);
        }
    }
}

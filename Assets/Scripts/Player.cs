using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : LivingEntity
{
    // Player에서 사용할 변수 선언
    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    CapsuleCollider capsuleCollider;

    public Slider hpBar;
    private Animator animator;
    private AudioSource audioSource;

   
    public float speed = 3f;
    private float h, v;
    public int level = 0;

   
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        startingHealth = 100f;
    }

    void Update()
    {
        hpBar.value = health;
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


    // 피격이벤트
    public override void OnDamage(float damage, Vector2 hitPos)
    {

        gameObject.tag = "PlayerDamaged";

        // damage
        health -= damage;

        if(health <= 0)
        {
            Die();
            return;
        }

        // view alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // reaction force
        int dirX = transform.position.x - hitPos.x > 0 ? 1 : -1;
        int dirY = transform.position.y - hitPos.y > 0 ? 1 : -1;
        playerRigidbody.AddForce(new Vector2(dirX, dirY)* 2f, ForceMode2D.Impulse);
        Invoke("OffDamage", 2f);
        Invoke("RestoreReactionForce", 0.2f);

        // play sound
        audioSource.Play();
        //Invoke("OffDamage", 2f);

    }

    void RestoreReactionForce()
    {
        playerRigidbody.velocity = Vector2.zero;
    }
    void OffDamage()
    {
        gameObject.tag = "Player";
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    public override void Die()
    {
        animator.SetTrigger("Dead");
        GameManager.instance.EndGame();
    }

    public void LevelUp()
    {

    }
    
}

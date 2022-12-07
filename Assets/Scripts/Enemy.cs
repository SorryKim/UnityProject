using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    // 변수 선언
   
    private Rigidbody2D rigidbody;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer spriteRenderer;
    private Transform target;
    private Animator animator;
    private AudioSource audioSource;

    public float damage;
    public float speed;
    public float EXP;
    public bool isDead;

  

    private void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>().transform;
        audioSource = GetComponent<AudioSource>();
    }

    public void Setup(EnemyData enemyData)
    {
        startingHealth = enemyData.health;
        damage = enemyData.damage;
    }

    private void Update()
    {
        // move
        if(!isDead)
            Move();
    }

    

    private void Move()
    {
        // 적과 타겟의 방향 설정
        float dirX = target.position.x - transform.position.x;
        float dirY = target.position.y - transform.position.y;
        dirX = dirX < 0 ? -1 : 1;
        dirY = dirY < 0 ? -1 : 1;
        if (dirX <= 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
     
        // Move
        transform.Translate(new Vector2(dirX, dirY)  * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Weapon")
        {
            OnDamage(GameManager.instance.weapon.damage, collision.transform.position);
        }

       if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            player.OnDamage(10f, gameObject.transform.position);
        }
    }

    public override void OnDamage(float damage, Vector2 hitPos)
    {
        gameObject.layer = 9;

        // damage
        health -= damage;
        if(health <= 0)
        {
            Die();
        }

        animator.SetTrigger("Hit");
        
        // reaction force
        int dirX = transform.position.x - hitPos.x > 0 ? 1 : -1;
        int dirY = transform.position.y - hitPos.y > 0 ? 1 : -1;
        rigidbody.AddForce(new Vector2(dirX, dirY) * 2f, ForceMode2D.Impulse);

        // audio
        audioSource.Play();
        Invoke("OffDamage", 0.1f);
        Invoke("RestoreReactionForce", 0.1f);
    }

    void RestoreReactionForce()
    {
        rigidbody.velocity = Vector2.zero;
    }

    void OffDamage()
    {
        gameObject.tag = "EnemyDamaged";
    }

    public override void Die()
    {
        capsuleCollider.enabled = false;
        GameManager.instance.EXP += EXP;
        GameManager.instance.score += EXP;
        isDead = true;
        animator.SetBool("Dead", true);
        Invoke("Destroy", 3f);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        isDead = false;
        capsuleCollider.enabled = true;
    }


}

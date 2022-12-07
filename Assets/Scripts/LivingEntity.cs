using System;
using UnityEngine;

// 체력, 데미지 받아들이기, 사망 기능, 사망 이벤트를 제공
public class LivingEntity : MonoBehaviour
{
    public float startingHealth = 100f; // 시작 체력
    public float health;// 현재 체력
    public bool dead { get; protected set; } // 사망 상태
    public event Action onDeath; // 사망시 발동할 이벤트

    // 생명체가 활성화될때 상태를 리셋
    protected virtual void OnEnable()
    {
        // 사망하지 않은 상태로 시작
        dead = false;
        // 체력을 시작 체력으로 초기화
        health = startingHealth;
    }

    // 데미지를 입는 기능
    public virtual void OnDamage(float damage, Vector2 hitPos)
    {
    }

    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
            return;

        health += newHealth;
        if(health >= startingHealth)
        {
            health = startingHealth;
        }

    }

    // 사망 처리
    public virtual void Die()
    {
    }
}
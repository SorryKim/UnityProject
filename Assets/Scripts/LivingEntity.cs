using System;
using UnityEngine;

// ü��, ������ �޾Ƶ��̱�, ��� ���, ��� �̺�Ʈ�� ����
public class LivingEntity : MonoBehaviour
{
    public float startingHealth = 100f; // ���� ü��
    public float health;// ���� ü��
    public bool dead { get; protected set; } // ��� ����
    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ

    // ����ü�� Ȱ��ȭ�ɶ� ���¸� ����
    protected virtual void OnEnable()
    {
        // ������� ���� ���·� ����
        dead = false;
        // ü���� ���� ü������ �ʱ�ȭ
        health = startingHealth;
    }

    // �������� �Դ� ���
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

    // ��� ó��
    public virtual void Die()
    {
    }
}
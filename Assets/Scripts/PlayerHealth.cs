using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public AudioClip deathClip;
    public AudioClip hitClip;

    private AudioClip playerAudioPlayer;
    private Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioClip>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
    }

 

    public override void Die()
    {
        base.Die();

        playerAnimator.SetTrigger("Dead");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {

        }
    }
}

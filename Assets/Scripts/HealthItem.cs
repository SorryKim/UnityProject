using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{

    private AudioSource audioSource;
    public override void DestroyAfterTime()
    {
        Invoke("DestroyItem", 15f);
    }

    public override void RunItem()
    {
        GameManager.instance.player.RestoreHealth(20f);
    }

    // Start is called before the first frame update
    void Start()
    {
        DestroyAfterTime();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.Play();
            RunItem();
            gameObject.SetActive(false);
        }
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private AudioSource m_AudioSource;
    public WeaponData weaponData;

    public float damage;
    public float timeBetFire;

    private void Awake()
    {  
        m_AudioSource = GetComponent<AudioSource>();
        
    }

    public void Setup(WeaponData weaponData)
    {
        damage = weaponData.damage;
        timeBetFire = weaponData.timeBetFire;
    }

    private void Update()
    {   
        Vector3 pos = GameManager.instance.player.transform.position;
        transform.RotateAround(pos, new Vector3(0,0,1), timeBetFire * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}

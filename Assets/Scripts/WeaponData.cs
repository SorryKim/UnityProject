using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/WeaponData", fileName = "Weapon data")]
public class WeaponData : ScriptableObject 
{

    public AudioClip hitClip;
    public float damage = 10f;
    public float timeBetFire = 0.5f;
}

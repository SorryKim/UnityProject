using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/EnemyData", fileName = "Enemy data")]
public class EnemyData : ScriptableObject
{
    public float health = 100f;
    public float damage = 10f;
    public float speed = 10f;
    public float EXP = 10f;
    
}

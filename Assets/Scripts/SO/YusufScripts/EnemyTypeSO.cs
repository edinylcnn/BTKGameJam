using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy Type")]

public class EnemyTypeSO : ScriptableObject
{
    public Color enemyColor = Color.red;
    public float health = 20f;
    public float attackDmg = 1.5f;
    public float attackSpeed = 1f;
}

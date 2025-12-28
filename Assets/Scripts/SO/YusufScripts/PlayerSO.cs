using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]


public class PlayerSO : ScriptableObject
{
    public float health = 100f;
    public float attackDmg = 2f;
    public float defense = 0.5f;
    public bool hasTurret = false;
    public bool hasBlockShield=false;
}

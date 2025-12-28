using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private PlayerSO playerso = null;
    public float health = 0;
    public float attackdmg = 0;
    public float defense = 0;
    public bool hasTurret = false;
    public bool hasBlockShield = false;

    void Start()
    {
        health = playerso.health;
        attackdmg = playerso.attackDmg;
        defense = playerso.defense;
        hasTurret = playerso.hasTurret;
        hasBlockShield = playerso.hasBlockShield;
    }



}

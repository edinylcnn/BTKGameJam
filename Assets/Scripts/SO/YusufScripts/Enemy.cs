using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusufScripts
{
    public class Enemy : MonoBehaviour
    {
    
        [SerializeField] private EnemyTypeSO enemyType = null;
        public float health = 0;
        public float attackdmg = 0;
        public float attackSpeed = 0;
        void Start()
        {
            GetComponent<Renderer>().material.color = enemyType.enemyColor;
            health = enemyType.health;
            attackdmg = enemyType.attackDmg;
            attackSpeed = enemyType.attackSpeed;
        }
    
    
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private StatsSO stats;


    private void Start()
    {
        stats.Defanse += 1;
        stats.Power = 6;
    }
}
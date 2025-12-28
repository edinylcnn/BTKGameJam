using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KafesAnimation : MonoBehaviour
{
    public float speed = 2f;      
    public float angleRange = 2f; 

    private Quaternion baseRot;

    void Start()
    {
        baseRot = transform.localRotation; 
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * angleRange;
        transform.localRotation = baseRot * Quaternion.Euler(offset, 0f, 0f);
}
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            FindObjectOfType<actionManager>().OnWallTriggered();
            Debug.Log("buraya girdi");
        }
    }
}

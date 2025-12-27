using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private Transform RgtDoor;

    [SerializeField] private Transform LftDoor;

    // Start is called before the first frame update
    void Start()
    {
        OpenDoor();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenDoor()
    {
        RgtDoor.DOLocalMoveX(-2, 0.6f);
        LftDoor.DOLocalMoveX(2, 0.6f);
    }
}
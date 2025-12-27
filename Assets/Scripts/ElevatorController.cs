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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenDoor();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        RgtDoor.DOLocalMoveX(-2, 0.6f).SetEase(Ease.OutQuad);
        LftDoor.DOLocalMoveX(2, 0.6f).SetEase(Ease.OutQuad);
    }

    public void CloseDoor()
    {
        RgtDoor.DOLocalMoveX(-0.8f, 0.6f).SetEase(Ease.OutQuad);
        LftDoor.DOLocalMoveX(0.8f, 0.6f).SetEase(Ease.OutQuad);
    }
}
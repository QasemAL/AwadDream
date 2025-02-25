using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTarget;

    private Vector3 tempPos;

    [SerializeField]
    private float minX, maxX;

    void Start()
    {
        playerTarget = GameObject.FindWithTag("Player").transform;
    }


    void LateUpdate()
    {

        if (!playerTarget)
            return;

        tempPos = transform.position;
        tempPos.x = playerTarget.position.x;

        if (tempPos.x < minX)
            tempPos.x = minX;

        if (tempPos.x > maxX)
            tempPos.x = maxX;

        transform.position = tempPos;

    }
}
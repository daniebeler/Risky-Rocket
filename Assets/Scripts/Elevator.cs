using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    private int FadeSequence = 1;
    private float startTime;
    private float startPosX;
    private float startPosY;

    [SerializeField]
    private int mode = 0;

    [SerializeField]
    private float elevatorHeight = 10f;

    [SerializeField]
    private float duration = 3f;
    private Rigidbody2D rigid;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        startTime = Time.time;
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (FadeSequence == 1)
        {
            float t = (Time.time - startTime) / duration;
            if (mode == 0)
            {
                rigid.MovePosition(new Vector3(transform.position.x, Mathf.SmoothStep(startPosY, startPosY + elevatorHeight, t), 0));
            }
            else
            {
                rigid.MovePosition(new Vector3(Mathf.SmoothStep(startPosX, startPosX + elevatorHeight, t), transform.position.y, 0));
            }

            if (t >= 1)
            {
                FadeSequence = 2;
                startTime = Time.time;
            }
        }
        else if (FadeSequence == 2)
        {
            float t = (Time.time - startTime) / duration;
            if (mode == 0)
            {
                rigid.MovePosition(new Vector3(transform.position.x, Mathf.SmoothStep(startPosY + elevatorHeight, startPosY, t), 0));
            }
            else
            {
                rigid.MovePosition(new Vector3(Mathf.SmoothStep(startPosX + elevatorHeight, startPosX, t), transform.position.y, 0));
            }
            if (t >= 1)
            {
                FadeSequence = 1;
                startTime = Time.time;
            }
        }
    }
}

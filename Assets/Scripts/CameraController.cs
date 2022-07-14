using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float damping = 0.5f;
    public float lookAheadFactor = 0;
    public float lookAheadReturnSpeed = 10;
    public float lookAheadMoveThreshold = 0;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;
    private Transform target;
    private int FadeSequence = 0;
    private float startTime;

    Vector3 newPos;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
        Camera.main.orthographicSize = 100f;
    }

    private void Update()
    {
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);


        transform.position = newPos;
        m_LastTargetPosition = target.position;


        if (FadeSequence == 1)
        {
            float t = (Time.time - startTime) / 3f;
            Camera.main.orthographicSize = Mathf.SmoothStep(100, 7, t);
            if (t >= 1)
            {
                FadeSequence = 0;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().setPlayingStatus(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<CanvasController>().FadeInGameOverlay();
            }
        }
        else if (FadeSequence == 2)
        {
            float t = (Time.time - startTime) / 3f;
            Camera.main.orthographicSize = Mathf.SmoothStep(7, 100, t);
            if (t >= 1)
            {
                FadeSequence = 0;
                GameObject.FindGameObjectWithTag("LevelHolder").GetComponent<LevelsController>().disableLevels();
            }
        }
    }

    public void zoomInCamera(Vector2 pos)
    {
        transform.position = pos;
        startTime = Time.time;
        FadeSequence = 1;
    }

    public void zoomOutCamera()
    {
        startTime = Time.time;
        FadeSequence = 2;
    }
}

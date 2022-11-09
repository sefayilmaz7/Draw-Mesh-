using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Balloon : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Vector3 spawnPos => Vector3.zero;

    private bool connected = false;

    #region Clamp Values

    private float minX = -2.5f;
    private float maxX = 2.5f;

    #endregion

    private void Inflate()
    {
        transform.DOScale(1, 0.7f).From(0).OnComplete(() =>
        {
            connected = true;
            PushBalloon();
        });
    }

    private void PushBalloon()
    {
        rb.AddForce(Vector3.up * 100 , ForceMode.Force);
    }

    private void ConnectRope()
    {
        var lineRenderer = GetComponentInChildren<LineRenderer>();
        if (!lineRenderer)
            return;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, spawnPos);
        lineRenderer.SetPosition(1, transform.position - new Vector3(0,0.5f,0));
    }

    private void Update()
    {
        ClampPositions();
        
        if (connected)
            ConnectRope();
    }
    
    private void Start()
    {
        Inflate();
    }

    private void ClampPositions()
    {
        Vector3 tmpPos = transform.position;
        tmpPos.y = Mathf.Clamp(tmpPos.y, 1, Mathf.Infinity);
        tmpPos.x = Mathf.Clamp(tmpPos.x, minX, maxX);
        transform.position = tmpPos;
    }

    private void OnEnable()
    {
        BalloonSpawner.UpdateBoundaries += UpdateBoundariesCallBack;
    }

    private void OnDisable()
    {
        BalloonSpawner.UpdateBoundaries -= UpdateBoundariesCallBack;
    }

    private void UpdateBoundariesCallBack()
    {
        minX -= 0.02f;
        maxX += 0.02f;
    }
}
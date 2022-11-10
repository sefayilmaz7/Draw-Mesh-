using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Balloon : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Vector3 spawnPos => Vector3.zero;

    private bool connected = false;
    
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
        var randNumber = Random.Range(0, 100);
        Vector3 force = new Vector3(Random.Range(0.05f, 0.1f), Random.Range(0.5f,1f) ,randNumber > 50 ? -1 : 1);
        rb.velocity = force;
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
        KeepPushing();
        
        if (connected)
            ConnectRope();
    }

    private void KeepPushing()
    {
        if (rb.position.y < 1.5)
        {
            rb.AddForce(Vector3.up * 3, ForceMode.Force);
        }
        else
        {
            rb.AddForce(-1 * Vector3.up, ForceMode.Force);
        }
    }

    private void Start()
    {
        Inflate();
    }
}
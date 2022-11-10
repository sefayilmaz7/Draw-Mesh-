using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject balloon;
    [SerializeField] private Color[] balloonColors;
    [SerializeField] private Rigidbody connectedBody;

    private void SpawnAndInitialize()
    {
        var spawnedBalloon = ObjectPool.Instance.GetPooledObject();
        if(!spawnedBalloon)
            return;
        spawnedBalloon.transform.position = Vector3.zero;
        spawnedBalloon.GetComponent<MeshRenderer>().material.color = balloonColors[Random.Range(0, balloonColors.Length -1)];
        var joint = spawnedBalloon.AddComponent<SpringJoint>();
        SetJointConfig(joint);
    }

    private void SetJointConfig(SpringJoint joint)
    {
        joint.connectedBody = connectedBody;
        // We can decrease the max distance for lower radius of balloons
        joint.maxDistance = 2;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAndInitialize) , 0.2f, 2);
    }
}

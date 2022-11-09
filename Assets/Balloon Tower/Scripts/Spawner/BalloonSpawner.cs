using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject balloon;
    [SerializeField] private Color[] balloonColors;
    [SerializeField] private Rigidbody connectedBody;

    public static event UnityAction UpdateBoundaries;

    private void SpawnAndInitialize()
    {
        var spawnedBalloon = ObjectPool.Instance.GetPooledObject();
        spawnedBalloon.transform.position = Random.insideUnitCircle;
        spawnedBalloon.GetComponent<MeshRenderer>().material.color = balloonColors[Random.Range(0, balloonColors.Length -1)];
        var joint = spawnedBalloon.AddComponent<SpringJoint>();
        SetJointConfig(joint);
        UpdateBoundaries?.Invoke();
    }

    private void SetJointConfig(SpringJoint joint)
    {
        joint.maxDistance = 2;
        joint.connectedBody = connectedBody;
        joint.spring = 0;
    }

    private void Start()
    {
        InvokeRepeating("SpawnAndInitialize" , 0.2f, 2);
    }
}

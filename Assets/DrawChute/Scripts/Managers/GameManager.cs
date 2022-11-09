using System;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10)]
public class GameManager : MonoBehaviour
{
    public static event UnityAction GameStarted;

    private void Start()
    {
        GameStarted?.Invoke();
    }

    private void OnEnable()
    {
        GameStarted += SetGameConfig;
    }

    private void OnDisable()
    {
        GameStarted -= SetGameConfig;
    }

    private void SetGameConfig()
    {
        Application.targetFrameRate = 60;
    }
}

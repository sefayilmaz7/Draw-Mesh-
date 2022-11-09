using System;
using UnityEngine;
using DG.Tweening;

public class DrawArea : MonoBehaviour
{
    [SerializeField] private GameObject drawAreaPanel;

    private Vector3 defaultScale;

    private void Awake()
    {
        defaultScale = drawAreaPanel.transform.localScale;
    }

    private void InitArea()
    {
        drawAreaPanel.transform.DOScale(defaultScale, 0.8f).SetEase(Ease.InOutCirc).From(0);
    }

    private void OnEnable()
    {
        GameManager.GameStarted += InitArea;
    }

    private void OnDisable()
    {
        GameManager.GameStarted -= InitArea;
    }
}

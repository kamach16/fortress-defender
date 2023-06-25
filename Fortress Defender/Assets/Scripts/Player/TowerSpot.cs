using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    [SerializeField] private Transform canvas;

    private void Start()
    {
        LookAtCamera();
    }

    private void OnEnable()
    {
        GameManager.OnLevelWin += ShowCanvas;
        GameManager.OnNewLevelStarted += HideCanvas;
    }

    private void OnDisable()
    {
        GameManager.OnLevelWin -= ShowCanvas;
        GameManager.OnNewLevelStarted -= HideCanvas;
    }

    private void LookAtCamera()
    {
        canvas.LookAt(Camera.main.transform);
    }

    private void ShowCanvas()
    {
        canvas.gameObject.SetActive(true);
    }

    private void HideCanvas()
    {
        canvas.gameObject.SetActive(false);
    }
}

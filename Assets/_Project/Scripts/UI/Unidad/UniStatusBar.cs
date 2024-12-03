using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniStatusBar : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public RectTransform myTransform;
    [SerializeField] public Transform target;

    
    private Camera mainCamera = null;

    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void LateUpdate()
    {
        myTransform.position = mainCamera.WorldToScreenPoint(target.position);
    }
}

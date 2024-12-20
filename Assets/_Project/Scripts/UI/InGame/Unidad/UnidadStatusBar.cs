using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnidadStatusBar : MonoBehaviour
{
    [SerializeField] private Slider statusBar;

    private Transform barTrans;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update() => FallowUnit();

    public void Init(Transform barTrans) => this.barTrans = barTrans;

    public void SetBar(float value) => statusBar.value = value;

    private void FallowUnit() => transform.position = cam.WorldToScreenPoint(barTrans.position);
}

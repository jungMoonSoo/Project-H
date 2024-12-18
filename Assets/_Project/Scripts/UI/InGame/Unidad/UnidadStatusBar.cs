using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnidadStatusBar : MonoBehaviour
{
    private Slider statusBar;
    private Transform barTrans;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        TryGetComponent(out statusBar);
    }

    private void Update() => FallowUnit();

    public void Init(Transform _barTrans) => barTrans = _barTrans;

    public void SetBar(float _value) => statusBar.value = _value;

    private void FallowUnit() => transform.position = cam.WorldToScreenPoint(barTrans.position);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnidadHpBar : MonoBehaviour
{
    private Slider hpBar;
    private Transform barTrans;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        TryGetComponent(out hpBar);
    }

    private void Update() => FallowUnit();

    public void Init(Transform _barTrans) => barTrans = _barTrans;

    public void SetBar(float _value) => hpBar.value = _value;

    private void FallowUnit() => transform.position = cam.WorldToScreenPoint(barTrans.position);
}

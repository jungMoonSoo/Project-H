using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnidadStatusBar : MonoBehaviour
{
    [SerializeField] private Slider statusBar;

    private Unidad unidad;
    private Transform barTrans;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update() => FallowUnit();

    public void Init(Unidad unidad, Transform barTrans)
    {
        this.unidad = unidad;
        this.barTrans = barTrans;
    }

    public void SetBar(float value) => statusBar.value = value;

    private void FallowUnit()
    {
        Vector3 dir = cam.transform.rotation * (barTrans.position - unidad.transform.position);

        transform.position = cam.WorldToScreenPoint(unidad.transform.position + dir);
    }
}

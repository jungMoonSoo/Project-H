using UnityEngine;
using UnityEngine.UI;

public class UnidadStatusBar : MonoBehaviour
{
    [SerializeField] private Slider statusBar;
    [SerializeField] private Image fillImage;

    [SerializeField] private Sprite[] fillSprites;

    private Transform barTrans;

    private Camera cam;

    private void Start() => cam = Camera.main;

    private void Update() => FallowUnit();

    public void Init(Transform barTrans, UnitType type)
    {
        this.barTrans = barTrans;

        fillImage.sprite = type switch
        {
            UnitType.Ally => fillSprites[0],
            UnitType.Enemy => fillSprites[1],
            _ => fillSprites[0],
        };
    }

    public void SetBar(float value) => statusBar.value = value;

    private void FallowUnit() => transform.position = cam.WorldToScreenPoint(barTrans.position);
}

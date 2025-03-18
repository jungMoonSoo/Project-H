using UnityEngine.Pool;
using UnityEngine;

public class TextPopupManager : Singleton<TextPopupManager>
{
    [Header("ObjectPool Settings")]
    [SerializeField] private TextPopup popupPrefab;
    [SerializeField] private Transform popupParent;
    private IObjectPool<TextPopup> popupPool;

    [Header("Color Settings")]
    [SerializeField] private Color32 onDamageColor;
    [SerializeField] private Color32 onHealColor;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        popupPool = new ObjectPool<TextPopup>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);
    }

    public void PopupDamage(string text, Vector3 noticePos) => PopupText(text, noticePos, onDamageColor);

    public void PopupHeal(string text, Vector3 noticePos) => PopupText(text, noticePos, onHealColor);

    private void PopupText(string text, Vector3 noticePos, Color32 color)
    {
        TextPopup _textPopup = popupPool.Get();

        _textPopup.SetText(text);
        _textPopup.SetColor(color);

        _textPopup.SetPosition(cam.WorldToScreenPoint(noticePos));

        _textPopup.Show();
    }

    private TextPopup CreateObject()
    {
        TextPopup textPopup = Instantiate(popupPrefab, popupParent);

        textPopup.SetPool(popupPool);

        return textPopup;
    }

    private void OnGetObject(TextPopup textPopup) => textPopup.gameObject.SetActive(true);

    private void OnReleseObject(TextPopup textPopup) => textPopup.gameObject.SetActive(false);

    private void OnDestroyObject(TextPopup textPopup) => Destroy(textPopup.gameObject);
}

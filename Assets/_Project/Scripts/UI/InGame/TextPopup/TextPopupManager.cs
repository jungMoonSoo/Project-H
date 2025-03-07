using UnityEngine;

public class TextPopupManager : Singleton<TextPopupManager>
{
    [Header("ObjectPool Settings")]
    [SerializeField] private TextPopup popupObject;
    [SerializeField] private Transform popupParent;
    private ObjectPool<TextPopup> popupObjects;

    [Header("Color Settings")]
    [SerializeField] private Color32 onDamageColor;
    [SerializeField] private Color32 onHealColor;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        popupObjects = new(popupObject)
        {
            OnEnqueue = OnEnqueue,
            OnDequeue = OnDequeue
        };

        for (int i = 0; i < 10; i++) popupObjects.CreateDefault(popupParent);
    }

    public void PopupDamage(string text, Vector3 noticePos) => PopupText(text, noticePos, onDamageColor);

    public void PopupHeal(string text, Vector3 noticePos) => PopupText(text, noticePos, onHealColor);

    private void PopupText(string text, Vector3 noticePos, Color32 color)
    {
        TextPopup _textPopup = popupObjects.Dequeue(popupParent);

        _textPopup.SetText(text);
        _textPopup.SetColor(color);

        _textPopup.SetPosition(cam.WorldToScreenPoint(noticePos));

        _textPopup.Show();
    }

    private void OnEnqueue(TextPopup textPopup)
    {
        textPopup.SetActive(false);
    }

    private void OnDequeue(TextPopup textPopup)
    {
        if (!textPopup.IsInitialized()) textPopup.Init(popupObjects);

        textPopup.SetActive(true);
    }
}

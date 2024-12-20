using System.Collections;
using System.Collections.Generic;
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

        for (int i = 0; i < 10; i++) popupObjects.Enqueue(Instantiate(popupObject, popupParent));
    }

    public void PopupDamage(string text, Vector2 position) => PopupText(text, position, onDamageColor);

    public void PopupHeal(string text, Vector2 position) => PopupText(text, position, onHealColor);

    private void PopupText(string text, Vector2 position, Color32 color)
    {
        TextPopup _textPopup = popupObjects.Dequeue(popupParent);

        _textPopup.SetText(text);
        _textPopup.SetColor(color);
        _textPopup.SetPosition(cam.WorldToScreenPoint(position));

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

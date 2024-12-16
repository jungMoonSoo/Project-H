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
    }

    public void PopupDamage(string _text, Vector2 _position) => PopupText(_text, _position, onDamageColor);

    public void PopupHeal(string _text, Vector2 _position) => PopupText(_text, _position, onHealColor);

    private void PopupText(string _text, Vector2 _position, Color32 _color)
    {
        TextPopup _textPopup = popupObjects.Dequeue(popupParent);

        _textPopup.SetText(_text);
        _textPopup.SetColor(_color);
        _textPopup.SetPosition(cam.WorldToScreenPoint(_position));

        _textPopup.Show();
    }

    private void OnEnqueue(TextPopup _textPopup)
    {
        _textPopup.SetActive(false);
    }

    private void OnDequeue(TextPopup _textPopup)
    {
        if (!_textPopup.IsInitialized()) _textPopup.Init(popupObjects);

        _textPopup.SetActive(true);
    }
}

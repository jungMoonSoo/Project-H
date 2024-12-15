using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopupHandle : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void Init()
    {
        if (text == null) TryGetComponent(out text);
    }

    public void SetText(string _text) => text.text = _text;

    public void SetColor(Color32 _color) => text.color = _color;

    public void SetAlpha(float _alpha) => text.alpha = _alpha;

    public void SetAlpha(int _alpha) => text.alpha = _alpha / 255f;

    public void SetPosition(Vector2 _position) => transform.position = _position;

    public float GetAlpha() => text.alpha;

    public Vector2 GetPosition() => transform.position;
}

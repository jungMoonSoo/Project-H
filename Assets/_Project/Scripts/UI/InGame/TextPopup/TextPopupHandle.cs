using TMPro;
using UnityEngine;

public class TextPopupHandle : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void Init()
    {
        if (text == null) TryGetComponent(out text);
    }

    public void SetText(string text) => this.text.text = text;

    public void SetColor(Color32 color) => text.color = color;

    public void SetAlpha(float alpha) => text.alpha = alpha;

    public void SetAlpha(int alpha) => text.alpha = alpha / 255f;

    public void SetPosition(Vector2 position) => transform.position = position;

    public float GetAlpha() => text.alpha;

    public Vector2 GetPosition() => transform.position;
}

using System.Collections;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private ObjectPool<TextPopup> popupObjects;

    public void Init(ObjectPool<TextPopup> popupObjects)
    {
        this.popupObjects = popupObjects;

        if (text == null) TryGetComponent(out text);
    }

    public void Show() => StartCoroutine(TextAnim());

    public void Hide() => popupObjects.Enqueue(this);

    public void SetActive(bool value) => gameObject.SetActive(value);

    public void SetText(string text) => this.text.text = text;

    public void SetColor(Color32 color) => text.color = color;

    public void SetAlpha(float alpha) => text.alpha = alpha;

    public void SetAlpha(int alpha) => text.alpha = alpha / 255f;

    public void SetPosition(Vector3 position) => transform.position = position;

    public float GetAlpha() => text.alpha;

    public Vector3 GetPosition() => transform.position;

    private IEnumerator TextAnim()
    {
        SetAlpha(3f);

        while (GetAlpha() > 0f)
        {
            SetAlpha(GetAlpha() - Time.deltaTime * 3);
            SetPosition(GetPosition() + 100 * Time.deltaTime * Vector3.up);

            yield return null;
        }

        Hide();
    }
}

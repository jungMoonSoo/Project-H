using System.Collections;
using TMPro;
using UnityEngine.Pool;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text textPrefab;

    private IObjectPool<TextPopup> popupPool;

    public void SetPool(IObjectPool<TextPopup> pool)
    {
        popupPool = pool;

        if (textPrefab == null) TryGetComponent(out textPrefab);
    }

    public void Show() => StartCoroutine(TextAnim());

    public void Hide() => popupPool.Release(this);

    public void SetActive(bool value) => gameObject.SetActive(value);

    public void SetText(string text) => this.textPrefab.text = text;

    public void SetColor(Color32 color) => textPrefab.color = color;

    public void SetAlpha(float alpha) => textPrefab.alpha = alpha;

    public void SetAlpha(int alpha) => textPrefab.alpha = alpha / 255f;

    public void SetPosition(Vector3 position) => transform.position = position;

    public float GetAlpha() => textPrefab.alpha;

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

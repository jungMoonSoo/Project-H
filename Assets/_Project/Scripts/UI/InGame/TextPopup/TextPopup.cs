using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextPopupHandle))]
public class TextPopup : MonoBehaviour
{
    private TextPopupHandle handle;
    private ObjectPool<TextPopup> popupObjects;

    public void Init(ObjectPool<TextPopup> popupObjects)
    {
        this.popupObjects = popupObjects;

        TryGetComponent(out handle);

        handle.Init();
    }

    public bool IsInitialized() => handle != null;

    public void Show() => StartCoroutine(TextAnim());

    public void Hide() => popupObjects.Enqueue(this);

    public void SetActive(bool value) => gameObject.SetActive(value);

    public void SetText(string text) => handle.SetText(text);

    public void SetColor(Color32 color) => handle.SetColor(color);

    public void SetPosition(Vector3 position) => handle.SetPosition(position);

    private IEnumerator TextAnim()
    {
        handle.SetAlpha(3f);

        while (handle.GetAlpha() > 0f)
        {
            handle.SetAlpha(handle.GetAlpha() - Time.deltaTime * 3);
            handle.SetPosition(handle.GetPosition() + 100 * Time.deltaTime * Vector3.up);

            yield return null;
        }

        Hide();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextPopupHandle))]
public class TextPopup : MonoBehaviour
{
    private TextPopupHandle handle;
    private ObjectPool<TextPopup> popupObjects;

    public void Init(ObjectPool<TextPopup> _popupObjects)
    {
        popupObjects = _popupObjects;

        TryGetComponent(out handle);

        handle.Init();
    }

    public bool IsInitialized() => handle != null;

    public void Show() => StartCoroutine(TextAnim());

    public void Hide() => popupObjects.Enqueue(this);

    public void SetActive(bool _value) => gameObject.SetActive(_value);

    public void SetText(string _text) => handle.SetText(_text);

    public void SetColor(Color32 _color) => handle.SetColor(_color);

    public void SetPosition(Vector2 _position) => handle.SetPosition(_position);

    private IEnumerator TextAnim()
    {
        handle.SetAlpha(1f);

        while (handle.GetAlpha() > 0f)
        {
            handle.SetAlpha(handle.GetAlpha() - Time.deltaTime);
            handle.SetPosition(handle.GetPosition() + 100 * Time.deltaTime * Vector2.up);

            yield return null;
        }

        Hide();
    }
}

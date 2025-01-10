using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TouchCollider : MonoBehaviour
{
    [SerializeField] private Unidad unidad;

    public UnitType UnitType => unidad.Owner;

    private BoxCollider2D unitCollider;

    private void Awake() => TryGetComponent(out unitCollider);

    public void PickUnit() => unidad.ChangeState(UnitState.Pick);

    public void DropUnit() => unidad.ChangeState(UnitState.Ready);

    public void SetUnitPos(Vector2 pos) => unidad.transform.position = pos;

    public void SetActiveCollider(bool _active) => unitCollider.enabled = _active;

    public T GetHitComponent<T>(int layerMask = -1) where T : class
    {
        TryGetHitComponent(out T component, layerMask);

        return component;
    }

    public bool TryGetHitComponent<T>(out T component,int layerMask = -1) where T : class
    {
        SetActiveCollider(false);

        RaycastHit2D hit = Physics2D.Raycast(unidad.transform.position, Vector2.zero, 1f, layerMask);

        SetActiveCollider(true);

        if (hit.collider != null && hit.collider.TryGetComponent(out component)) return true;

        component = null;

        return false;
    }
}

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class UnidadColliderHandle : MonoBehaviour
{
    [SerializeField] private Unidad unidad;

    public UnitType UnitType => unidad.Owner;

    private BoxCollider2D unitCollider;

    private void Start() => TryGetComponent(out unitCollider);

    public void SetUnitPos(Vector2 pos) => unidad.transform.position = pos;

    public void SetActiveCollider(bool _active) => unitCollider.enabled = _active;

    public T GetHitComponent<T>() where T : class
    {
        SetActiveCollider(false);

        RaycastHit2D hit = Physics2D.Raycast(unidad.transform.position, Vector2.zero);

        SetActiveCollider(true);

        if (hit.collider != null && hit.collider.TryGetComponent(out T component)) return component;

        return null;
    }
}

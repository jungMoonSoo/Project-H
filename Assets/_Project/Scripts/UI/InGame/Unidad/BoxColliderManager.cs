using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxColliderManager : MonoBehaviour
{
    [SerializeField] private Unidad unidad;

    private Vector3 size;
    private BoxCollider unitCollider;

    public UnitType UnitType => unidad.Owner;

    public Vector3 Size => size;

    private void Awake()
    {
        TryGetComponent(out unitCollider);

        size = unitCollider.size;
    }

    public void PickUnit() => unidad.ChangeState(UnitState.Pick);

    public void DropUnit() => unidad.ChangeState(UnitState.Ready);

    public void SetUnitPos(Vector3 pos) => unidad.transform.position = pos;

    public void SetActiveCollider(bool _active) => unitCollider.enabled = _active;

    public T GetHitComponent<T>(int layerMask = -1) where T : class
    {
        TryGetHitComponent(out T component, layerMask);

        return component;
    }

    public bool TryGetHitComponent<T>(out T component,int layerMask = -1) where T : class
    {
        SetActiveCollider(false);

        RaycastHit[] hit = new RaycastHit[1];

        int length = Physics.defaultPhysicsScene.Raycast(unidad.transform.position + Vector3.up, Vector3.down, hit, layerMask : layerMask);

        SetActiveCollider(true);

        if (length != 0 && hit[0].collider.TryGetComponent(out component)) return true;

        component = null;

        return false;
    }
}

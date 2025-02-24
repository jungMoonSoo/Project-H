using UnityEngine;

public interface ICustomCollider
{
    public Vector2 Radius { get; }
    public Vector3 Center { get; }

    public bool OnEnter(ICustomCollider coll);
}

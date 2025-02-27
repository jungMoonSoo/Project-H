using UnityEngine;

public interface ICustomCollider
{
    public Vector2 AreaSize { get; }

    public Vector2 Radius { get; }
    public Vector3 Center { get; }

    public Vector3 Direction { get; set; }

    public bool OnEnter(ICustomCollider coll);
}

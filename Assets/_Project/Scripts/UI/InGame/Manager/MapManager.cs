using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    public Vector2 mapPos;
    public Vector2 mapSize;

    public Vector3 ClampPositionToMap(Vector3 pos, Vector2 colliderSize)
    {
        float dist = (mapPos.x - mapSize.x * 0.5f) - (pos.x - colliderSize.x);

        if (dist > 0) pos.x += dist;

        dist = (mapPos.x + mapSize.x * 0.5f) - (pos.x + colliderSize.x);

        if (dist < 0) pos.x += dist;

        dist = (mapPos.y - mapSize.y * 0.5f) - (pos.z - colliderSize.y);

        if (dist > 0) pos.z += dist;

        dist = (mapPos.y + mapSize.y * 0.5f) - (pos.z + colliderSize.y);

        if (dist < 0) pos.z += dist;

        return pos;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Vector3 mapSize = new Vector3(this.mapSize.x, 0, this.mapSize.y) * 0.5f;
        Vector3 mapPos = new(this.mapPos.x, 0, this.mapPos.y);

        Vector3 bottomLeft = mapPos - mapSize;
        Vector3 topRight = mapPos + mapSize;

        Gizmos.DrawLine(bottomLeft, new Vector3(bottomLeft.x, 0, topRight.z));
        Gizmos.DrawLine(bottomLeft, new Vector3(topRight.x, 0, bottomLeft.z));
        Gizmos.DrawLine(new Vector3(topRight.x, 0, bottomLeft.z), topRight);
        Gizmos.DrawLine(new Vector3(bottomLeft.x, 0, topRight.z), topRight);
    }
#endif
}

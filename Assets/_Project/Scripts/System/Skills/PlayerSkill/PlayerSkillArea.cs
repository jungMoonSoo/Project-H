using UnityEngine;

public class PlayerSkillArea : MonoBehaviour
{
    [SerializeField] private Transform areaTransform;

    public void SetSize(float sizeX, float sizeY)
    {
        SetSize(new Vector2(sizeX, sizeY));
    }
    public void SetSize(Vector2 size)
    {
        areaTransform.localScale = size;
    }
}

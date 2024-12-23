using Unity.VisualScripting;
using UnityEngine;

public class LinearSkillArea: SkillAreaBase, ISkillArea
{
    public byte SpriteCode => 1;
    
    public void SetPosition(Vector3 worldPosition)
    {
        Transform.position = InGameManager.Instance.PlayerTransform.position + Vector3.forward;

        float angle = VectorCalc.CalcRotation(worldPosition - Transform.position);
        Transform.eulerAngles.Set(0, 0, angle);
    }
}
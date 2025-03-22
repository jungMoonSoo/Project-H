using Spine.Unity;
using UnityEngine;

public class SpineSkillHandle : MonoBehaviour
{
    [SerializeField] private SpineEffectHandle effectHandle;
    [SerializeField] private int effectIndex;

    private Vector3 targetPos;

    public void SetTargetPos(Vector3 targetPos) => this.targetPos = targetPos;

    /// <summary>
    /// 스킬 사용 시 HitObject 생성
    /// </summary>
    /// <param name="unidad">시전자</param>
    public void Spawn(Unidad unidad)
    {
        HitObject hitObject = Instantiate(unidad.Status.skillInfo?.effectPrefab);

        hitObject.SetTarget(targetPos);

        if (effectHandle != null)
        {
            SpineBoneData bone = effectHandle.GetBoneInfo(effectIndex);

            hitObject.SetEffect(bone.effectManager);
            hitObject.Init(unidad, bone.bone.GetWorldPosition(unidad.View));
        }
        else hitObject.Init(unidad, unidad.transform.position);
    }
}

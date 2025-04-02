using Spine.Unity;
using UnityEngine;

public class SpineSkillHandle : MonoBehaviour
{
    [SerializeField] private HitObjectManager hitObjectManager;

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
        if (unidad.Status.skillInfo == null) return;

        hitObjectManager.SetPrefab(unidad.Status.skillInfo.effectPrefab);

        HitObject hitObject = hitObjectManager.HitObjectPool.Get();

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

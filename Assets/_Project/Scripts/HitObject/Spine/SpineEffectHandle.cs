using Spine.Unity;
using UnityEngine;

public class SpineEffectHandle : MonoBehaviour
{
    [SerializeField] private Transform view;
    [SerializeField] private SkeletonAnimation skeletonAnimation;

    [SerializeField] private SpineBoneData[] bones;

    private void Awake()
    {
        for (int i = 0; i < bones.Length; i++)
        {
            if (bones[i].boneName == "") continue;

            bones[i].bone = skeletonAnimation.skeleton.FindBone(bones[i].boneName);
        }
    }

    public SpineBoneData GetBoneInfo(int index) => bones[index];

    /// <summary>
    /// 특정 Bone 위치에 Effect 생성
    /// </summary>
    /// <param name="index">bones 배열 index</param>
    public void SpawnEffect(int index)
    {
        if (bones[index].effectManager == null) return;

        EffectSystem effect = bones[index].effectManager.EffectPool.Get();

        effect.transform.SetParent(transform);
        effect.transform.position = bones[index].bone.GetWorldPosition(view);
    }

    /// <summary>
    /// 특정 Bone Index의 EffectManager에서 미리 Effect 생성 (현재 공격, 피격, 스킬 Effect 순으로 기록되어 있음)
    /// </summary>
    /// <param name="index">bones Index</param>
    /// <param name="count">생성할 Effect 수</param>
    public void CreateDefaultEffect(int index, int count)
    {
        if (bones.Length <= index) return;

        bones[index].effectManager.CreateDefault(count);
    }
}

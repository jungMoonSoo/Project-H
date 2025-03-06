using Spine.Unity;
using UnityEngine;

public class SpineEffectHandle : MonoBehaviour
{
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

    public void SpawnEffect(int index)
    {
        EffectSystem effect = bones[index].effectManager.GetEffect(transform);

        effect.transform.position = bones[index].bone.GetWorldPosition(transform);
    }
}

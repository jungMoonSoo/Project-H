using Unity.VisualScripting;
using UnityEngine;

public class CheckLinearHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float splitCount = 1;

    private Vector3 targetPos;

    private float nowDist;
    private float endDist;
    private float splitDist;

    private int applyCount;

    public void Init(HitObjectBase @base)
    {
        applyCount = 0;
        targetPos = @base.TargetPos;

        endDist = Vector3.Distance(transform.position, targetPos);
        splitDist = endDist / splitCount;
    }

    public void Check(HitObjectBase @base)
    {
        if (Move())
        {
            @base.OnTrigger();
            applyCount++;
        }

        if (transform.position == targetPos) @base.OnFinish();
    }

    private bool Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        nowDist = Vector3.Distance(transform.position, targetPos);

        return nowDist <= endDist - splitDist * (applyCount + 1);
    }
}

using UnityEngine;

public class CheckLinearHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float splitCount = 1;

    private bool init;

    private float nowDist;
    private float endDist;
    private float splitDist;

    private int applyCount;

    private Vector3 targetPos;

    private readonly float offset = 0.01f;

    public void Check(HitObjectBase @base)
    {
        if (!init) Init(@base);

        if (Move()) return;

        @base.OnTrigger();
        applyCount++;

        if (nowDist < offset)
        {
            init = false;
            applyCount = 0;

            @base.OnFinish();
        }
    }

    private void Init(HitObjectBase @base)
    {
        init = true;

        transform.position = @base.Caster.transform.position;

        targetPos = @base.TargetPos;

        endDist = Vector3.Distance(transform.position, targetPos);
        splitDist = endDist / splitCount;
    }

    private bool Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        nowDist = Vector3.Distance(transform.position, targetPos);

        return endDist - splitDist * (applyCount + 1) < nowDist + offset;
    }
}

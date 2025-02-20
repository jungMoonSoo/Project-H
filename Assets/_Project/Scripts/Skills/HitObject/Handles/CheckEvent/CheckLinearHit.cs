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

    public void Check(HitObjectBase @base)
    {
        if (!init) Init(@base);

        if (Move())
        {
            if (nowDist < 0.01f)
            {
                init = false;
                applyCount = 0;

                @base.OnFinish();
            }

            return;
        }

        @base.OnTrigger();
        applyCount++;
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
        Vector3 pos = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        transform.position = pos;

        nowDist = Vector3.Distance(transform.position, targetPos);

        return endDist - nowDist < splitDist * (applyCount + 1);
    }
}

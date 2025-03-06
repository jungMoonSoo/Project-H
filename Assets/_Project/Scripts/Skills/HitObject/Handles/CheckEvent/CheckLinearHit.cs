using UnityEngine;

public class CheckLinearHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float splitCount = 0;

    [SerializeField] private bool straight = true;

    private Vector3 targetPos;

    private float nowDist;
    private float endDist;
    private float splitDist;

    private int applyCount;

    public void Init(HitObject @base)
    {
        applyCount = 0;

        targetPos = @base.TargetPos;

        if (straight) targetPos.y = transform.position.y;

        endDist = Vector3.Distance(transform.position, targetPos);
        splitDist = endDist / splitCount;
    }

    public void Check(HitObject @base)
    {
        if (transform.position == targetPos)
        {
            if (splitCount == 0) @base.OnTrigger();

            @base.OnFinish();
        }
        else
        {
            Move();

            if (nowDist < endDist - splitDist * applyCount)
            {
                @base.OnTrigger();
                applyCount++;
            }
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        nowDist = Vector3.Distance(transform.position, targetPos);
    }
}

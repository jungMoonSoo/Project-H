using UnityEngine;

public class CheckLinearHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float splitCount = 0;

    [SerializeField] private bool rotate = true;
    [SerializeField] private bool straight = true;

    private Vector3 targetPos;

    private float nowDist;
    private float endDist;
    private float splitDist;

    private int applyCount;

    public void Init(HitObject hitObject)
    {
        applyCount = 0;
    }

    public void Check(HitObject hitObject)
    {
        targetPos = hitObject.TargetPos;

        Refresh();

        if (nowDist < 0.01f)
        {
            if (splitCount == 0) hitObject.OnTrigger();

            hitObject.OnFinish();
        }
        else
        {
            Move();

            if (nowDist < endDist - splitDist * applyCount)
            {
                hitObject.OnTrigger();
                applyCount++;
            }
        }
    }

    private void Refresh()
    {
        if (straight) targetPos.y = transform.position.y;

        nowDist = Vector3.Distance(transform.position, targetPos);
        endDist = nowDist;

        splitDist = endDist / splitCount;
    }

    private void Move()
    {
        if (rotate) Rotate();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        nowDist = Vector3.Distance(transform.position, targetPos);
    }

    private void Rotate()
    {
        Vector3 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle == 180) angle -= 180;
        else if (angle <= -90) angle += 180;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

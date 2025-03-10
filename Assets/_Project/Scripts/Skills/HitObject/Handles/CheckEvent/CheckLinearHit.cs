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

        targetPos = hitObject.TargetPos;

        if (straight) targetPos.y = transform.position.y;

        endDist = Vector3.Distance(transform.position, targetPos);
        splitDist = endDist / splitCount;
    }

    public void Check(HitObject hitObject)
    {
        if (transform.position == targetPos)
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

    private void Move()
    {
        if (rotate)
        {
            Vector3 direction = targetPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle < -90 ? angle + 180 : angle);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        nowDist = Vector3.Distance(transform.position, targetPos);
    }
}

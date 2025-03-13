using UnityEngine;

public class CheckLinearHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float splitCount = 0;

    [SerializeField] private bool rotate = true;

    [SerializeField] private LookCameraSystem lookCamera;

    private Vector3 targetPos;

    private float nowDist;
    private float endDist;
    private float splitDist;

    private int applyCount;

    private bool lookAtRight;

    public void Init(HitObject hitObject)
    {
        applyCount = 0;

        lookAtRight = hitObject.Caster.transform.localScale.x > 0;
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
        if (targetPos.x - transform.position.x < 0) lookCamera.Flip(lookAtRight);
        else lookCamera.Flip(!lookAtRight);

        transform.LookAt(targetPos);
        transform.Rotate(new(0, -90, 0));
    }
}

using UnityEngine;

public class CheckParabolaHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 1;

    [SerializeField] private float height = 5;
    [SerializeField] private float endYPos = 0;

    private Vector3 startPos;
    private Vector3 targetPos;

    private float timer;

    public void Init(HitObjectBase @base)
    {
        timer = 0;

        startPos = transform.position;
        targetPos = @base.TargetPos;
    }

    public void Check(HitObjectBase @base)
    {
        if (MoveParabola()) return;

        @base.OnTrigger();
        @base.OnFinish();
    }

    private bool MoveParabola()
    {
        if (Vector3.Distance(transform.position, targetPos) > 0.01f && transform.position.y >= endYPos)
        {
            timer += Time.deltaTime * speed;

            Vector3 pos = Vector3.Lerp(startPos, targetPos, timer);

            pos.y = -4 * height * timer * timer + 4 * height * timer + Mathf.Lerp(startPos.y, endYPos, timer);

            transform.position = pos;

            return true;
        }

        return false;
    }
}

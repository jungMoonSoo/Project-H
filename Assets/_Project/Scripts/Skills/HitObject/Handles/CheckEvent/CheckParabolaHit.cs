using UnityEngine;

public class CheckParabolaHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 1;

    [SerializeField] private float height = 5;
    [SerializeField] private float endYPos = 0;

    private Vector3 startPos;
    private Vector3 targetPos;

    private bool check;
    private float timer;

    public void Init(HitObject @base)
    {
        timer = 0;
        check = false;

        startPos = transform.position;
        targetPos = @base.TargetPos;
    }

    public void Check(HitObject @base)
    {
        if (MoveParabola()) return;

        @base.OnTrigger();
        @base.OnFinish();
    }

    private bool MoveParabola()
    {
        if (transform.position.y > endYPos) check = true;

        if (!check || transform.position.y >= endYPos)
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

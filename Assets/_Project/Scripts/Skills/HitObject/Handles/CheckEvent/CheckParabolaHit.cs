using UnityEngine;

public class CheckParabolaHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float height = 5;
    [SerializeField] private float endYPos = 0;

    private bool init;

    private Vector3 startPos;
    private Vector3 targetPos;

    private float timer;

    public void Check(HitObjectBase @base)
    {
        if (!init) Init(@base);

        if (MoveParabola()) return;

        @base.OnTrigger();

        init = false;

        @base.OnFinish();
    }

    private void Init(HitObjectBase @base)
    {
        init = true;

        timer = 0;

        transform.position = @base.Caster.transform.position + Vector3.up;

        startPos = transform.position;
        targetPos = @base.TargetPos;
    }

    private bool MoveParabola()
    {
        if (transform.position.y >= endYPos)
        {
            timer += Time.deltaTime;

            Vector3 pos = Vector3.Lerp(startPos, targetPos, timer);

            pos.y = -4 * height * timer * timer + 4 * height * timer + Mathf.Lerp(startPos.y, endYPos, timer);

            transform.position = pos;

            return true;
        }

        return false;
    }
}

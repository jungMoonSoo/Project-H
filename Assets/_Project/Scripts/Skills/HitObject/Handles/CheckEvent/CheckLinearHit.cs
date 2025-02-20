using UnityEngine;

public class CheckLinearHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 10;

    private bool init;

    private Vector3 targetPos;

    public void Check(HitObjectBase @base)
    {
        if (!init) Init(@base);

        if (Move()) return;

        @base.OnTrigger();

        init = false;

        @base.OnFinish();
    }

    private void Init(HitObjectBase @base)
    {
        init = true;

        transform.position = @base.Caster.transform.position;

        targetPos = @base.TargetPos;
    }

    private bool Move()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        transform.position = pos;

        return Vector3.Distance(transform.position, targetPos) > 0.01f;
    }
}

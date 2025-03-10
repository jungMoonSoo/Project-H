using UnityEngine;

public class CheckParabolaHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 1;

    [SerializeField] private float height = 5;
    [SerializeField] private float endYPos = 0;

    [SerializeField] private bool rotate = true;

    private Vector3 startPos;
    private Vector3 targetPos;

    private bool check;
    private float timer;

    public void Init(HitObject hitObject)
    {
        timer = 0;
        check = false;

        startPos = transform.position;
        targetPos = hitObject.TargetPos;
    }

    public void Check(HitObject hitObject)
    {
        if (MoveParabola()) return;

        hitObject.OnTrigger();
        hitObject.OnFinish();
    }

    private bool MoveParabola()
    {
        if (transform.position.y > endYPos) check = true;

        if (!check || transform.position.y >= endYPos)
        {
            timer += Time.deltaTime * speed;

            Vector3 pos = Vector3.Lerp(startPos, targetPos, timer);

            pos.y = -4 * height * timer * timer + 4 * height * timer + Mathf.Lerp(startPos.y, endYPos, timer);

            if (rotate)
            {
                Vector3 direction = pos - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, angle < -90 ? angle + 180 : angle);
            }

            transform.position = pos;

            return true;
        }

        return false;
    }
}

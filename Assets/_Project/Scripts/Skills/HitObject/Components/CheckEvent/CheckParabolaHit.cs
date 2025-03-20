using UnityEngine;

public class CheckParabolaHit : MonoBehaviour, IHitObjectCheckEvent
{
    [SerializeField] private float speed = 1;

    [SerializeField] private float height = 5;
    [SerializeField] private float endYPos = 0;

    [SerializeField] private bool rotate = true;

    [SerializeField] private LookCameraSystem lookCamera;

    private Vector3 startPos;
    private Vector3 targetPos;

    private bool check;
    private float timer;

    private bool lookAtRight;

    public void Init(Unidad caster)
    {
        timer = 0;
        check = false;

        startPos = transform.position;

        lookAtRight = caster.transform.localScale.x > 0;
    }

    public void OnEvent(HitObject hitObject)
    {
        targetPos = hitObject.TargetPos;

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

            if (rotate) Rotate();

            transform.position = pos;

            return true;
        }

        return false;
    }

    private void Rotate()
    {
        if (targetPos.x - transform.position.x < 0) lookCamera.Flip(lookAtRight);
        else lookCamera.Flip(!lookAtRight);

        transform.LookAt(targetPos);
        transform.Rotate(new(0, -90, 0));
    }
}

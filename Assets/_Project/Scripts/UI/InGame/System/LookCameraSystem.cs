using UnityEngine;

public class LookCameraSystem : MonoBehaviour
{
    [SerializeField] private bool flip = false;

    public void Flip(bool flip)
    {
        if (this.flip == flip) return;

        this.flip = flip;

        transform.Rotate((flip ? -1 : 1) * 2 * LookController.Instance.Rotation);
    }

    private void Start()
    {
        LookController.Instance.actions += Look;

        Look(LookController.Instance.Rotation);
    }

    private void OnDestroy()
    {
        LookController.Instance.actions -= Look;
    }

    private void Look(Vector3 rotation) => transform.Rotate(flip ? -rotation : rotation);
}

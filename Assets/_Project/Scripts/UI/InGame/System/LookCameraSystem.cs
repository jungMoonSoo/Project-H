using UnityEngine;

public class LookCameraSystem : MonoBehaviour
{
    private void Start()
    {
        LookController.Instance.actions += Look;

        Look(LookController.Instance.Rotation);
    }

    private void OnDestroy()
    {
        LookController.Instance.actions -= Look;
    }

    private void Look(Vector3 rotation) => transform.Rotate(rotation);
}

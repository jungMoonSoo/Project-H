using UnityEngine;

public class HitEnableCheck : MonoBehaviour, IHitableCheck
{
    public bool Check() => true;
}

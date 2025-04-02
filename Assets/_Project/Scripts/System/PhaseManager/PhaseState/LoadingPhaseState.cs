using UnityEngine;

// TODO: Unidad가 사용하는 Pooling 미리 작업
// RunPhase로 넘어가기 전에 Pooling작업을 하는 Phase
// 최적화를 위한 Phase로 다른 용도는 존재하지 않아야 함.
public class LoadingPhaseState : MonoBehaviour, IPhaseState
{
    public void OnEnter()
    {
        Unidad[] unidads = GameObject.FindObjectsOfType<Unidad>();
        foreach (Unidad unidad in unidads)
        {
            unidad.SetEffectPool();
            unidad.SetHitObjectPool();
        }
        
        // 준비가 완료되면, RunPhase로 넘어가야 함.
        PhaseManager.Instance.ChangeState(PhaseState.Run);
    }
    
    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}

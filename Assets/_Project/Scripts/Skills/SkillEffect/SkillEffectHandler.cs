using System;
using Unity.VisualScripting;
using UnityEngine;

public class SkillEffectHandler : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TargetingSystemType targetingSystemType;
    [SerializeField] private AnimatorEventHandler animatorEventHandler;
    
    
    #region ◇ Properties ◇
    public ITargetingSystem TargetingSystem
    {
        get
        {
            if (_TargetingSystem is null)
            {
                switch (targetingSystemType)
                {
                    case TargetingSystemType.Single:
                        _TargetingSystem = new SingleTargetingSystem();
                        break;
                    case TargetingSystemType.Linear:
                        _TargetingSystem = new LinearTargetingSystem();
                        break;
                    case TargetingSystemType.Area:
                        _TargetingSystem = new AreaTargetingSystem();
                        break;
                    default:
                        throw new Exception("TargetingSystem Type 미존재.");
                        break;
                }
            }
            
            return _TargetingSystem;
        }
    }
    private ITargetingSystem _TargetingSystem = null;
    #endregion


    void Start()
    {
        animatorEventHandler.OnTriggerEvent = OnTrigger;
        animatorEventHandler.OnFinishEvent = OnFinish;
        Animator anim;
    }

    private void OnTrigger()
    {
        
    }

    private void OnFinish()
    {
        Destroy(gameObject);
    }
}
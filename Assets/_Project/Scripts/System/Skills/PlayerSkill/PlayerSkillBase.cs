using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayerSkillBase: MonoBehaviour, IPlayerSkill
{
    public abstract GameObject AreaEffect
    {
        get;
    }
    public abstract GameObject SkillEffect
    {
        get;
    }
    public abstract Camera MainCamera
    {
        get;
    }
    public abstract float CoolDown
    {
        get;
    }

    public virtual bool IsCooled
    {
        get => lastUse.AddSeconds(CoolDown) > DateTime.Now;
    }

    private DateTime lastUse = DateTime.MinValue;
    private Transform areaEffectTrans = null;
    private Vector3 lastDragedPosition = Vector3.zero;

    public virtual void Execute()
    {
        lastUse = DateTime.Now;
        StartCoroutine(nameof(StartCoolDown));
        OnSkill(lastDragedPosition);
    }

    public virtual void OnSelect()
    {
        if (!IsCooled)
        {
            InGameManager.Instance.PauseGame(PauseType.UseSkill);
            if(AreaEffect != null)
            {
                areaEffectTrans = Instantiate(AreaEffect).transform;
            }
        }
        else
        {
            CooledSkill();
        }
    }
    public virtual void OnDrag(Vector2 position)
    {
        lastDragedPosition = MainCamera.ScreenToWorldPoint(position) + new Vector3(0, 0, 1);
        if (areaEffectTrans != null)
        {
            areaEffectTrans.position = lastDragedPosition;
        }
    }
    public virtual void EndAction()
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);
        if(areaEffectTrans != null)
        {
            Destroy(areaEffectTrans.gameObject);
        }
    }



    protected virtual IEnumerator StartCoolDown()
    {
        BeginCoolDown();
        yield return new WaitForSeconds(CoolDown);
        AfterCoolDown();
    }





    protected abstract void OnSkill(Vector3 position);
    protected abstract void CooledSkill();

    protected abstract void BeginCoolDown();
    protected abstract void AfterCoolDown();
}
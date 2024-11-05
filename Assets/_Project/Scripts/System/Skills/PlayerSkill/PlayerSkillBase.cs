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
    public abstract float CoolDown
    {
        get;
    }
    public virtual bool IsCooled
    {
        get;
        private set;
    }

    private Transform areaEffectTrans = null;
    private Vector3 lastDragedPosition = Vector3.zero;

    public virtual void Execute()
    {
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
    public virtual void OnDrag(Vector3 position)
    {
        lastDragedPosition = position + new Vector3(0, 0, 1);
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



    /// <summary>
    /// 쿨타임 구현용 Coroutine Method
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartCoolDown()
    {
        IsCooled = true;
        BeginCoolDown();
        yield return new WaitForSeconds(CoolDown);
        IsCooled = false;
        AfterCoolDown();
    }



    #region ◇ 추상화 메소드 ◇
    /// <summary>
    /// 스킬 동작 부분
    /// </summary>
    /// <param name="position">스킬 사용 위치</param>
    protected abstract void OnSkill(Vector3 position);
    /// <summary>
    /// 스킬 쿨타임 때 요청 시
    /// </summary>
    protected abstract void CooledSkill();

    /// <summary>
    /// 쿨타임 전 동작<br/>
    /// UI 변경에 사용하기 위함
    /// </summary>
    protected abstract void BeginCoolDown();
    /// <summary>
    /// 쿨타임 종료 동작<br/>
    /// UI 변경에 사용하기 위함
    /// </summary>
    protected abstract void AfterCoolDown();
    #endregion
}
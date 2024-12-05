using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class ActionSkillBase: MonoBehaviour, IActionSkill
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

    protected abstract ISkillArea SkillArea
    {
        get;
    }
    protected abstract Sprite SkillAreaSprite
    {
        get;
    }

    private Vector3 lastDragedPosition = Vector3.zero; 
    private GameObject skillAreaObject = null;

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
            if (AreaEffect == null) return;
            
            skillAreaObject = Instantiate(AreaEffect);
            SkillArea.GameObject = skillAreaObject;
            SkillArea.SetSprite(SkillAreaSprite);
            SetAreaSize(SkillArea);
        }
        else
        {
            CooledSkill();
        }
    }
    public virtual void OnDrag(Vector3 position)
    {
        lastDragedPosition = position + new Vector3(0, 0, 1);
        if (skillAreaObject != null)
        {
            SkillArea.SetPosition(lastDragedPosition);
        }
    }
    public virtual void EndAction()
    {
        InGameManager.Instance.ResumeGame(PauseType.UseSkill);
        if (skillAreaObject == null) return;
        
        Destroy(skillAreaObject);
        skillAreaObject = null;
    }



    /// <summary>
    /// 쿨타임 구현용 Coroutine Method
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartCoolDown()
    {
        IsCooled = true;
        SkillCooldown?.BeginCoolDown(CoolDown);
        yield return new WaitForSeconds(CoolDown);
        IsCooled = false;
        SkillCooldown?.AfterCoolDown();
    }



    #region ◇ 추상화 부분 ◇
    /// <summary>
    /// 스킬 쿨타임 UI 조작 인터페이스
    /// </summary>
    public abstract IPlayerSkillCooldown SkillCooldown
    {
        get;
    }

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
    /// 플레이어 스킬 아레아 확인용 스크립트에 수정이 필요할 때 사용하는 Method
    /// </summary>
    /// <param name="skillArea"></param>
    protected abstract void SetAreaSize(ISkillArea skillArea);
    #endregion
}
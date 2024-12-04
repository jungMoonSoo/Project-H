using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatusCalc
{
    public static int GetDamage(bool _isActive, DamageStatus _dmgStatus, GuardStatus guardStatus, int _fd)
    {
        if (_dmgStatus.atk < 0) return 0;

        int _accInc = 0; // 추가 명중률
        int _atkIncAdd = 0;
        int _atkIncMul = 0;
        int _criInc = 0; // 추가 치명타 확률
        int _crpIncAdd = 0; // 추가 치명타 피해량 합

        int _dodInc = 0; // 추가 회피율
        int _caInc = 0; // 추가 치명타 저항률
        int _defIncAdd = 0; // 추가 방어력 합
        int _defIncMul = 0; // 추가 방어력 곱

        // 명중 체크
        if (Random.Range(0, 101) > (_dmgStatus.acc + _accInc) - (guardStatus.dod + _dodInc)) return 0;

        float _dmg = CalcAtk(_dmgStatus.atk, _atkIncAdd, _atkIncMul, _dmgStatus.skp);

        if (!_isActive) _dmg *= CheckCriDmg(_dmgStatus.cri + _criInc, _dmgStatus.crp + _crpIncAdd, guardStatus.ca + _caInc);

        _dmg *= CheckDef(guardStatus.def, _defIncAdd, _defIncMul);

        _dmg += _fd; // 고정피해

        return Mathf.FloorToInt(_dmg);
    }

    /// <summary>
    /// 스킬 계수로 인한 피해량 계산
    /// </summary>
    /// <param name="_atk">공격력</param>
    /// <param name="_atkIncAdd">공격력 증가량 (합연산)</param>
    /// <param name="_atkIncMul">공격력 증가량 (곱연산)</param>
    /// <param name="_skp">스킬 계수</param>
    /// <returns></returns>
    private static float CalcAtk(int _atk, int _atkIncAdd, int _atkIncMul, int _skp)
    {
        return (_atk + (_atk * _atkIncMul) + _atkIncAdd) * (_skp * 0.01f);
    }

    /// <summary>
    /// 치명타 데미지 계산
    /// </summary>
    /// <param name="_cri">치명타 확률</param>
    /// <param name="_crp">치명타 데미지</param>
    /// <param name="_ca">치명타 저항률</param>
    /// <returns></returns>
    private static float CheckCriDmg(int _cri, int _crp, int _ca)
    {
        if (Random.Range(0, 101) < _cri - _ca) return _crp * 0.01f;

        return 1;
    }

    /// <summary>
    /// 데미지 감소율 계산
    /// </summary>
    /// <param name="_def">방어력</param>
    /// <param name="_defIncAdd">추가 방어력 (합연산)</param>
    /// <param name="_defIncMul">추가 방어력 (곱연산)</param>
    /// <returns></returns>
    private static float CheckDef(float _def, int _defIncAdd, int _defIncMul)
    {
        _def *= _defIncMul * 0.01f;
        _def += _defIncAdd;

        return 1 - _def / (_def + UnitManager.Instance.DM);
    }

    public static int GetHeal(int _healAmount)
    {
        if (_healAmount < 0) return 0;

        return _healAmount;
    }
}

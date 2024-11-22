using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatusManager : IUnitStatus
{
    private readonly Unit unit;

    public UnitStatus Status { get; }
    public LerpSprite HpBar { get; }

    public UnitStatusManager(Unit _unit, UnitStatus _status, LerpSprite _hpBar)
    {
        unit = _unit;
        Status = _status;
        HpBar = _hpBar;
    }

    public bool OnDamage(bool _isActive, int _acc, int _atk, int _skp, int _cri, int _crp, int _fd)
    {
        if (_atk < 0) return false;

        int _acci = 0; // 추가 명중률
        int _crii = 0; // 추가 치명타 확률
        int _crpi = 0; // 추가 치명타 피해량

        int _dodi = 0; // 추가 회피율

        // 명중 체크
        if (Random.Range(0, 101) > (_acc + _acci) - (Status.dod + _dodi)) return false;

        float _dmg = CalculateDamage(_atk, _skp);

        _dmg = ApplyCriticalDamage(_isActive, _cri + _crii, _crp + _crpi, _dmg);
        _dmg = ApplyDefense(_dmg);

        _dmg += _fd; // 고정피해

        // HP 감소 및 MP 회복
        Status.hp[0].Data -= Mathf.FloorToInt(_dmg);
        Status.mp[0].Data += Mathf.FloorToInt(Status.mpRegen * 0.5f);

        return true;
    }

    private float CalculateDamage(int _atk, int _skp)
    {
        int _atki = 0; // 추가 공격력

        return (_atk + (_atk * _atki) + _atki) * (_skp * 0.01f);
    }

    private float ApplyCriticalDamage(bool _isActive, int _cri, int _crp, float _dmg)
    {
        int _cai = 0; // 추가 치명타 저항률

        if (!_isActive && Random.Range(0, 101) < _cri - (Status.ca + _cai)) _dmg *= _crp * 0.01f;

        return _dmg;
    }

    private float ApplyDefense(float _dmg)
    {
        int _defi = 0; // 추가 방어력
        float _def = Status.def + (_defi * 0.01f) + _defi;

        return _dmg * (1 - _def / (_def + UnitManager.Instance.DM));
    }

    public bool OnDamage(bool _isActive, UnitStatus _targetStatus, int _fd) => OnDamage(_isActive, _targetStatus.acc, _targetStatus.atk, _targetStatus.skp, _targetStatus.cri, _targetStatus.crp, _fd);

    public bool OnHeal(int _healAmount)
    {
        if (_healAmount < 0) return false;

        Status.hp[0].Data += _healAmount;

        return true;
    }
}

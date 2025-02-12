public enum SkillAreaType
{
    /// <summary> Caster 스스로 타겟팅 </summary>
    Me,
    
    /// <summary> Caster를 제외한 싱글 타겟팅 </summary>
    AllySingle,
    /// <summary> Caster를 포함한 싱글 타겟팅 </summary>
    WeSingle,
    /// <summary> 적 싱글 타겟팅 </summary>
    TheySingle,
    
    /// <summary> 땅 타겟팅 </summary>
    Ground,
    /// <summary> 자신을 중심으로한 회전 타겟팅 </summary>
    Rotate,
}

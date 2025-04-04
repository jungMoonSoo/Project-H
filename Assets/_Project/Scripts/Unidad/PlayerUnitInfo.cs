using System;

[Serializable]
public class PlayerUnitInfo
{
    public uint id;
    public uint level;

    public PlayerUnitInfo(uint id, uint level)
    {
        this.id = id;
        this.level = level;
    }
}

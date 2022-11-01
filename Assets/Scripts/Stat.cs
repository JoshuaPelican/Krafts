[System.Serializable]
public class Stat
{
    public StatType type;
    public float value;

    public enum StatType
    {
        Color,
        Creativity,
        Uniqueness,
    }

    public Stat(StatType _type, float _value = 0)
    {
        type = _type;
        value = _value;
    }
}

[System.Serializable]
public class StatMod
{
    public Stat.StatType statType;
    public float value;
}

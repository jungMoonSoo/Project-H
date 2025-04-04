using System.Collections.Generic;

public static class ConvertDict
{
    public static List<U> ConvertValues<T, U>(this Dictionary<T, U> dict)
    {
        List<U> result = new();

        foreach (U value in dict.Values) result.Add(value);

        return result;
    }
}

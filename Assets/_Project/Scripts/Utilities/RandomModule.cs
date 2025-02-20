using System;

public static class RandomModule
{
    public static int GetRandom(int max)
    {
        int microsecond = Convert.ToInt32(DateTime.Now.ToString("ffffff"));

        return new Random(microsecond).Next(max);
    }
}

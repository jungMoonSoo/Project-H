using System;

public static class RandomModule
{
    public static int GetRandom(int seed, int max) => new Random(seed).Next(max);
}

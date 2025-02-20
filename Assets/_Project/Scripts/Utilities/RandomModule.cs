using AmplifyShaderEditor;
using System;
using System.Security.Cryptography;
using UnityEngine;

public static class RandomModule
{
    private static readonly RandomNumberGenerator generator = RandomNumberGenerator.Create();

    public static long GetSecurityRandom(long min, long max)
    {
        byte[] bytes = new byte[8];
        generator.GetBytes(bytes);
        
        long rValue = BitConverter.ToInt64(bytes);
        long aValue = rValue == long.MinValue ? 0 : Math.Abs(rValue);
        
        return aValue % (max - min) + min;
    }
}

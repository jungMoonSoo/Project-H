using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

public static class Logger
{
    private static StringBuilder stringBuilder = new StringBuilder();



    public static void Debug(string log, params object[] args)
    {
        Log(log, LogType.Debug, args);
    }
    public static void Info(string log, params object[] args)
    {
        Log(log, LogType.Info, args);
    }
    public static void Warning(string log, params object[] args)
    {
        Log(log, LogType.Warning, args);
    }
    public static void Error(string log, params object[] args)
    {
        Log(log, LogType.Error, args);
    }



    private static void Log(string log, LogType type, params object[] args)
    {
        stringBuilder.Clear();
        stringBuilder.Append($"{GetLogInfo(type)} - ");
        stringBuilder.Append(log);
        if (args.Length > 0)
        {
            stringBuilder.Append(" [");
            stringBuilder.Append(string.Join(", ", args));
            stringBuilder.Append(']');
        }

        UnityEngine.Debug.Log(stringBuilder.ToString());
    }
    private static string GetLogInfo(LogType type)
    {
        DateTime time = DateTime.Now;
        StackFrame stackFrame = new StackTrace().GetFrame(3);
        MethodBase callingMethod = stackFrame.GetMethod();

        return $"{time.ToString("yyyy-MM-dd HH:mm:ss")}\t[{type.ToString()}]\t[{callingMethod.DeclaringType.Name}]::[{callingMethod.Name}]";
    }
}

public enum LogType
{
    Debug,
    Info,
    Warning,
    Error,
}
using System;

public static class AuxiliarDeEnum
{
    public static T StringParaEnum<T>(string s)
    {
        T C = (T)Enum.Parse(typeof(T), s);        

        return C;
    }

    public static T EnumParaEnum<T>(Enum t)
    {
        T C = (T)Enum.Parse(typeof(T), t.ToString());
        return C;
    }
}

using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Bound
{
    [SerializeField]
    private Vector2 _lt;
    [SerializeField]
    private Vector2 _rb;

    public Vector2 Center => new Vector2(_lt.x + (_rb.x - _lt.x) * 0.5f, _lt.y + (_rb.y - _lt.y) * 0.5f);
    public Vector2 Size => new Vector2(_rb.x - _lt.x, _rb.y - _lt.y);

    public Vector2 GetRandomPos()
    {
        float x = Random.Range(_lt.x, _rb.x);
        float y = Random.Range(_lt.y, _rb.y);
        return new Vector2(x, y);
    }
}
public static class Define
{
    
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }
            return _mainCam;
        }

    }

    private static Camera _mainCam;


    public static Vector2 MousePos => MainCam.ScreenToWorldPoint(Input.mousePosition);

    public static T GetRandomEnum<T>(int startPos = 0, int? length = null) where T : System.Enum
    {
        Array values = Enum.GetValues(typeof(T));
        if (length == null)
        {
            return (T)values.GetValue(Random.Range(startPos, values.Length));
        }
        else
            return (T)values.GetValue(Random.Range(startPos, length.Value));
    }
    public static T StringToEnum<T>(this string enumString) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), enumString);
    }

    public static T GetEnumValue<T>(this Enum e) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        return (T)(object)e;
    }

    public static Bound GetRandomBound(params Bound[] bounds)
    {

        float random = Random.Range(0f, 1f);
        float sizeAmount = 0f;

        float currentSizeRandom = 0f;

        for (int i = 0; i < bounds.Length; ++i)
        {
            sizeAmount += bounds[i].Size.magnitude;
        }
        for (int i = 0; i < bounds.Length; ++i)
        {
            if (random <= bounds[i].Size.magnitude / sizeAmount + currentSizeRandom)
            {
                return bounds[i];
            }
            else
            {
                currentSizeRandom += bounds[i].Size.magnitude / sizeAmount;
            }
        }

        int rand = Random.Range(0, bounds.Length);
        return bounds[rand];
    }

    [StructLayout(LayoutKind.Explicit)]
    struct EnumUnion32<T> where T : Enum
    {
        [FieldOffset(0)]
        public T Enum;

        [FieldOffset(0)]
        public int Int;
    }

    public static int EnumToInt<T>(T e) where T : Enum
    {
        var unionDefault = default(EnumUnion32<T>);
        unionDefault.Enum = e;
        return unionDefault.Int;
    }

    public static T IntToEnum<T>(int value) where T : Enum
    {
        var unionDefault = default(EnumUnion32<T>);
        unionDefault.Int = value;
        return unionDefault.Enum;
    }
}

namespace System.Runtime.CompilerServices
{
    public static class IsExternalInit
    {}
}

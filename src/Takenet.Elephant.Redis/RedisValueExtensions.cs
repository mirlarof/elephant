﻿using System;
using System.Globalization;
using StackExchange.Redis;

namespace Takenet.Elephant.Redis
{
    public static class RedisValueExtensions
    {
        public static object Cast(this RedisValue value, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (type == typeof(int)) return (int)value;            
            if (type == typeof(int?)) return (int?)value;            
            if (type == typeof(long)) return (long)value;            
            if (type == typeof(long?)) return (long?)value;
            if (type == typeof(bool)) return (bool)value;            
            if (type == typeof(bool?)) return (bool?)value;            
            if (type == typeof(byte[])) return (byte[])value;            
            if (type == typeof(string)) return (string)value;
            if (type == typeof(Guid)) return Guid.Parse(value);
            if (type == typeof(DateTimeOffset)) return DateTimeOffset.Parse(value, DateTimeFormatInfo.InvariantInfo);
            if (type == typeof(DateTime)) return DateTime.Parse(value, DateTimeFormatInfo.InvariantInfo);            
            throw new NotSupportedException($"The property type '{value.GetType()}'  is not supported");
        }

        public static T Cast<T>(this RedisValue value)
        {
            return (T)Cast(value, typeof(T));
        }

        public static RedisValue ToRedisValue(this object value)
        {
            if (value == null) return RedisValue.Null;            
            if (value is int) return (int)value;            
            if (value is long) return (long)value;            
            if (value is bool) return (bool)value;            
            if (value is byte[]) return (byte[])value;            
            if (value is string) return (string)value;
            if (value is Guid) return value.ToString();
            if (value is DateTimeOffset) return ((DateTimeOffset)value).ToString(DateTimeFormatInfo.InvariantInfo);
            if (value is DateTime) return ((DateTime)value).ToString(DateTimeFormatInfo.InvariantInfo);
            throw new NotSupportedException($"The property type '{value.GetType()}'  is not supported");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Translation.Common.Enumerations
{
    [Serializable]
    public abstract class Enumeration : IComparable
    {
        public int Value { get; }
        public string DisplayName { get; }
        public string Description { get; }

        protected Enumeration() { }
        protected Enumeration(int value, string displayName, string description = "")
        {
            Value = value;
            DisplayName = displayName;
            Description = description;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public static List<T> GetAll<T>() where T : Enumeration
        {
            return typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>()
                .ToList();
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            return otherValue == null 
                   ? false 
                   : GetType() == obj.GetType() && Value.Equals(otherValue.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool IsValueNotValid<T>(int value) where T : Enumeration
        {
            return !typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Any(x => x.GetValue(null).ToString() == FromValue<T>(value).ToString());
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            return Parse<T, int>(value, "value", item => item.Value == value);
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            return Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            return GetAll<T>().FirstOrDefault(predicate) 
                   ?? throw new ApplicationException($"'{value}' is not a valid {description} in {typeof(T)}");
        }

        public int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration)other).Value);
        }
    }
}
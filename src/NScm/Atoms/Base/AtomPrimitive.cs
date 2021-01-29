namespace NScm
{
    using System;
    using System.Collections.Generic;

    public abstract class AtomPrimitive<T> : Atom, IAtomPrimitive<T>, IEquatable<AtomPrimitive<T>>, IComparable<AtomPrimitive<T>>
    {
        public AtomPrimitive(T value)
        {
            this.NativeValue = value;
        }

        public T NativeValue { get; }

        public int CompareTo(AtomPrimitive<T> other)
        {
            return Comparer<T>.Default.Compare(NativeValue, other.NativeValue);
        }

        public bool Equals(AtomPrimitive<T> other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (GetType() != other.GetType()) return false;
            return NativeValue.Equals(other.NativeValue);
        }

        public override bool Equals(object obj)
        {
            if (obj is AtomPrimitive<T> a) return Equals(a);
            return false;
        }

        public override int GetHashCode()
        {
            return NativeValue.GetHashCode();
        }

        public override string ToString()
        {
            return NativeValue.ToString();
        }

        public static bool operator ==(AtomPrimitive<T> left, AtomPrimitive<T> right)
        {
            return EqualityComparer<AtomPrimitive<T>>.Default.Equals(left, right);
        }

        public static bool operator !=(AtomPrimitive<T> left, AtomPrimitive<T> right)
        {
            return !(left == right);
        }

        public static implicit operator T(AtomPrimitive<T> a) => a.NativeValue;
    }
}

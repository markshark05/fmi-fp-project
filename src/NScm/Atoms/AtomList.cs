namespace NScm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class AtomList : Atom,
        IEnumerable, IEnumerable<Atom>, IReadOnlyList<Atom>, IReadOnlyCollection<Atom>, IEquatable<AtomList>
    {
        private readonly List<Atom> list;

        public AtomList()
        {
            this.list = new List<Atom>();
        }

        public AtomList(IEnumerable<Atom> list)
        {
            this.list = new List<Atom>(list);
        }

        public Atom this[int index]
        {
            get => list[index];
        }

        public int Count => list.Count;

        public bool Contains(Atom item) => list.Contains(item);

        public IEnumerator<Atom> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

        public override string ToString()
        {
            return string.Format("({0})", string.Join(" ", list.Select(a => a.ToString())));
        }

        public override bool Equals(object obj)
        {
            if (obj is AtomList al) return Equals(al);
            return false;
        }

        public bool Equals(AtomList other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (!list.Any() && !other.list.Any()) return true;
            return ReferenceEquals(this, other);
        }

        public override int GetHashCode() => list.GetHashCode();

        public static bool operator ==(AtomList left, AtomList right) => EqualityComparer<AtomList>.Default.Equals(left, right);
        public static bool operator !=(AtomList left, AtomList right) => !(left == right);
    }
}

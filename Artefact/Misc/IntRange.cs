using System;

namespace Artefact.Misc
{
    [Serializable]
    struct IntRange
    {
        public int Min { get; }
        public int Max { get; }

        public IntRange(int min, int max)
        {
#if !DISABLE_RNG
            Min = min;
#else
            Min = max;
#endif
            Max = max;
        }

        public IntRange(int amount) : this(amount, amount) { }

        public override string ToString()
        {
            if (Min == Max) return $"{Min}";
            return $"{Min} - {Max}";
        }

        public static IntRange operator *(IntRange a, float b)
        {
            return new IntRange((int)Math.Ceiling(a.Min * b), (int)Math.Ceiling(a.Max * b));
        }
    }
}

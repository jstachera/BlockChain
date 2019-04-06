using System;
using System.Collections.Generic;
using System.Text;

namespace BlockChain.Common.Extensions
{
    public static class ByteArrayExtensions
    {
        public static bool Compare(this byte[] b1, byte[] b2)
        {
            return CompareSpan(b1, b2);
        }

        public static bool CompareSpan(this ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.SequenceEqual(a2);
        }

        public static bool StartsWith(this byte[] b1, byte[] b2)
        {
            return StartsWithSpan(b1, b2);

        }

        public static bool StartsWithSpan(this ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.Slice(0, a2.Length).CompareSpan(a2);
        }
    }
}

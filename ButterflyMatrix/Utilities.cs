using MatrixExpressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ButterflyMatrices
{
    public static class Utilities
    {
        public static StructuredProduct Product(this IEnumerable<IStructuredMatrix> matrices)
        {
            return new StructuredProduct(matrices.ToArray());
        }

        public static IEnumerable<Matrix> Create(this IEnumerable<IStructuredMatrix> matrices)
        {
            return matrices.Select(matrix => matrix.Create());
        }

        public static UInt32 NextPowerOf2(UInt32 v)
        {
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            v++;

            return v;
        }


        private static readonly uint[] b = { 0x2, 0xC, 0xF0, 0xFF00, 0xFFFF0000 };
        private static readonly uint[] S = { 1, 2, 4, 8, 16 };
        public static int Log2(int v)
        {
            uint r = 0; // result of log2(v) will go here
            for (int i = 4; i >= 0; i--) // unroll for speed...
            {
                if ((v & b[i]) > 0)
                {
                    v >>= (int)S[i];
                    r |= S[i];
                }
            }
            return (int)r;
        }
    }
}

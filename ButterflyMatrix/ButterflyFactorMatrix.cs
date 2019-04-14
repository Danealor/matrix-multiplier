using MatrixExpressions;
using System;
using System.Linq;

namespace ButterflyMatrices
{
    public class ButterflyFactorMatrix : IStructuredMatrix
    {
        public ButterflyFactor[] Factors { get; }

        public int n => k * Factors.Length;
        public int k => Factors[0].k;

        /// <summary>
        /// Create a butterfly factor matrix from butterfly factors.
        /// Note: Size of "factors" must be the same, a power of 2, and greater than or equal to 1.
        /// </summary>
        /// <param name="factors">All butterfly factors</param>
        public ButterflyFactorMatrix(ButterflyFactor[] factors)
        {
            if (factors.Length < 1)
                throw new ArgumentException("The size of factors must be greater than or equal to 1.");
            if (Utilities.NextPowerOf2((uint)factors.Length) != factors.Length)
                throw new ArgumentException("The size of factors must be a power of 2.");
            if (!factors.All(factor => factor.k == factors[0].k))
                throw new ArgumentException("All factor sizes must be the same.");

            Factors = factors;
        }

        public Matrix Create()
        {
            return Factors.Create().BlockDiagonal();
        }
    }
}

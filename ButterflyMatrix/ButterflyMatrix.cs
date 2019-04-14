using MatrixExpressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ButterflyMatrices
{
    public class ButterflyMatrix : IStructuredMatrix
    {
        public ButterflyFactorMatrix[] FactorMatrices { get; }

        public int n => FactorMatrices[0].n;

        /// <summary>
        /// Create a butterfly matrix from butterfly factor matrices.
        /// Note: Size of "factorMatrices" must be log2(n), and k must start at n and halve in each FactorMatrix.
        /// </summary>
        /// <param name="factorMatrices">All butterfly factor matrices</param>
        public ButterflyMatrix(ButterflyFactorMatrix[] factorMatrices)
        {
            if (factorMatrices.Length < 1)
                throw new ArgumentException("The size of factors must be greater than or equal to 1.");
            if (factorMatrices.Length != Utilities.Log2(factorMatrices[0].n))
                throw new ArgumentException("The size of factorMatrices must be log2(n).");
            for (int i = 0, k = factorMatrices[0].n; i < factorMatrices.Length; i++, k /= 2)
                if (factorMatrices[i].k != k)
                    throw new ArgumentException("k must start at n and halve in each FactorMatrix.");

            FactorMatrices = factorMatrices;
        }

        public static IEnumerable<ButterflyMatrix> Generate(int n, string id, IVariableStore store)
        {
            int logn = Utilities.Log2(n);

            for (int i = 0; ; i++)
            {
                ButterflyFactorMatrix[] factorMatrices = new ButterflyFactorMatrix[logn];
                for (int j = 0, k = n; j < factorMatrices.Length; j++, k /= 2)
                {
                    ButterflyFactor[] factors = new ButterflyFactor[n / k];
                    for (int r = 0; r < factors.Length; r++)
                    {
                        Expression[] elements = new Expression[2 * k];
                        for (int p = 0; p < elements.Length; p++)
                        {
                            elements[p] = new Variable($"{id}_{i}_{j}_{r}_{p}", store);
                        }
                        factors[r] = new ButterflyFactor(elements);
                    }
                    factorMatrices[j] = new ButterflyFactorMatrix(factors);
                }
                yield return new ButterflyMatrix(factorMatrices);
            }
        }

        public Matrix Create()
        {
            return FactorMatrices.Create().Product();
        }
    }
}

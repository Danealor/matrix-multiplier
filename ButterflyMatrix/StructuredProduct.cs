using System;
using System.Collections.Generic;
using System.Text;
using MatrixExpressions;

namespace ButterflyMatrices
{
    public class StructuredProduct : IStructuredMatrix
    {
        IStructuredMatrix[] Factors { get; }

        public StructuredProduct(IStructuredMatrix[] factors)
        {
            Factors = factors;
        }

        public Matrix Create()
        {
            return Factors.Create().Product();
        }
    }
}

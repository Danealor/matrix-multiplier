using MatrixExpressions;
using System;

namespace ButterflyMatrices
{
    public class ButterflyFactor : IStructuredMatrix
    {
        public Expression[] TopLeft { get; }
        public Expression[] TopRight { get; }
        public Expression[] BotLeft { get; }
        public Expression[] BotRight { get; }

        public int k => TopLeft.Length * 2;

        /// <summary>
        /// Create a butterfly factor with the diagonal elements.
        /// Note: Size of each argment must be the same, a power of 2, and greater than or equal to 1.
        /// </summary>
        /// <param name="topLeft">Top Left diagonal element</param>
        /// <param name="topRight">Top Right diagonal element</param>
        /// <param name="botLeft">Bot Left diagonal element</param>
        /// <param name="botRight">Bot Right diagonal element</param>
        public ButterflyFactor(Expression[] topLeft, Expression[] topRight, Expression[] botLeft, Expression[] botRight)
        {
            if (topLeft.Length != topRight.Length || topLeft.Length != botLeft.Length || topLeft.Length != botRight.Length)
                throw new ArgumentException("All diagonal elements must have the same size!");
            if (topLeft.Length < 1)
                throw new ArgumentException("The size of each diagonal element must be greater than or equal to 1.");
            if (Utilities.NextPowerOf2((uint)topLeft.Length) != topLeft.Length * 2)
                throw new ArgumentException("The size of each diagonal element must be a power of 2.");

            TopLeft = topLeft;
            TopRight = topRight;
            BotLeft = botLeft;
            BotRight = botRight;
        }

        /// <summary>
        /// Create a butterfly factor with all diagonal elements, in order of: Top Left, Top Right, Bot Left, Bot Right.
        /// Note: Size of "elements" must be a power of 2, and greater than or equal to 4.
        /// </summary>
        /// <param name="elements">All diagonal elements</param>
        public ButterflyFactor(Expression[] elements)
        {
            if (elements.Length < 4)
                throw new ArgumentException("The size of elements must be greater than or equal to 4.");
            if (Utilities.NextPowerOf2((uint)elements.Length) != elements.Length)
                throw new ArgumentException("The size of elements must be a power of 2.");

            TopLeft  = new Expression[elements.Length / 4];
            TopRight = new Expression[elements.Length / 4];
            BotLeft  = new Expression[elements.Length / 4];
            BotRight = new Expression[elements.Length / 4];

            Array.Copy(elements, 0 * elements.Length / 4, TopLeft , 0, elements.Length / 4);
            Array.Copy(elements, 1 * elements.Length / 4, TopRight, 0, elements.Length / 4);
            Array.Copy(elements, 2 * elements.Length / 4, BotLeft , 0, elements.Length / 4);
            Array.Copy(elements, 3 * elements.Length / 4, BotRight, 0, elements.Length / 4);
        }

        public Matrix Create()
        {
            Matrix M = new Matrix(k, k);

            int r = TopLeft.Length;
            for (int i = 0; i < TopLeft.Length; i++)
            {
                M[i, i] = TopLeft[i];
                M[i, r + i] = TopRight[i];
                M[r + i, i] = BotLeft[i];
                M[r + i, r + i] = BotRight[i];
            }

            return M;
        }
    }
}

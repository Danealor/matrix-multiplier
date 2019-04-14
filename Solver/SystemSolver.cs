using MatrixExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver
{
    public class SystemSolver
    {
        private Variable.BaseComparer baseComparer = new Variable.BaseComparer();

        public Equation[] FindUnsolvableDomain(IEnumerable<Equation> system)
        {
            LinkedList<Equation> curSystem = new LinkedList<Equation>(system);
            while (curSystem.Count > 0)
            {
                Variable pivot = curSystem.SelectMany(eq => eq.LHS.Terms.SelectMany(term => term.Variables)).ArgMax(variable => variable.Exponent);
                var nodes = curSystem.SelectNodes().Where(node => node.Value.LHS.Terms.Any(term => term.Variables.BinarySearchOrDefault(pivot, baseComparer).Exponent != 0)).ToArray();

                var pivotNode = nodes[0];
                Equation pivotEquation = pivotNode.Value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.Specifications
{
    internal class ParameterReplacerVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;

        private readonly Expression _newExpression;

        private ParameterReplacerVisitor(ParameterExpression oldParameter, Expression newExpression)
        {
            _oldParameter = oldParameter;
            _newExpression = newExpression;
        }

        internal static Expression Replace(Expression expression, ParameterExpression oldParameter, Expression newExpression)
        {
            return new ParameterReplacerVisitor(oldParameter, newExpression).Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return p == _oldParameter ? _newExpression : p;
        }
    }
}

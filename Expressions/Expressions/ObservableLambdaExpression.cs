using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NMF.Expressions
{
    internal class ObservableLambdaExpression<T> : NotifyExpression<Expression<T>>
    {
        public ObservableLambdaExpression(Expression<T> value)
            : base(value) { }
        
        public override bool IsParameterFree
        {
            get { return true; }
        }

        protected override Expression<T> GetValue()
        {
            return Value;
        }

        public override IEnumerable<INotifiable> Dependencies { get { return Enumerable.Empty<INotifiable>(); } }

        public override INotifyExpression<Expression<T>> ApplyParameters(IDictionary<string, object> parameters)
        {
            var visitor = new ApplyParametersVisitor(parameters);
            return new ObservableLambdaExpression<T>((Expression<T>)visitor.Visit(Value));
        }
    }
}

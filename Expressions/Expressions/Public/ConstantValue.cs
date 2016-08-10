﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NMF.Expressions
{
    public sealed class ConstantValue<T> : INotifyValue<T>
    {
        private T value;

        public ConstantValue(T value)
        {
            this.value = value;
        }

        public IEnumerable<INotifiable> Dependencies { get { return Enumerable.Empty<INotifiable>(); } }

        private readonly ShortList<INotifiable> successors = new ShortList<INotifiable>();

        public event EventHandler<ValueChangedEventArgs> ValueChanged { add { } remove { } }

        public IList<INotifiable> Successors { get { return successors; } }

        public ExecutionMetaData ExecutionMetaData { get; } = new ExecutionMetaData();

        public T Value
        {
            get { return value; }
        }

        public INotificationResult Notify(IList<INotificationResult> sources)
        {
            throw new InvalidOperationException("A constant cannot have a dependency and therefore cannot be notified of a dependency change.");
        }
    }
}

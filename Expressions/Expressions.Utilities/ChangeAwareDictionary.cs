﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NMF.Expressions
{
    /// <summary>
    /// Provides a notifiable associative memory
    /// </summary>
    /// <typeparam name="TKey">The type of the keys</typeparam>
    /// <typeparam name="TValue">The type of the values</typeparam>
    public class ChangeAwareDictionary<TKey, TValue>
    {
        private class Entry : INotifyReversableExpression<TValue>, INotifyPropertyChanged
        {
            private readonly IExecutionContext context = ExecutionEngine.Current.Context;
            private readonly SuccessorList successors = new SuccessorList();
            private TValue value;

            public Entry()
            {
                successors.CollectionChanged += (obj, e) =>
                {
                    if (successors.Count == 0)
                        context.RemoveChangeListener(this, this, "Value");
                    else if (e.Action == NotifyCollectionChangedAction.Add && successors.Count == 1)
                        context.AddChangeListener(this, this, "Value");
                };
            }

            public IEnumerable<INotifiable> Dependencies
            {
                get
                {
                    return Enumerable.Empty<INotifiable>();
                }
            }

            public ExecutionMetaData ExecutionMetaData { get; } = new ExecutionMetaData();

            public bool IsReversable
            {
                get
                {
                    return true;
                }
            }

            public IList<INotifiable> Successors
            {
                get
                {
                    return successors;
                }
            }

            public TValue Value
            {
                get
                {
                    return value;
                }

                set
                {
                    if (!EqualityComparer<TValue>.Default.Equals(value, this.value))
                    {
                        var oldValue = this.value;
                        this.value = value;
                        ValueChanged?.Invoke(this, new ValueChangedEventArgs(oldValue, value));
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
                    }
                }
            }

            bool INotifyExpression.CanBeConstant
            {
                get
                {
                    return false;
                }
            }

            bool INotifyExpression.IsConstant
            {
                get
                {
                    return false;
                }
            }

            bool INotifyExpression.IsParameterFree
            {
                get
                {
                    return true;
                }
            }

            TValue INotifyValue<TValue>.Value
            {
                get
                {
                    return Value;
                }
            }

            object INotifyExpression.ValueObject
            {
                get
                {
                    return Value;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public event EventHandler<ValueChangedEventArgs> ValueChanged;

            public void Dispose()
            {
                successors.Clear();
            }

            public INotificationResult Notify(IList<INotificationResult> sources)
            {
                return new ValueChangedNotificationResult<TValue>(this, value, value);
            }

            INotifyExpression<TValue> INotifyExpression<TValue>.ApplyParameters(IDictionary<string, object> parameters)
            {
                return this;
            }

            INotifyExpression<TValue> INotifyExpression<TValue>.Reduce()
            {
                return this;
            }
        }

        private Dictionary<TKey, Entry> inner;

        /// <summary>
        /// Creates a new change-aware dictionary
        /// </summary>
        public ChangeAwareDictionary() : this((IEqualityComparer<TKey>)null) { }

        /// <summary>
        /// Creates a new change-aware dictionary
        /// </summary>
        /// <param name="comparer">The comparer that should be used to compare dictionary entries</param>
        public ChangeAwareDictionary(IEqualityComparer<TKey> comparer)
        {
            inner = new Dictionary<TKey, Entry>(comparer);
        }

        /// <summary>
        /// Creates a new change-aware dictionary based on a template
        /// </summary>
        /// <param name="template">The template dictionary</param>
        public ChangeAwareDictionary(ChangeAwareDictionary<TKey, TValue> template)
        {
            if (template == null)
            {
                inner = new Dictionary<TKey, Entry>();
            }
            else
            {
                inner = new Dictionary<TKey, Entry>(template.inner, template.inner.Comparer);
            }
        }

        /// <summary>
        /// Gets or sets a value for the given key
        /// </summary>
        /// <param name="key">The key element</param>
        /// <returns>The current value for the given key</returns>
        public TValue this[TKey key]
        {
            [ObservableProxy(typeof(ChangeAwareDictionary<,>), "AsNotifiable")]
            get
            {
                return AsNotifiable(key).Value;
            }
            set
            {
                AsNotifiable(key).Value = value;
            }
        }

        /// <summary>
        /// Gets an object that tracks the current value for the given key
        /// </summary>
        /// <param name="key">The given key</param>
        /// <returns>An object that tracks the current value and notifies clients when this value changes</returns>
        public INotifyReversableValue<TValue> AsNotifiable(TKey key)
        {
            Entry entry;
            if (!inner.TryGetValue(key, out entry))
            {
                entry = new Entry();
                inner.Add(key, entry);
            }

            return entry;
        }

        /// <summary>
        /// Forgets the notifiable value for the given key
        /// </summary>
        /// <param name="key">The key object</param>
        /// <returns>True, if there was an element for the given key, otherwise false</returns>
        public bool Forget(TKey key)
        {
            return inner.Remove(key);
        }
    }
}

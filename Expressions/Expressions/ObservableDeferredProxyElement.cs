﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using NMF.Utilities;

namespace NMF.Expressions
{
    internal class ObservableDeferredProxyElement<T, T1> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T>>>(), argument1)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1>(ProxyGenerator, Argument1.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T>>>(), argument1, argument2)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T>>>(), argument1, argument2, argument3)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }
        public INotifyExpression<T7> Argument7 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6, argument7)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6, Argument7);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters), Argument7.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            Argument7.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                Argument7.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }
        public INotifyExpression<T7> Argument7 { get; private set; }
        public INotifyExpression<T8> Argument8 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6, Argument7, Argument8);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters), Argument7.ApplyParameters(parameters), Argument8.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            Argument7.Attach();
            Argument8.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                Argument7.Detach();
                Argument8.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }
        public INotifyExpression<T7> Argument7 { get; private set; }
        public INotifyExpression<T8> Argument8 { get; private set; }
        public INotifyExpression<T9> Argument9 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6, Argument7, Argument8, Argument9);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters), Argument7.ApplyParameters(parameters), Argument8.ApplyParameters(parameters), Argument9.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            Argument7.Attach();
            Argument8.Attach();
            Argument9.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                Argument7.Detach();
                Argument8.Detach();
                Argument9.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }
        public INotifyExpression<T7> Argument7 { get; private set; }
        public INotifyExpression<T8> Argument8 { get; private set; }
        public INotifyExpression<T9> Argument9 { get; private set; }
        public INotifyExpression<T10> Argument10 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9, argument10)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6, Argument7, Argument8, Argument9, Argument10);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters), Argument7.ApplyParameters(parameters), Argument8.ApplyParameters(parameters), Argument9.ApplyParameters(parameters), Argument10.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            Argument7.Attach();
            Argument8.Attach();
            Argument9.Attach();
            Argument10.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                Argument7.Detach();
                Argument8.Detach();
                Argument9.Detach();
                Argument10.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }
        public INotifyExpression<T7> Argument7 { get; private set; }
        public INotifyExpression<T8> Argument8 { get; private set; }
        public INotifyExpression<T9> Argument9 { get; private set; }
        public INotifyExpression<T10> Argument10 { get; private set; }
        public INotifyExpression<T11> Argument11 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9, argument10, argument11)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6, Argument7, Argument8, Argument9, Argument10, Argument11);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters), Argument7.ApplyParameters(parameters), Argument8.ApplyParameters(parameters), Argument9.ApplyParameters(parameters), Argument10.ApplyParameters(parameters), Argument11.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            Argument7.Attach();
            Argument8.Attach();
            Argument9.Attach();
            Argument10.Attach();
            Argument11.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                Argument7.Detach();
                Argument8.Detach();
                Argument9.Detach();
                Argument10.Detach();
                Argument11.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }
        public INotifyExpression<T7> Argument7 { get; private set; }
        public INotifyExpression<T8> Argument8 { get; private set; }
        public INotifyExpression<T9> Argument9 { get; private set; }
        public INotifyExpression<T10> Argument10 { get; private set; }
        public INotifyExpression<T11> Argument11 { get; private set; }
        public INotifyExpression<T12> Argument12 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11, INotifyExpression<T12> argument12)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9, argument10, argument11, argument12)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11, INotifyExpression<T12> argument12)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6, Argument7, Argument8, Argument9, Argument10, Argument11, Argument12);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters), Argument7.ApplyParameters(parameters), Argument8.ApplyParameters(parameters), Argument9.ApplyParameters(parameters), Argument10.ApplyParameters(parameters), Argument11.ApplyParameters(parameters), Argument12.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            Argument7.Attach();
            Argument8.Attach();
            Argument9.Attach();
            Argument10.Attach();
            Argument11.Attach();
            Argument12.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                Argument7.Detach();
                Argument8.Detach();
                Argument9.Detach();
                Argument10.Detach();
                Argument11.Detach();
                Argument12.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T13>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }
        public INotifyExpression<T7> Argument7 { get; private set; }
        public INotifyExpression<T8> Argument8 { get; private set; }
        public INotifyExpression<T9> Argument9 { get; private set; }
        public INotifyExpression<T10> Argument10 { get; private set; }
        public INotifyExpression<T11> Argument11 { get; private set; }
        public INotifyExpression<T12> Argument12 { get; private set; }
        public INotifyExpression<T13> Argument13 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11, INotifyExpression<T12> argument12, INotifyExpression<T13> argument13)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T13>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9, argument10, argument11, argument12, argument13)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T13>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11, INotifyExpression<T12> argument12, INotifyExpression<T13> argument13)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6, Argument7, Argument8, Argument9, Argument10, Argument11, Argument12, Argument13);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters), Argument7.ApplyParameters(parameters), Argument8.ApplyParameters(parameters), Argument9.ApplyParameters(parameters), Argument10.ApplyParameters(parameters), Argument11.ApplyParameters(parameters), Argument12.ApplyParameters(parameters), Argument13.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            Argument7.Attach();
            Argument8.Attach();
            Argument9.Attach();
            Argument10.Attach();
            Argument11.Attach();
            Argument12.Attach();
            Argument13.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                Argument7.Detach();
                Argument8.Detach();
                Argument9.Detach();
                Argument10.Detach();
                Argument11.Detach();
                Argument12.Detach();
                Argument13.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T13>, INotifyValue<T14>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }
        public INotifyExpression<T7> Argument7 { get; private set; }
        public INotifyExpression<T8> Argument8 { get; private set; }
        public INotifyExpression<T9> Argument9 { get; private set; }
        public INotifyExpression<T10> Argument10 { get; private set; }
        public INotifyExpression<T11> Argument11 { get; private set; }
        public INotifyExpression<T12> Argument12 { get; private set; }
        public INotifyExpression<T13> Argument13 { get; private set; }
        public INotifyExpression<T14> Argument14 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11, INotifyExpression<T12> argument12, INotifyExpression<T13> argument13, INotifyExpression<T14> argument14)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T13>, INotifyValue<T14>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9, argument10, argument11, argument12, argument13, argument14)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T13>, INotifyValue<T14>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11, INotifyExpression<T12> argument12, INotifyExpression<T13> argument13, INotifyExpression<T14> argument14)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6, Argument7, Argument8, Argument9, Argument10, Argument11, Argument12, Argument13, Argument14);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters), Argument7.ApplyParameters(parameters), Argument8.ApplyParameters(parameters), Argument9.ApplyParameters(parameters), Argument10.ApplyParameters(parameters), Argument11.ApplyParameters(parameters), Argument12.ApplyParameters(parameters), Argument13.ApplyParameters(parameters), Argument14.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            Argument7.Attach();
            Argument8.Attach();
            Argument9.Attach();
            Argument10.Attach();
            Argument11.Attach();
            Argument12.Attach();
            Argument13.Attach();
            Argument14.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                Argument7.Detach();
                Argument8.Detach();
                Argument9.Detach();
                Argument10.Detach();
                Argument11.Detach();
                Argument12.Detach();
                Argument13.Detach();
                Argument14.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : NotifyExpression<T>
    {
        private INotifyExpression<T> proxy;
        public Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T13>, INotifyValue<T14>, INotifyValue<T15>, INotifyValue<T>> ProxyGenerator { get; private set; }
        public INotifyExpression<T1> Argument1 { get; private set; }
        public INotifyExpression<T2> Argument2 { get; private set; }
        public INotifyExpression<T3> Argument3 { get; private set; }
        public INotifyExpression<T4> Argument4 { get; private set; }
        public INotifyExpression<T5> Argument5 { get; private set; }
        public INotifyExpression<T6> Argument6 { get; private set; }
        public INotifyExpression<T7> Argument7 { get; private set; }
        public INotifyExpression<T8> Argument8 { get; private set; }
        public INotifyExpression<T9> Argument9 { get; private set; }
        public INotifyExpression<T10> Argument10 { get; private set; }
        public INotifyExpression<T11> Argument11 { get; private set; }
        public INotifyExpression<T12> Argument12 { get; private set; }
        public INotifyExpression<T13> Argument13 { get; private set; }
        public INotifyExpression<T14> Argument14 { get; private set; }
        public INotifyExpression<T15> Argument15 { get; private set; }

        public ObservableDeferredProxyElement(MethodInfo proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11, INotifyExpression<T12> argument12, INotifyExpression<T13> argument13, INotifyExpression<T14> argument14, INotifyExpression<T15> argument15)
            : this(proxyGenerator.CreateDelegate<Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T13>, INotifyValue<T14>, INotifyValue<T15>, INotifyValue<T>>>(), argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9, argument10, argument11, argument12, argument13, argument14, argument15)
        { }

        private ObservableDeferredProxyElement(Func<INotifyValue<T1>, INotifyValue<T2>, INotifyValue<T3>, INotifyValue<T4>, INotifyValue<T5>, INotifyValue<T6>, INotifyValue<T7>, INotifyValue<T8>, INotifyValue<T9>, INotifyValue<T10>, INotifyValue<T11>, INotifyValue<T12>, INotifyValue<T13>, INotifyValue<T14>, INotifyValue<T15>, INotifyValue<T>> proxyGenerator, INotifyExpression<T1> argument1, INotifyExpression<T2> argument2, INotifyExpression<T3> argument3, INotifyExpression<T4> argument4, INotifyExpression<T5> argument5, INotifyExpression<T6> argument6, INotifyExpression<T7> argument7, INotifyExpression<T8> argument8, INotifyExpression<T9> argument9, INotifyExpression<T10> argument10, INotifyExpression<T11> argument11, INotifyExpression<T12> argument12, INotifyExpression<T13> argument13, INotifyExpression<T14> argument14, INotifyExpression<T15> argument15)
        {
            ProxyGenerator = proxyGenerator;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
            Argument1 = argument1;
        }

        private void EnsureProxy()
        {
            if (proxy == null)
            {
                var proxyVal = ProxyGenerator(Argument1, Argument2, Argument3, Argument4, Argument5, Argument6, Argument7, Argument8, Argument9, Argument10, Argument11, Argument12, Argument13, Argument14, Argument15);
                proxy = proxyVal as INotifyExpression<T>;
                if (proxy == null)
                {
                    proxy = new ObservableProxyExpression<T>(proxyVal);
                }     
                proxy.ValueChanged += Proxy_ValueChanged;
            }
        }

        private void Proxy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Refresh();
        }

        public override bool CanReduce
        {
            get
            {
                return proxy != null;
            }
        }

        public override INotifyExpression<T> Reduce()
        {
            Attach();
		    EnsureProxy();
            return proxy.Reduce();
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public override bool IsParameterFree
        {
            get
            {
			    EnsureProxy();
                return proxy.IsParameterFree;
            }
        }

		public override string ToString()
		{
			if (proxy != null)
			{
				return "proxy for " + proxy.ToString();
			}
			else
			{
				return "(deferred proxy)";
			}
		}

        public override INotifyExpression<T> ApplyParameters(IDictionary<string, object> parameters)
        {
            if (proxy != null)
            {
                return proxy.ApplyParameters(parameters);
            }
            else
            {
                return new ObservableDeferredProxyElement<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ProxyGenerator, Argument1.ApplyParameters(parameters), Argument2.ApplyParameters(parameters), Argument3.ApplyParameters(parameters), Argument4.ApplyParameters(parameters), Argument5.ApplyParameters(parameters), Argument6.ApplyParameters(parameters), Argument7.ApplyParameters(parameters), Argument8.ApplyParameters(parameters), Argument9.ApplyParameters(parameters), Argument10.ApplyParameters(parameters), Argument11.ApplyParameters(parameters), Argument12.ApplyParameters(parameters), Argument13.ApplyParameters(parameters), Argument14.ApplyParameters(parameters), Argument15.ApplyParameters(parameters));
            }
        }

        protected override void AttachCore()
        {
		    EnsureProxy();
            Argument1.Attach();
            Argument2.Attach();
            Argument3.Attach();
            Argument4.Attach();
            Argument5.Attach();
            Argument6.Attach();
            Argument7.Attach();
            Argument8.Attach();
            Argument9.Attach();
            Argument10.Attach();
            Argument11.Attach();
            Argument12.Attach();
            Argument13.Attach();
            Argument14.Attach();
            Argument15.Attach();
            proxy.Attach();
        }

        protected override void DetachCore()
        {
		    if (proxy != null)
			{
                Argument1.Detach();
                Argument2.Detach();
                Argument3.Detach();
                Argument4.Detach();
                Argument5.Detach();
                Argument6.Detach();
                Argument7.Detach();
                Argument8.Detach();
                Argument9.Detach();
                Argument10.Detach();
                Argument11.Detach();
                Argument12.Detach();
                Argument13.Detach();
                Argument14.Detach();
                Argument15.Detach();
                proxy.Detach();
			}
        }

        protected override T GetValue()
        {
            EnsureProxy();
            return proxy.Value;
        }
    }
    internal class ObservableDeferredElementTypes
	{
		public static readonly Type[] Types = { typeof(ObservableDeferredProxyElement<,>), typeof(ObservableDeferredProxyElement<,,>), typeof(ObservableDeferredProxyElement<,,,>), typeof(ObservableDeferredProxyElement<,,,,>), typeof(ObservableDeferredProxyElement<,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,,,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,,,,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,,,,,,,,,>), typeof(ObservableDeferredProxyElement<,,,,,,,,,,,,,,,>)};
	}
}
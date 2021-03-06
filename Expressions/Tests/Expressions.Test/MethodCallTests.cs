﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NMF.Expressions.Test
{
    [TestClass]
    public class MethodCallTests
    {
        [TestMethod]
        public void MethodCall_NoObservableTarget_NoUpdate()
        {
            var update = false;
            var dummy = new Dummy<int>() { Item = 23 };

            var test = new NotifyValue<string>(() => dummy.Item.ToString());

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 42;

            Assert.IsFalse(update);
        }

        [TestMethod]
        public void MethodCall_ObservableTarget_Update()
        {
            var update = false;
            var dummy = new ObservableDummy<int>() { Item = 23 };

            var test = new NotifyValue<string>(() => dummy.Item.ToString());

            test.ValueChanged += (o, e) =>
            {
                update = true;
                Assert.AreEqual("23", e.OldValue);
                Assert.AreEqual("42", e.NewValue);
            };

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 42;

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
        }

        [TestMethod]
        public void MethodCall_ObservableTarget_NoUpdateWhenDetached()
        {
            var update = false;
            var dummy = new ObservableDummy<int>() { Item = 23 };

            var test = new NotifyValue<string>(() => dummy.Item.ToString());

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            test.Detach();

            dummy.Item = 42;

            Assert.IsFalse(update);

            test.Attach();

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
            update = false;

            dummy.Item = 0;

            Assert.IsTrue(update);
        }

        [TestMethod]
        public void MethodCall_NoObservableArgument_NoUpdates()
        {
            var update = false;
            var dummy = new Dummy<string>() { Item = "23" };

            var test = new NotifyValue<int>(() => int.Parse(dummy.Item));

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual(23, test.Value);
            Assert.IsFalse(update);

            dummy.Item = "42";

            Assert.IsFalse(update);
        }

        [TestMethod]
        public void MethodCall_ObservableArgument_Update()
        {
            var update = false;
            var dummy = new ObservableDummy<string>() { Item = "23" };

            var test = new NotifyValue<int>(() => int.Parse(dummy.Item));

            test.ValueChanged += (o, e) =>
            {
                update = true;
                Assert.AreEqual(23, e.OldValue);
                Assert.AreEqual(42, e.NewValue);
            };

            Assert.AreEqual(23, test.Value);
            Assert.IsFalse(update);

            dummy.Item = "42";

            Assert.IsTrue(update);
            Assert.AreEqual(42, test.Value);
        }

        [TestMethod]
        public void MethodCall_ObservableArgument_NoUpdatesWhenDetached()
        {
            var update = false;
            var dummy = new ObservableDummy<string>() { Item = "23" };

            var test = new NotifyValue<int>(() => int.Parse(dummy.Item));

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual(23, test.Value);
            Assert.IsFalse(update);

            test.Detach();

            dummy.Item = "42";

            Assert.IsFalse(update);

            test.Attach();

            Assert.IsTrue(update);
            Assert.AreEqual(42, test.Value);
            update = false;

            dummy.Item = "0";

            Assert.IsTrue(update);
        }
    }
}

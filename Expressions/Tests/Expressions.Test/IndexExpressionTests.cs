﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NMF.Expressions.Test
{
    [TestClass]
    public class IndexExpressionTests
    {
        [TestMethod]
        public void Indexer_NoObservableSource_NoUpdates()
        {
            var update = false;
            var coll = new List<string>();
            coll.Add("23");

            var test = new NotifyValue<string>(() => coll[0]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            coll.Insert(0, "42");

            Assert.IsFalse(update);
        }

        [TestMethod]
        public void Indexer_ObservableSource_Updates()
        {
            var update = false;
            var coll = new ObservableCollection<string>();
            coll.Add("23");

            var test = new NotifyValue<string>(() => coll[0]);

            test.ValueChanged += (o, e) =>
            {
                update = true;
                Assert.AreEqual("23", e.OldValue);
                Assert.AreEqual("42", e.NewValue);
            };

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            coll.Insert(0, "42");

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
        }

        [TestMethod]
        public void Indexer_ObservableSource_NoUpdatesWhenDetached()
        {
            var update = false;
            var coll = new ObservableCollection<string>();
            coll.Add("23");

            var test = new NotifyValue<string>(() => coll[0]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            test.Detach();

            coll.Insert(0, "42");

            Assert.IsFalse(update);

            test.Attach();

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
            update = false;

            coll.Insert(0, "Foo");

            Assert.IsTrue(update);
        }

        [TestMethod]
        public void Indexer_NoObservableIndex_NoUpdates()
        {
            var update = false;
            var coll = new List<string>();
            coll.Add("23");
            coll.Add("42");
            var dummy = new Dummy<int>();

            var test = new NotifyValue<string>(() => coll[dummy.Item]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsFalse(update);
        }

        [TestMethod]
        public void Indexer_ObservableIndex_Updates()
        {
            var update = false;
            var coll = new List<string>();
            coll.Add("23");
            coll.Add("42");
            var dummy = new ObservableDummy<int>();

            var test = new NotifyValue<string>(() => coll[dummy.Item]);

            test.ValueChanged += (o, e) =>
            {
                update = true;
                Assert.AreEqual("23", e.OldValue);
                Assert.AreEqual("42", e.NewValue);
            };

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
        }

        [TestMethod]
        public void Indexer_ObservableIndex_NoUpdatesWhenDetached()
        {
            var update = false;
            var coll = new List<string>();
            coll.Add("23");
            coll.Add("42");
            var dummy = new ObservableDummy<int>();

            var test = new NotifyValue<string>(() => coll[dummy.Item]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            test.Detach();

            dummy.Item = 1;

            Assert.IsFalse(update);

            test.Attach();

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
            update = false;

            dummy.Item = 0;

            Assert.IsTrue(update);
        }

        [TestMethod]
        public void ArrayIndexer_NoObservableIndex_NoUpdates()
        {
            var update = false;
            var coll = new string[] { "23", "42" };
            var dummy = new Dummy<int>();

            var test = new NotifyValue<string>(() => coll[dummy.Item]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsFalse(update);
        }

        [TestMethod]
        public void ArrayIndexer_ObservableIndex_Updates()
        {
            var update = false;
            var coll = new string[] { "23", "42" };
            var dummy = new ObservableDummy<int>();

            var test = new NotifyValue<string>(() => coll[dummy.Item]);

            test.ValueChanged += (o, e) =>
            {
                update = true;
                Assert.AreEqual("23", e.OldValue);
                Assert.AreEqual("42", e.NewValue);
            };

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
        }

        [TestMethod]
        public void ArrayIndexer_ObservableIndex_NoUpdatesWhenDetached()
        {
            var update = false;
            var coll = new string[] { "23", "42" };
            var dummy = new ObservableDummy<int>();

            var test = new NotifyValue<string>(() => coll[dummy.Item]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            test.Detach();

            dummy.Item = 1;

            Assert.IsFalse(update);

            test.Attach();

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
            update = false;

            dummy.Item = 0;

            Assert.IsTrue(update);
        }

        [TestMethod]
        public void ArrayIndexer2d_NoObservableIndex_NoUpdates()
        {
            var update = false;
            var coll = new string[,] { { "23", "42" } };
            var dummy = new Dummy<int>();

            var test = new NotifyValue<string>(() => coll[0, dummy.Item]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsFalse(update);
        }

        [TestMethod]
        public void ArrayIndexer2d_ObservableIndex_Updates()
        {
            var update = false;
            var coll = new string[,] { { "23", "42" } };
            var dummy = new ObservableDummy<int>();

            var test = new NotifyValue<string>(() => coll[0, dummy.Item]);

            test.ValueChanged += (o, e) =>
            {
                update = true;
                Assert.AreEqual("23", e.OldValue);
                Assert.AreEqual("42", e.NewValue);
            };

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
        }

        [TestMethod]
        public void ArrayIndexer2d_ObservableIndex_NoUpdatesWhenDetached()
        {
            var update = false;
            var coll = new string[,] { { "23", "42" } };
            var dummy = new ObservableDummy<int>();

            var test = new NotifyValue<string>(() => coll[0, dummy.Item]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            test.Detach();

            dummy.Item = 1;

            Assert.IsFalse(update);

            test.Attach();

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
            update = false;

            dummy.Item = 0;

            Assert.IsTrue(update);
        }

        [TestMethod]
        public void ArrayLongIndexer_NoObservableIndex_NoUpdates()
        {
            var update = false;
            var coll = new string[] { "23", "42" };
            var dummy = new Dummy<long>();

            var test = new NotifyValue<string>(() => coll[dummy.Item]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsFalse(update);
        }

        [TestMethod]
        public void ArrayLongIndexer_ObservableIndex_Updates()
        {
            var update = false;
            var coll = new string[] { "23", "42" };
            var dummy = new ObservableDummy<long>();

            var test = new NotifyValue<string>(() => coll[dummy.Item]);

            test.ValueChanged += (o, e) =>
            {
                update = true;
                Assert.AreEqual("23", e.OldValue);
                Assert.AreEqual("42", e.NewValue);
            };

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
        }

        [TestMethod]
        public void ArrayLongIndexer2d_NoObservableIndex_NoUpdates()
        {
            var update = false;
            var coll = new string[,] { { "23", "42" } };
            var dummy = new Dummy<long>();

            var test = new NotifyValue<string>(() => coll[0, dummy.Item]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsFalse(update);
        }

        [TestMethod]
        public void ArrayLongIndexer2d_ObservableIndex_Updates()
        {
            var update = false;
            var coll = new string[,] { { "23", "42" } };
            var dummy = new ObservableDummy<long>();

            var test = new NotifyValue<string>(() => coll[0L, dummy.Item]);

            test.ValueChanged += (o, e) =>
            {
                update = true;
                Assert.AreEqual("23", e.OldValue);
                Assert.AreEqual("42", e.NewValue);
            };

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            dummy.Item = 1;

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
        }

        [TestMethod]
        public void ArrayLongIndexer2d_ObservableIndex_NoUpdatesWhenDetached()
        {
            var update = false;
            var coll = new string[,] { { "23", "42" } };
            var dummy = new ObservableDummy<long>();

            var test = new NotifyValue<string>(() => coll[0, dummy.Item]);

            test.ValueChanged += (o, e) => update = true;

            Assert.AreEqual("23", test.Value);
            Assert.IsFalse(update);

            test.Detach();

            dummy.Item = 1;

            Assert.IsFalse(update);

            test.Attach();

            Assert.IsTrue(update);
            Assert.AreEqual("42", test.Value);
            update = false;

            dummy.Item = 0;

            Assert.IsTrue(update);
        }
    }
}

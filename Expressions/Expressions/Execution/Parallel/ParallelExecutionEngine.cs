﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NMF.Expressions
{
    public abstract class ParallelExecutionEngine : ExecutionEngine
    {
        protected abstract void Schedule(List<INotifiable> nodes, Action<INotifiable> action);

        protected override void Execute(List<INotifiable> nodes)
        {
            Schedule(nodes, MarkNode);
            Schedule(nodes, NotifyNode);
        }

        private void NotifyNode(INotifiable node)
        {
            int currentValue = 1;
            bool evaluating = true;

            do
            {
                var metaData = node.ExecutionMetaData;
                if (metaData.RemainingVisits != currentValue)
                {
                    int remaining = Interlocked.Add(ref metaData.RemainingVisits, -currentValue);
                    if (remaining > 0)
                        return;
                }
                else
                {
                    metaData.RemainingVisits = 0;
                }

                currentValue = metaData.TotalVisits;
                metaData.TotalVisits = 0;

                foreach (var dep in node.Dependencies)
                {
                    if (dep.ExecutionMetaData.Result != null)
                    {
                        metaData.Sources.Add(dep.ExecutionMetaData.Result);
                        dep.ExecutionMetaData.Result = null;
                    }
                }

                if (evaluating || metaData.Sources.Count > 0)
                {
                    var result = node.Notify(metaData.Sources);
                    evaluating = result.Changed;
                    if (evaluating && node.Successors.HasSuccessors)
                        metaData.Result = result;
                }

                metaData.Sources.Clear();
            }
            while (node.Successors.HasSuccessors && (node = node.Successors[0]) != null);
        }

        private void MarkNode(INotifiable node)
        {
            do
            {
                var metaData = node.ExecutionMetaData;
                Interlocked.Increment(ref metaData.RemainingVisits);
                Interlocked.Increment(ref metaData.TotalVisits);
            }
            while (node.Successors.HasSuccessors && (node = node.Successors[0]) != null);
        }
    }
}

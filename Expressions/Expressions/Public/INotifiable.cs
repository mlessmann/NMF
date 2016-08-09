﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NMF.Expressions
{
    /// <summary>
    /// Represents a node in a graph which can notify its successors
    /// and gets notified by its dependencies.
    /// </summary>
    public interface INotifiable
    {
        /// <summary>
        /// The nodes that will get notified by this node.
        /// </summary>
        IList<INotifiable> Successors { get; }

        /// <summary>
        /// Nodes that notify this node.
        /// </summary>
        IEnumerable<INotifiable> Dependencies { get; }

        /// <summary>
        /// Gets called when one of the dependencies signals a notification.
        /// </summary>
        /// <param name="source">Contains information about what triggered this notification.</param>
        /// <returns>An object describing the changes that happened in this notification.</returns>
        INotificationResult Notify(IList<INotificationResult> sources);

        int TotalVisits { get; set; }

        int RemainingVisits { get; set; }
    }
}

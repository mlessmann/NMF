﻿using NMF.Transformations.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NMF.Transformations
{
    /// <summary>
    /// Defines a simple transformation rule of a transformation that has multiple input arguments and no output
    /// </summary>
    /// <remarks>Simple means that the transformation rule does not require a custom computation class</remarks>
    public abstract class InPlaceTransformationRule : InPlaceTransformationRuleBase
    {
        private bool needDependencies;

        /// <summary>
        /// Creates a new transformation rule
        /// </summary>
        public InPlaceTransformationRule()
        {
            var createOutput = this.GetType().GetRuntimeMethod("Init", new[] { typeof(object[]), typeof(ITransformationContext) });
            needDependencies = createOutput.DeclaringType != typeof(InPlaceTransformationRule);
        }

        /// <summary>
        /// Gets a value indicating whether the output for all dependencies must have been created before this rule creates the output
        /// </summary>
        public override bool NeedDependenciesForOutputCreation
        {
            get { return needDependencies; }
        }

        private class SimpleComputation : InPlaceComputation
        {
            public SimpleComputation(InPlaceTransformationRule transformationRule, object[] input, IComputationContext context)
                : base(transformationRule, context, input) { }

            public override void Transform()
            {
                if (Input != null)
                {
                    (TransformationRule as InPlaceTransformationRule).Transform(Input, TransformationContext);
                    OnComputed(EventArgs.Empty);
                }
            }

            public override object CreateOutput(IEnumerable context)
            {
                if (Input != null)
                {
                    (TransformationRule as InPlaceTransformationRule).Init(Input, TransformationContext);
                }
                return null;
            }
        }

        /// <summary>
        /// Creates a new Computation instance for this transformation rule or the given input 
        /// </summary>
        /// <param name="input">The input arguments for this computation</param>
        /// <param name="context">The context for this computation</param>
        /// <returns>A computation object</returns>
        public override Computation CreateComputation(object[] input, IComputationContext context)
        {
            if (input == null) return null;
            if (input.Length != InputType.Length) throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, ErrorStrings.TransformationRuleWrongNumberOfArguments, this.GetType().Name));
            return new SimpleComputation(this, input, context);
        }


        /// <summary>
        /// Initializes the transformation. This is done before any other transformation rule hits Transform
        /// </summary>
        /// <param name="input">The input for this transformation rule</param>
        /// <param name="context">The current transformation context</param>
        public abstract void Init(object[] input, ITransformationContext context);

        /// <summary>
        /// Initializes the transformation output
        /// </summary>
        /// <param name="input">The input of the transformation rule</param>
        /// <param name="context">The context (and trace!) object</param>
        /// <remarks>At this point, all the transformation outputs are created (also the delayed ones), thus, the trace is fully reliable</remarks>
        public abstract void Transform(object[] input, ITransformationContext context);
    }
}

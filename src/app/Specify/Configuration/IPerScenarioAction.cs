﻿using System;

namespace Specify.Configuration
{
    /// <summary>
    /// Represents an action to be performed before and after each scenario.
    /// </summary>
    public interface IPerScenarioAction
    {
        /// <summary>
        /// Action to be performed before each scenario.
        /// </summary>
        void Before<TSut>(IScenario<TSut> scenario) where TSut : class;

        /// <summary>
        /// Action to be performed after each scenario.
        /// </summary>
        void After();

        /// <summary>
        /// Whether this action should be performed for a particular type.
        /// </summary>
        bool ShouldExecute(Type type);

        /// <summary>
        /// The order the action will run in.
        /// </summary>
        int Order { get; set; }
    }
}

﻿using System;

using Autofac;

namespace Yarm.Functions.ServiceLocators
{
    /// <summary>
    /// This represents the handler entity for dependency registration to service locator.
    /// </summary>
    public class RegistrationHandler
    {
        /// <summary>
        /// Gets or sets the action to register type.
        /// </summary>
        public Action<ContainerBuilder> RegisterTypeAsInstancePerDependency { get; set; }
    }
}

namespace Devkit.Auth.ServiceBus;

using Devkit.ServiceBus.Interfaces;
using MassTransit;

public class AuthRegistry : IBusRegistry
{
    /// <summary>
    /// Configure message consumers.
    /// </summary>
    /// <param name="configurator">The configurator.</param>
    public void RegisterConsumers(IBusRegistrationConfigurator configurator)
    {
    }
}
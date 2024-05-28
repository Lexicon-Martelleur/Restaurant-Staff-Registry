using StaffRegistry.constant;
using StaffRegistry.model;

namespace StaffRegistry.events;

internal class StaffRegistryEventArgs<EventData>(
    RepositoryResult status,
    string msg,
    EventData data
    ) : EventArgs
{
    internal RepositoryResult Status { get; } = status;

    internal String Msg { get; } = msg;

    internal EventData Data { get; } = data;
}

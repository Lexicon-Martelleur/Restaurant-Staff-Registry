using StaffRegistry.constant;
using StaffRegistry.model;

namespace StaffRegistry.events;

public class StaffRegistryEventArgs<EventData>(
    RepositoryResult status,
    string msg,
    EventData data
    ) : EventArgs
{
    public RepositoryResult Status { get; } = status;

    public String Msg { get; } = msg;

    public EventData Data { get; } = data;
}

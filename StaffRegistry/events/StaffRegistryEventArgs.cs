using StaffRegistry.constant;
using StaffRegistry.model;

namespace StaffRegistry.events;

public class StaffRegistryEventArgs(
    RepositoryResult status,
    string msg,
    StaffVO data
    ) : EventArgs
{
    public RepositoryResult Status { get; } = status;

    public String Msg { get; } = msg;

    public StaffVO Data { get; } = data;
}

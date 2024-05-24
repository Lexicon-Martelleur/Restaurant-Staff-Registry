using Retaurant_Staff_Registry.constant;
using Retaurant_Staff_Registry.model;

namespace Retaurant_Staff_Registry.events;

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

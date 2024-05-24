namespace Retaurant_Staff_Registry.utility;

internal static class IDUtility
{
    private static readonly HashSet<int> _IDs = [];

    internal static int GetInMemoryUniqueID()
    {
        Random random = new();
        int id;
        do
        {
            id = random.Next(1, int.MaxValue);
        } while (!_IDs.Add(id));
        return id;
    }
}

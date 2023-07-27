public static class NeighborPathNodeExtentions
{
    public static bool HasTrap(this INeighborPathNode node)
    {
        return node.trap != null;
    }

    public static bool HasTrap(this INeighborPathNode node, out TrapComponent trap)
    {
        trap = node.trap;
        return trap != null;
    }
}

/// <summary>
/// Represents a utility class which makes working with layers in Unity easier.
/// </summary>
public class LayerMaskUtility
{
    /// <summary>
    /// Returns an <see cref="int"/> with just one bit set at <see cref="bitPos"/>.
    /// </summary>
    /// <param name="bitPos">Position at which the bit should be 1 in the bitmask.</param>
    /// <returns>The calculated bitmask.</returns>
    public static int BitPositionToMask(int bitPos)
    {
        return (1 << bitPos);
    }
}

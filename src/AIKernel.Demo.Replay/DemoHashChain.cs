using System.Security.Cryptography;
using System.Text;

namespace AIKernel.Demo.Replay;

public static class DemoHashChain
{
    public const string GenesisHash = "GENESIS";

    public static string ComputeEntryHash(
        string entryPayload)
    {
        ArgumentNullException.ThrowIfNull(entryPayload);

        return Sha256Hex(entryPayload);
    }

    public static string ComputeNext(
        string previousHash,
        string entryHash)
    {
        ArgumentNullException.ThrowIfNull(previousHash);
        ArgumentNullException.ThrowIfNull(entryHash);

        return Sha256Hex(previousHash + entryHash);
    }

    public static string CanonicalizePayload(
        string stepName,
        string deltaSummary,
        string timestamp)
    {
        ArgumentNullException.ThrowIfNull(stepName);
        ArgumentNullException.ThrowIfNull(deltaSummary);
        ArgumentNullException.ThrowIfNull(timestamp);

        return string.Join(
            '\n',
            $"delta_summary={Normalize(deltaSummary)}",
            $"step_name={Normalize(stepName)}",
            $"timestamp={Normalize(timestamp)}");
    }

    private static string Sha256Hex(
        string value)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value))).ToLowerInvariant();

    private static string Normalize(
        string value)
        => value.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n').Trim();
}

using System.Text;

namespace Blockchain;

public static class ByteArrayExtensions
{
    public static string HashByteArrayToString(this byte[] hashByteArray, int powDifficulty)
    {
        var builder = new StringBuilder();
        foreach (var hashByte in hashByteArray) builder.Append(hashByte.ToString($"x{powDifficulty}"));
        return builder.ToString();
    }
}
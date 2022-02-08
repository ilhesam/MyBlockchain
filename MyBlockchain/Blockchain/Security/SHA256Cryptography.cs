using System.Security.Cryptography;
using System.Text;

namespace Blockchain;

public class SHA256Cryptography : IBlockchainCryptography
{
    public async Task<byte[]> HashAsync(string data)
    {
        var bytes = Encoding.ASCII.GetBytes(data);
        return await HashAsync(bytes);
    }

    public async Task<byte[]> HashAsync(byte[] data)
    {
        var sha256 = SHA256.Create();
        await using var stream = new MemoryStream(data);
        return await sha256.ComputeHashAsync(stream);
    }
}
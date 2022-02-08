namespace Blockchain;

public interface IBlockchainCryptography
{
    Task<byte[]> HashAsync(string data);
    Task<byte[]> HashAsync(byte[] data);
}
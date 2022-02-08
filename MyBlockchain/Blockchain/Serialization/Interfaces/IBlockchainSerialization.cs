namespace Blockchain;

public interface IBlockchainSerialization
{
    string Serialize(object data);
}
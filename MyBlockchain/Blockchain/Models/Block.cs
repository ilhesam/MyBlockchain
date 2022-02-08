using Newtonsoft.Json;

namespace Blockchain;

public class Block<TData>
{
    public Block(TData data, string previousHash, long nonce)
    {
        Data = data;
        PreviousHash = previousHash;
        Nonce = nonce;
    }

    public void SetHash(string hash)
    {
        if (Hash != null) throw new Exception("This block has already been hashed");
        Hash = hash ?? throw new ArgumentNullException(nameof(hash));
    }

    public static Block<TData> GenesisBlock()
    {
        var genesisBlock = new Block<TData>(default, default, default);
        genesisBlock.SetHash("0000");
        return genesisBlock;
    }

    public long Nonce { get; }
    public DateTime TimeStamp { get; } = DateTime.UtcNow;
    public string PreviousHash { get; }
    [JsonIgnore] public string Hash { get; private set; }
    public TData Data { get; }
}
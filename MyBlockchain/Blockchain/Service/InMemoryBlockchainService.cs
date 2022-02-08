using BasicBlockchain.Blockchain;

namespace Blockchain;

public class InMemoryBlockchainService<TData> : BlockchainService<TData>
{
    protected IList<Block<TData>> InMemoryChain = new List<Block<TData>>();

    public InMemoryBlockchainService(IBlockchainCryptography cryptography, IBlockchainSerialization serializer, MineOptions mineOptions) : base(cryptography, serializer, mineOptions)
    {
        // Add Genesis Block
        var genesisBlock = Block<TData>.GenesisBlock();
        StoreBlockAsync(genesisBlock).Wait();
    }

    public override async Task<Block<TData>> GetLatestBlockAsync() => await Task.Run(() => InMemoryChain.Last());
    public override async Task StoreBlockAsync(Block<TData> block) => await Task.Run(() => InMemoryChain.Add(block));
}
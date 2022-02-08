namespace Blockchain;

public interface IBlockchainService<TData>
{
    Task<Block<TData>> MineBlockAsync(TData data);
    Task StoreBlockAsync(Block<TData> block);
    Task<Block<TData>> GetLatestBlockAsync();
}
using BasicBlockchain.Blockchain;

namespace Blockchain;

public abstract class BlockchainService<TData> : IBlockchainService<TData>
{
    private long _nonce;

    protected readonly IBlockchainCryptography Cryptography;
    protected readonly IBlockchainSerialization Serializer;
    protected readonly MineOptions MineOptions;

    protected BlockchainService(IBlockchainCryptography cryptography, IBlockchainSerialization serializer, MineOptions mineOptions)
    {
        Cryptography = cryptography;
        Serializer = serializer;
        MineOptions = mineOptions;
    }

    public async Task<Block<TData>> MineBlockAsync(TData data)
    {
        var previousBlock = await GetLatestBlockAsync();
        _nonce = 0;

        var block = await MineBlockAsync(data, previousBlock, MineOptions.ProofOfWorkDifficulty);
        await StoreBlockAsync(block);
        return block;
    }

    private async Task<Block<TData>> MineBlockAsync(TData data, Block<TData> previousBlock, int powDifficulty)
    {
        // Create block
        var block = new Block<TData>(data, previousBlock.Hash, _nonce);

        // Serialize block
        var serialized = Serializer.Serialize(block);

        // Hash block
        var hashByteArray = await Cryptography.HashAsync(serialized);
        var hash = hashByteArray.HashByteArrayToString(powDifficulty);
        block.SetHash(hash);

        // Proof of Work (PoW)
        var hashValidationTemplate = new string('0', powDifficulty);
        while (block.Hash[..powDifficulty] != hashValidationTemplate)
        {
            _nonce += 1;
            block = await MineBlockAsync(data, previousBlock, powDifficulty);
        }

        return block;
    }

    public abstract Task<Block<TData>> GetLatestBlockAsync();
    public abstract Task StoreBlockAsync(Block<TData> block);
}
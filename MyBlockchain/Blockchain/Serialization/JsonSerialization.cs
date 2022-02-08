using Newtonsoft.Json;

namespace Blockchain;

public class JsonSerialization : IBlockchainSerialization
{
    public string Serialize(object data) => JsonConvert.SerializeObject(data);
}
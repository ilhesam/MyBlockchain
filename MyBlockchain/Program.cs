using System.Diagnostics;
using Blockchain;
using Newtonsoft.Json;

Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Welcome to Blockchain World!\n");

Console.ForegroundColor = ConsoleColor.White; Console.Write("Please enter number of blocks you want to mine: ");
int blocksCount = int.Parse(Console.ReadLine());

// Other cryptography algorithms can be used instead of SHA256 (like SHA512, etc.)
// Just create a new class and implement IBlockchainCryptography
IBlockchainCryptography cryptography = new SHA256Cryptography();

// Other serialization types can be used instead of JSON (like string format, XML, etc.)
// Just create a new class and implement IBlockchainSerialization
IBlockchainSerialization serializer = new JsonSerialization();

// For testing, Data is set to int
// Data can be any other type (like string, object, etc.)
IBlockchainService<int> service = new InMemoryBlockchainService<int>(cryptography, serializer, new MineOptions());

Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\nBlockchain services is running...\n");

var stopwatch = new Stopwatch();
stopwatch.Start();
for (int i = 0; i < blocksCount; i++) await Mine(i + 1);
stopwatch.Stop();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("All blocks successfully mined :)!");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Number of mined blocks: {blocksCount}");
Console.WriteLine($"Mine time: {stopwatch.ElapsedMilliseconds}ms");

async Task Mine(int i)
{
    var stopwatch = new Stopwatch();

    Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"Start mining block {i}...\n");
    var randomNumber = new Random().Next(); // For testing

    stopwatch.Start();
    var block = await service.MineBlockAsync(randomNumber);
    stopwatch.Stop();

    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Block {i} is mined! Time: {stopwatch.ElapsedMilliseconds}ms");
    Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($"Hash: {block.Hash}");
    Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine($"{JsonConvert.SerializeObject(block, Formatting.Indented)}\n");
}
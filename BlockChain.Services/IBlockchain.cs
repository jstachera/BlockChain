using System.Collections.Generic;
using System.Security.Cryptography;

namespace BlockChain.Services
{
    public interface IBlockchain<T> : IEnumerable<Block<T>>
        where T : class
    {
        Block<T> Add(T data);
        bool IsValid();
        Block<T> Last();
        Block<T> this[int index] { get; }
        int Count { get; }
    }
}
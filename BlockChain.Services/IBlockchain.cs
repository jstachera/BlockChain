using System.Collections.Generic;

namespace BlockChain.Services
{
    public interface IBlockchain<T>: IEnumerable<Block<T>>
        where T : class 
    {
        Block<T> this[int index] { get; }
        int Count { get; }
        int Difficulty { get; set; }
        Block<T> Add(T data);
        bool IsValid();
        Block<T> Last();
    }
}
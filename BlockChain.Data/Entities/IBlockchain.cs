using System.Collections.Generic;

namespace BlockChain.Data.Entities
{
    interface IBlockchain<T>: IEnumerable<Block<T>> where T : class 
    {
        Block<T> Add(Block<T> block);
        bool IsValid();
        Block<T> Last();
        Block<T> this[int index] { get; }
    }
}
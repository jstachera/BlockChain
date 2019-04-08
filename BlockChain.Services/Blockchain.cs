using BlockChain.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain.Services
{
    public class Blockchain<T> : IBlockchain<T> where T : class

    {
        #region Properties
        private IList<Block<T>> Chain { set; get; }
        public int Count { get => this.Chain.Count; }

        public int Difficulty { set; get; } = 2;

        public Block<T> this[int index] => Chain[index];

        #endregion

        #region Ctor

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }

        #endregion

        #region Methods
        public IEnumerator<Block<T>> GetEnumerator() => Chain.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Chain.GetEnumerator();

        private void InitializeChain()
        {
            Chain = new List<Block<T>>();
        }

        private void AddGenesisBlock() => Chain.Add(CreateGenesisBlock());
        virtual protected Block<T> CreateGenesisBlock() => new Block<T>(null, default);
        public Block<T> Last() => Chain.Last();

        public Block<T> Add(T data)
        {
            Block<T> lastBlock = Last();
            Block<T> newBlock = new Block<T>(lastBlock.Hash, data)
            {
                Index = lastBlock.Index + 1
            };
            newBlock.Mine(Difficulty);
            Chain.Add(newBlock);
            return newBlock;
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block<T> cur = Chain[i];
                Block<T> prev = Chain[i - 1];

                if (!cur.IsHashValid())
                {
                    return false;
                }

                if (!cur.PreviousHash.Compare(prev.Hash))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}

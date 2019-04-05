using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain.Data.Entities
{
    class Blockchain<T> : IBlockchain<T> where T: class
    {
        #region Properties
        private IList<Block<T>> Chain { set; get; }
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
        virtual protected Block<T> CreateGenesisBlock() => new Block<T>(DateTime.Now, null, default);
        public Block<T> Last() => Chain.Last();
        public Block<T> Add(Block<T> block)
        {
            Block<T> last = Last();
            block.Index = last.Index + 1;
            block.PreviousHash = last.Hash;
            block.Hash = block.CalculateHash();
            Chain.Add(block);
            return block;
        }
        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block<T> cur = Chain[i];
                Block<T> prev = Chain[i - 1];

                if (cur.Hash != cur.CalculateHash())
                {
                    return false;
                }

                if (cur.PreviousHash != prev.Hash)
                {
                    return false;
                }
            }

            return true;
        }



        #endregion
    }
}

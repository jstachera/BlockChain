using BlockChain.Common.Extensions;

using System;
using System.Security.Cryptography;

namespace BlockChain.Data.Entities
{
    class Block<T> where T: class
    {
        #region Properties

        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public byte[] PreviousHash { get; set; }
        public int PreviousHashLength => (PreviousHash != null) ? PreviousHash.Length: 0;
        public byte[] Hash { get; set; }
        public int HashLength => (Hash != null) ? Hash.Length : 0;
        public T Data { get; set; }
        public bool HasData => Data != null;
        public HashAlgorithm HashFunc { get; set; }

        #endregion

        #region Ctor

        public Block(HashAlgorithm hashAlgorithm)
        {
            HashFunc = hashAlgorithm;
        }

        #endregion

        #region Methods

        public byte[] CalculateHash() => HashFunc.ComputeHash(GetDataToHash());

        public Block(DateTime timeStamp, byte[] previousHash, T data)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Data = data;
            Hash = CalculateHash();
        }

        private byte[] GetDataToHash()
        {
            byte[] timeStampBytes = TimeStamp.ToByteArray();
            byte[] dataBytes = (HasData) ? Data.ToByteArray(): null;
            byte[] destBytes = new byte[timeStampBytes.Length + PreviousHashLength + (HasData ? dataBytes.Length: 0)];

            // copy time stamp
            Array.Copy(timeStampBytes, destBytes, timeStampBytes.Length);

            // copy previous hash
            if (PreviousHashLength > 0)
            {
                Array.Copy(PreviousHash, 0, destBytes, timeStampBytes.Length, PreviousHashLength);
            }

            // copy data
            if (HasData)
            {
                Array.Copy(dataBytes, 0, destBytes, timeStampBytes.Length + PreviousHashLength, dataBytes.Length);
            }

            return destBytes;
        }

        #endregion
    }
}

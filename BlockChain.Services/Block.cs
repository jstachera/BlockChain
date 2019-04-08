using BlockChain.Common.Extensions;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlockChain.Services
{
    public class Block<T>
        where T: class
    {
        #region Properties

        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public byte[] PreviousHash { get; set; }
        public int PreviousHashLength => PreviousHash?.Length ?? 0;
        public byte[] Hash { get; set; }
        public int HashLength => Hash?.Length ?? 0;
        public T Data { get; set; }
        public bool HasData => Data != null;
        public int Nonce { get; set; } = 0;
        //TODO: refactor
        public SHA256 HashFunc { get; }

        #endregion

        #region Methods

        public byte[] CalculateHash() => HashFunc.ComputeHash(GetDataToHash(Nonce));

        public byte[] CalculateHash(int nonce) => HashFunc.ComputeHash(GetDataToHash(nonce));

        public Block(byte[] previousHash, T data)
        {
            HashFunc = SHA256.Create();
            Index = 0;
            TimeStamp = DateTime.Now;
            PreviousHash = previousHash;
            Data = data;
            Hash = CalculateHash();
        }

        public bool IsHashValid()
        {
            byte[] computedHash = CalculateHash();
            return Hash.Compare(computedHash);
        }

        public void Mine(int difficulty)
        {
            byte[] pattern = new byte[difficulty];
            Array.Fill<byte>(pattern, 0);
            while (Hash == null || !Hash.StartsWith(pattern))
            {
                Nonce++;
                Hash = CalculateHash();
            }
            //Parallel.For(0, int.MaxValue,
            //    (i, state) =>
            //    {
            //        byte[] h = this.CalculateHash(i);
            //        if (h.StartsWith(pattern))
            //        {
            //            Nonce = i;
            //            Hash = h;
            //            state.Break();
            //        }
            //    }
            //    );
        }

        private byte[] GetDataToHash(long nonce)
        {
            byte[] dataToHash = null;

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] timeStampBytes = TimeStamp.ToByteArray();
                byte[] dataBytes = (HasData) ? Data.ToByteArray() : null;
                byte[] nonceBytes = BitConverter.GetBytes(nonce);
           
                ms.Write(timeStampBytes, 0, timeStampBytes.Length);
                if (PreviousHashLength > 0)
                {
                    ms.Write(PreviousHash, 0, PreviousHashLength);
                }

                if (HasData)
                {
                    ms.Write(dataBytes, 0, dataBytes.Length);
                }

                ms.Write(nonceBytes, 0, nonceBytes.Length);
                ms.Flush();

                dataToHash = ms.ToArray();
            }
          
            return dataToHash;
        }

        #endregion
    }
}

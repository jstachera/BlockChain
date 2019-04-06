using System;
using System.Security.Cryptography;
using Xunit;

namespace BlockChain.Services.Test
{
    public class BlockChainTest
    {
        [Fact]
        public void Ctor_GenesisBlock_Created()
        {
            Blockchain<string> bc = new Blockchain<string>();
            Assert.True(bc.Count == 1
                && bc[0].PreviousHash == null
                && bc[0].Data == null);
        }

        [Fact]
        public void Add_Block_CheckData_True()
        {
            const string message = "Ala ma kota";
            Blockchain<string> bc = new Blockchain<string>
            {
                message
            };
            Assert.True(bc[1].Data == message);
        }

        [Fact]
        public void IsValid_True()
        {
            const string message1 = "ala ma kota";
            const string message2 = "kot ma kota";
            Blockchain<string> bc = new Blockchain<string>()
            {
                message1,
                message2
            };
            Assert.True(bc.IsValid());
        }

        [Fact]
        public void IsValid_False()
        {
            const string message1 = "ala ma kota";
            const string message2 = "kot ma kota";
            Blockchain<string> bc = new Blockchain<string>()
            {
                message1,
                message2
            };
            bc[1].Data = "Hack";
            Assert.True(!bc.IsValid());
        }
    }
}

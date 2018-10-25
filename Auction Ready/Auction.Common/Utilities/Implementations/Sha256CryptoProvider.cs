using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Auction.Common.Utilities.Interfaces;

namespace Auction.Common.Utilities.Implementations
{
    /// <summary>
    /// Represent SHA-256 crypto provider
    /// </summary>
    public class Sha256CryptoProvider : ICryptoProvider
    {
        private readonly SHA256CryptoServiceProvider _provider;
        public Sha256CryptoProvider()
        {
            _provider = new SHA256CryptoServiceProvider();
        }

        public string ComputeHash(byte[] data)
        {
            var hash =
                from h in _provider.ComputeHash(data)
                select (h + 0x100).ToString("x").Substring(1);

            return string.Join("", hash);
        }

        public string ComputeHash(string value)
        {
            return ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}

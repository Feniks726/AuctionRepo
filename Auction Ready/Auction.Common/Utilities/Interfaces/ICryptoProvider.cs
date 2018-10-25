namespace Auction.Common.Utilities.Interfaces
{
    /// <summary>
    /// Represent crypto provider
    /// </summary>
    public interface ICryptoProvider
    {
        /// <summary>
        /// Compute hash by byte array
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string ComputeHash(byte[] data);

        /// <summary>
        /// Compute hash by string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string ComputeHash(string value);
    }
}

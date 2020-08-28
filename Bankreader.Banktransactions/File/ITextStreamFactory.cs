using Bankreader.Application.Interfaces;
using System.IO;

namespace Bankreader.Infrastructure.Files
{
    /// <summary>
    /// Factory for creating TextStreams
    /// </summary>
    public interface ITextStreamFactory
    {
        /// <summary>
        /// Creates a TextReader
        /// </summary>
        /// <param name="path">Path where the TextReader should read from</param>
        TextReader Create(IFileInfoWrapper path);
    }
}
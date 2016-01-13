using System;
using BVSeoSdkDotNet.BVException;

namespace BVSeoSdkDotNet.Content.Loaders
{
    /// <summary>
    ///     Tool for loading content from a <see cref="Uri" />.
    /// </summary>
    internal interface IContentLoader
    {
        /// <summary>
        ///     Retrieve an HTML segment
        /// </summary>
        /// <param name="uri">Unique identifier for the HTML segment</param>
        /// <returns></returns>
        /// <exception cref="BVSdkException"></exception>
        string LoadContent(Uri uri);
    }
}
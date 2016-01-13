using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BVSeoSdkDotNet.BVException;

namespace BVSeoSdkDotNet.Content.Loaders
{
    internal interface IContentLoader
    {
        /// <summary>
        /// Retrieve an HTML segment
        /// </summary>
        /// <param name="uri">Unique identifier for the HTML segment</param>
        /// <returns></returns>
        /// <exception cref="BVSdkException"></exception>
        string LoadContent(Uri uri);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web;

namespace WinComposition.Playground {
	/// <summary>
	/// Sample URI resolver object for use with NavigateToLocalStreamUri
	/// This sample uses the local storage of the package as an example of how to write a resolver.
	/// </summary>
	public sealed class StreamUriWinRTResolver : IUriToStreamResolver {
		/// <summary>
		/// The entry point for resolving a Uri to a stream.
		/// </summary>
		/// <param name="uri"></param>
		/// <returns></returns>
		public IAsyncOperation<IInputStream> UriToStreamAsync(Uri uri) {
			// https://blogs.windows.com/buildingapps/2013/07/17/whats-new-in-webview-in-windows-8-1/
			if (uri == null)
			{
				throw new Exception();
			}
			string path = uri.AbsolutePath;
			// Because of the signature of this method, it can't use await, so we 
			// call into a separate helper method that can use the C# await pattern.
			return getContent(path).AsAsyncOperation();
		}
		/// <summary>
		/// Helper that maps the path to package content and resolves the Uri
		/// Uses the C# await pattern to coordinate async operations
		/// </summary>
		private async Task<IInputStream> getContent(string path) {
			// We use a package folder as the source, but the same principle should apply
			// when supplying content from other locations
			try
			{
				Uri localUri = new Uri("ms-appx://" + path);
				StorageFile f = await StorageFile.GetFileFromApplicationUriAsync(localUri);
				IRandomAccessStream stream = await f.OpenAsync(FileAccessMode.Read);
				return stream.GetInputStreamAt(0);
			}
			catch (Exception) { throw new Exception("Invalid path"); }
		}
	}
}

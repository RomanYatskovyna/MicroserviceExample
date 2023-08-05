using System.IO.Compression;
using Grpc.Net.Compression;

namespace UserService.Presentation.GrpcCompressionProviders;

public class BrotliCompressionProvider : ICompressionProvider
{
	private readonly CompressionLevel? _compressionLevel;

	public BrotliCompressionProvider(CompressionLevel compressionLevel)
	{
		_compressionLevel = compressionLevel;
	}

	public BrotliCompressionProvider()
	{

	}
	public string EncodingName => "br"; // Must match grpc-accept-encoding

	public Stream CreateCompressionStream(Stream stream, CompressionLevel? compressionLevel)
	{
		if (_compressionLevel is not null)
			return new BrotliStream(stream, compressionLevel ?? _compressionLevel.Value, true);
		if (_compressionLevel is null && compressionLevel is not null)
			return new BrotliStream(stream, compressionLevel.Value, true);
		return new BrotliStream(stream, CompressionLevel.Fastest, true);
	}

	public Stream CreateDecompressionStream(Stream stream)
	{
		return new BrotliStream(stream, CompressionMode.Decompress);
	}
}
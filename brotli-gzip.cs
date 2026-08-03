using System.IO.Compression; // To use BrotliStream, GZipStream.
using System.Xml; // To use XmlWriter, XmlReader.
using static System.Console;
using static System.IO.Path;
using static System.IO.File;
using static System.IO.Directory;


Compress(algorithm: "brotli");
Compress(algorithm: "gzip");



void Compress(string algorithm = "gzip")
{
    var watch = System.Diagnostics.Stopwatch.StartNew();
  // Define a file path using the algorithm as file extension.
  string filePath = Combine(
    Directory.GetCurrentDirectory(), $"streams.{algorithm}");

  FileStream file = Create(filePath);

  Stream compressor;

  if (algorithm == "gzip")
  {
    compressor = new GZipStream(file, CompressionMode.Compress);
  }
  else
  {
    compressor = new BrotliStream(file, CompressionMode.Compress);
  }

  using (compressor)
  {
    using (XmlWriter xml = XmlWriter.Create(compressor))
    {
      xml.WriteStartDocument();
      xml.WriteStartElement("callsigns");

      foreach (string item in Viper.Callsigns)
      {
        xml.WriteElementString("callsign", item);
      }
    }
  } // Also closes the underlying stream.

  OutputFileInfo(filePath);

  // Read the compressed file.
  WriteLine("Reading the compressed XML file:");
  file = Open(filePath, FileMode.Open);

  Stream decompressor;

  if (algorithm == "gzip")
  {
    decompressor = new GZipStream(
      file, CompressionMode.Decompress);
  }
  else
  {
    decompressor = new BrotliStream(
      file, CompressionMode.Decompress);
  }

  using (decompressor)
  using (XmlReader reader = XmlReader.Create(decompressor))
    while (reader.Read())
    {
      // Check if we are on an element node named callsign.
      if ((reader.NodeType == XmlNodeType.Element)
        && (reader.Name == "callsign"))
      {
        reader.Read(); // Move to the text inside element.
        WriteLine($"{reader.Value}"); // Read its value.
      }

      // Alternative syntax with property pattern matching:
      // if (reader is { NodeType: XmlNodeType.Element,
      //   Name: "callsign" })
    }
    watch.Stop();
    var elapsedMs = watch.ElapsedMilliseconds;
    WriteLine($"{algorithm}: {elapsedMs}");
}

static void OutputFileInfo(string filePath)
{
  FileInfo info = new(filePath);
  WriteLine($"File: {info.Name}");
  WriteLine($"Size: {info.Length:N0} bytes");
}

public class Viper
{
  // Define a field for the Viper pilot call signs.
  public static string[] Callsigns = new[]
  {
    "Husker", "Starbuck", "Apollo", "Boomer",
    "Bulldog", "Athena", "Helo", "Racetrack"
  };
}

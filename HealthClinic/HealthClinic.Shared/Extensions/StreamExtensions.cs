using System.IO;

namespace HealthClinic.Shared
{
    public static class StreamExtensions
    {
        public static byte[] ConvertStreamToByteArrary(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}

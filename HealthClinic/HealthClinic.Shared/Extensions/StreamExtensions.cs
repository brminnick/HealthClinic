using System.IO;

namespace HealthClinic.Common
{
    public static class StreamExtensions
    {
        public static byte[] ConvertStreamToByteArrary(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}

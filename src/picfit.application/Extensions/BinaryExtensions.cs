using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace picfit.application.Extensions
{
    public static class BinaryExtensions
    {
        public static byte[] ConvertToByteArray(this System.IO.Stream stream)
        {
            var streamLength = Convert.ToInt32(stream.Length);
            byte[] data = new byte[streamLength + 1];

            //convert to to a byte array
            stream.Read(data, 0, streamLength);
            stream.Close();

            return data;
        }
    }
}

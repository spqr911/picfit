using picfit.application.Infrastructure.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace picfit.infrastructure.Image
{
    public class Image: IImage
    {
        public int Scale { get; private set; }
        public byte[] Data { get; private set; }
        public Image(int scale, byte[] data)
        {
            Scale = scale;
            Data = data;
        }
    }
}

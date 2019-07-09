using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace picfit.application.Infrastructure.Image
{
    public interface IImage
    {
        int Scale { get; }
        byte[] Data { get; }
    }
}

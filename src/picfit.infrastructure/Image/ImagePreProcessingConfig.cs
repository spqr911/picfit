using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.infrastructure.Image
{
    public class ImagePreProcessingConfig
    {
        public string Type { get; set; }
        public ushort[] Scales { get; set; }
    }
}

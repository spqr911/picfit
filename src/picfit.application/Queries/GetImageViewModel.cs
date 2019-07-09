using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Queries
{
    public class GetImageViewModel
    {
        public byte[] Data { get; set; }
        public GetImageViewModel(byte[] data)
        {
            Data = data;
        }
    }
}

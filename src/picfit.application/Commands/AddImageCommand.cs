using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace picfit.application.Commands
{
    public class AddImageCommand : IRequest<AddImageCommandResult>
    {
        public byte[] Data { get; private set; }
        public string FileName { get; private set; }
        public bool RewriteMode { get; private set; }
        public AddImageCommand(byte[] data, string fileName, bool rewriteMode = false)
        {
            Data = data;
            FileName = fileName.ToLowerInvariant();
            RewriteMode = rewriteMode;
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Commands
{
    public class DeleteImageCommand : IRequest<DeleteImageCommandResult>
    {
        public string FolderName { get; private set; }
        public DeleteImageCommand(string folderName)
        {
            FolderName = folderName.ToLowerInvariant();
        }
    }
}

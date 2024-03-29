﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Queries
{
    public class GetScaledImageQuery : IRequest<GetImageViewModel>
    {
        public string FolderName { get; private set; }
        public string FileName { get; private set; }
        public GetScaledImageQuery(string folderName, string fileName)
        {
            FolderName = folderName.ToLowerInvariant();
            FileName = fileName.ToLowerInvariant();
        }
    }
}

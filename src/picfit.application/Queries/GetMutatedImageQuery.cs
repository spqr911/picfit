using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Queries
{
    public class GetMutatedImageQuery : IRequest<GetImageViewModel>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Extension { get; private set; }
    }
}

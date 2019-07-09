using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Commands
{
    public class DeleteImageCommandResult
    {
        public IEnumerable<string> Deleted { get; private set; }

        public DeleteImageCommandResult(IEnumerable<string> deleted)
        {
            Deleted = deleted;
        }
    }
}

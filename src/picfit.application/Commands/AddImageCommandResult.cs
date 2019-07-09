using System;
using System.Collections.Generic;
using System.Text;

namespace picfit.application.Commands
{
    public class AddImageCommandResult
    {
        public string Key { get; private set; }
        public IEnumerable<string> Scaled { get; private set; }
        public AddImageCommandResult(string key, IEnumerable<string> scaled)
        {
            Key = key;
            Scaled = scaled;
        }
    }
}

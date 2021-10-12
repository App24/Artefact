using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Saving
{
    [Serializable]
    class Saveable
    {
        public string SaveVersion { get; set; }
    }
}

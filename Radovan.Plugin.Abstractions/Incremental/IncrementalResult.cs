using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radovan.Plugin.Abstractions.Incremental
{
    public class IncrementalResult<T>
    {
        public string NewPage { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}

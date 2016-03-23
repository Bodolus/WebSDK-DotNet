using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accela.Web.SDK.Models
{
    public class CustomTable
    {
        public string id { get; set; }
        public CustomTableRow[] rows { get; set; }
    }
    public class CustomTableRow
    {
        public string id { get; set; }
        public Dictionary<string,string> fields { get; set; }
    }
}

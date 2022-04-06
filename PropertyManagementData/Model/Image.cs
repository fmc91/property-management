using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagementData.Model
{
    public class Image
    {
        public int ImageId { get; set; }

        public int? PropertyId { get; set; }

        public string FileName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagementCommon
{
    public class StorageOptions
    {
        public const string SectionName = "StorageOptions";

        public string SavedImageDirectory { get; set; }
    }
}

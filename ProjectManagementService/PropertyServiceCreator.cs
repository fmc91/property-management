using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementService
{
    public class PropertyServiceCreator
    {
        public PropertyService Create()
        {
            return new PropertyService(new PropertyManagementData.PropertyManagementContext());
        }
    }
}

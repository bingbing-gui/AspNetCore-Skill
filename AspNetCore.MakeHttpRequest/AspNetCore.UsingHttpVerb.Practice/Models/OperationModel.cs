using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.UsingHttpVerb.Practice.Models
{
    public class OperationModel
    {
        public string OperationIdFromRequestScope
        {
            get;
            set;
        }
        public string OperationIdFromHandlerScope
        {
            get;
            set;
        }

    }
}

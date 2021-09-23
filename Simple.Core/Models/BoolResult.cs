using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Core.Models
{
    public class BoolResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public int? Id { get; set; }

        public BoolResult()
        {

        }

        public BoolResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }
        public BoolResult(bool result, string message, int? id)
        {
            Result = result;
            Message = message;
            Id = id;
        }

    }
}

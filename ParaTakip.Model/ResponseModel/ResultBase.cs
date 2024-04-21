using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParaTakip.Model.ResponseModel
{
    public class ResultBase
    {
        public int ReferenceId { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}

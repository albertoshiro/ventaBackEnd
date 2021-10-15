using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSVenta.Models.Response
{
    public class Response
    {
        //solo 1 o 0, para ver el estatus
        public int  Success { get; set; }
        public string Message { get; set; }
        //a un tipo object le puedes meter cualquier tipo de objeto
        public object Data { get; set; }



    }
}

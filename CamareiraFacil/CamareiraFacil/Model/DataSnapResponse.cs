using System;
using System.Collections.Generic;
using System.Text;

namespace CamareiraFacil.Model
{
    public class DataSnapResponse<T>
    {
        public string message { get; set; }
        public string status { get; set; }
        public T result { get; set; }
    }
}

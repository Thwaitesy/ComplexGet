using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComplexGet.Contracts
{
    public class ComplexGet
    {
        public string Filter { get; set; }

        public string OrderBy { get; set; }

        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public ComplexGet()
        {
            this.PageNumber = 1;
            this.ItemsPerPage = 25;
            this.Filter = "";
            this.OrderBy = "";
        }
    }
}

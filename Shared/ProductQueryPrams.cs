using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryPrams
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 50;
        public int? brandId { get; set; }
        public int? TypeId { get; set; }

        public ProductSortingOption SortingOption { get; set; }

        public string? SearchValue { get; set; }

        public int PageIndex { get; set; } =1;


        private int pageSize = DefaultPageSize;
        public int PageSize

        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }




        }

    }
}

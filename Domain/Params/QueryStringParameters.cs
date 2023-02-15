using System.ComponentModel.DataAnnotations;

namespace Domain.Params
{
    public class QueryStringParameters
    {
        const int maxPageSize = 50;

        [Range(1, int.MaxValue, ErrorMessage = "Page Number must be greater than zero")]
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        [Range(1, int.MaxValue, ErrorMessage = "Page Size must be greater than zero")]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string OrderBy { get; set; }
    }
}

using System;
namespace ApplicationCore.Models
{
	public class PagedResultSetModel<T> where T : class
	{
        // constructor
        public PagedResultSetModel(int pageNumber, int totalRecords, int pageSize, IEnumerable<T> pagedData)
        {
            PageNumber = pageNumber;
            TotalRecords = totalRecords;
            TotalPages = (int) Math.Ceiling(totalRecords/ (double) pageSize);
            PagedData = pagedData;
        }

        // properties
        public int PageNumber { get; }
        public int TotalRecords { get; }
        public int TotalPages { get; }
        public int PageSize { get; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public IEnumerable<T> PagedData { get; set; }
    }
}


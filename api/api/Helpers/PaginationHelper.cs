﻿namespace api.Helpers
{
    public class PaginationHelper
    {
        public PaginationHelper(int currentPage, int itemPerPage,
            int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemPerPage = itemPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public int CurrentPage { get; set; }    
        public int ItemPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

    }
}

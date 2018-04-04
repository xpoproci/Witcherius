using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Witcherius.PL.Models
{
    public class PaginationModel
    {
        public int LeftBound { get; private set; }
        public int RightBound { get; private set; }
        public int Current => _currentPage;
        public int Last => (_listCount + _pageSize - 1) / _pageSize;

        private readonly int _currentPage;
        private readonly int _listCount;
        private readonly int _maxNumberOfPages;
        private readonly int _pageSize;

        public PaginationModel(int currentPage, int listCount)
        {
            _currentPage = currentPage;
            _listCount = listCount;
            _maxNumberOfPages = 5;
            _pageSize = 10;
            CalculateBounds();
        }

        private void CalculateBounds()
        {
            if (_listCount / _pageSize > _maxNumberOfPages)
            {
                LeftBound = _currentPage - 2;
                RightBound = _currentPage + 2;
            }
            
            LeftBound = 1;
            RightBound = (_listCount + _pageSize - 1) / _pageSize;
        }
    }
}
namespace Talabat.APIs.Helper
{
    public class PaginationItem<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public PaginationItem(int _PageIndex, int _PageSize,int count, IReadOnlyList<T> _Data) 
        { 
            PageSize= _PageSize;
            PageIndex= _PageIndex;
            Data= _Data;
            Count = count;
        
        }

    }
}

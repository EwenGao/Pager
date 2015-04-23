 public class Pager<T> : List<T>
    {
        public Pager(IEnumerable<T> items)
        {
            AddRange(items);
            ShowNumber = 8;
            Current = 1;
            PageName = "pageIndex";
        }
        public Pager()
        {

        }
        public string PageName { get; set; }

        public string Container { get; set; }

        public string ClassName { get; set; }

        public string CurreFormat { get; set; }

        public string PageFormat { get; set; }

        public string PrevFormat { get; set; }

        public string NextFormat { get; set; }

        public int Current { get; set; }

        public int RecordCount { get; set; }

        public int PageSize { get; set; }

        public int ShowNumber { get; set; }

        public bool AlwaysShow { get; set; }

    }

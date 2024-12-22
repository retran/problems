public class SQL
{
    private class Row
    {
        public int Id { get; private set; }
        public IList<string> Values { get; private set; }
        public bool Deleted { get; set; }

        public Row(int id, IList<string> row)
        {
            Id = id;
            Values = row;
            Deleted = false;
        }
    }

    private class Table
    {
        public int CurrentMaxId { get; private set; }

        public int Columns { get; private set; }

        public IReadOnlyList<Row> Rows
        {
            get
            {
                return _rows;
            }
        }

        private List<Row> _rows;

        public Table(int columns)
        {
            CurrentMaxId = 0;
            Columns = columns;
            _rows = new List<Row>();
        }

        internal void Add(IList<string> row)
        {
            CurrentMaxId++;
            _rows.Add(new Row(CurrentMaxId, row));
        }
    }

    private Dictionary<string, Table> _tables;

    public SQL(IList<string> names, IList<int> columns)
    {
        _tables = new Dictionary<string, Table>();
        for (int i = 0; i < names.Count; i++)
        {
            _tables[names[i]] = new Table(columns[i]);
        }
    }

    public void InsertRow(string name, IList<string> row)
    {
        var table = _tables[name];
        table.Add(row);
    }

    public void DeleteRow(string name, int rowId)
    {
        var table = _tables[name];
        var row = table.Rows[rowId - 1];
        row.Deleted = true;
    }

    public string SelectCell(string name, int rowId, int columnId)
    {
        var table = _tables[name];
        var row = table.Rows[rowId - 1];
        if (row.Deleted)
        {
            return null;
        }

        return row.Values[columnId - 1];
    }
}

/**
 * Your SQL object will be instantiated and called as such:
 * SQL obj = new SQL(names, columns);
 * obj.InsertRow(name,row);
 * obj.DeleteRow(name,rowId);
 * string param_3 = obj.SelectCell(name,rowId,columnId);
 */
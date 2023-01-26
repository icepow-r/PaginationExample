using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Pagination
{
    public partial class Form1 : Form
    {
        private const int TotalRecords = 66;
        private const int PageSize = 10;

        public Form1()
        {
            InitializeComponent();
            bindingSource1.DataSource = new PageOffsetList();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var offset = (int)bindingSource1.Current;
            var records = new List<Record>();
            for (var i = offset; i < offset + PageSize && i < TotalRecords; i++)
                records.Add(new Record { Index = i });
            listBox1.DataSource = records;
            listBox1.DisplayMember = "Index";
        }

        private class Record
        {
            public int Index { get; set; }
        }

        private class PageOffsetList : IListSource
        {
            public bool ContainsListCollection { get; protected set; }

            public IList GetList()
            {
                var pageOffsets = new List<int>();
                for (var offset = 0; offset < TotalRecords; offset += PageSize)
                    pageOffsets.Add(offset);
                return pageOffsets;
            }
        }
    }
}

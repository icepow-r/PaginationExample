using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Pagination
{
    public partial class Form1 : Form
    {
        private const int totalRecords = 66;
        private const int pageSize = 10;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Index" });
            bindingNavigator1.BindingSource = bindingSource1;
            bindingSource1.DataSource = new PageOffsetList();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var offset = (int)bindingSource1.Current;
            var records = new List<Record>();
            for (var i = offset; i < offset + pageSize && i < totalRecords; i++)
                records.Add(new Record { Index = i });
            dataGridView1.DataSource = records;
        }

        class Record
        {
            public int Index { get; set; }
        }

        class PageOffsetList : IListSource
        {
            public bool ContainsListCollection { get; protected set; }

            public IList GetList()
            {
                var pageOffsets = new List<int>();
                for (var offset = 0; offset < totalRecords; offset += pageSize)
                    pageOffsets.Add(offset);
                return pageOffsets;
            }
        }
    }
}

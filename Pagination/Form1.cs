using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Pagination
{
    public partial class Form1 : Form
    {
        private const int PageSize = 15;
        private readonly List<Material> items;
        private int listOffset = 0;
        private MaterialMembers listcurrentSortValueCount = MaterialMembers.Id;

        public Form1()
        {
            var random = new Random();
            InitializeComponent();

            toolStripComboBox1.Items.AddRange(Enum.GetNames(typeof(MaterialMembers)));
            toolStripComboBox1.SelectedIndex = 0;
            toolStripComboBox1.SelectedIndexChanged += toolStripComboBox1_SelectedIndexChanged;

            items = new List<Material>(Enumerable.Range(0, 301)
                .Select(x => new Material
                {
                    Id = x,
                    Name = Guid.NewGuid().ToString("N"),
                    Count = random.Next(5000),
                }));
            bindingSource1.DataSource = new List<int>(
                Enumerable.Range(1, (int)Math.Ceiling((decimal)items.Count / PageSize)));
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            listOffset = (int)bindingSource1.Current - 1;
            SetListDataSource(listOffset, listcurrentSortValueCount);
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listcurrentSortValueCount = (MaterialMembers)Enum.Parse(typeof(MaterialMembers),
                toolStripComboBox1.SelectedItem.ToString());
            SetListDataSource(listOffset, listcurrentSortValueCount);
        }

        private void SetListDataSource(int offset, MaterialMembers orderBy)
        {
            listBox1.DataSource = items
                .OrderBy(GetOrderByLambda(orderBy))
                .Skip(offset * PageSize)
                .Take(PageSize)
                .ToList();
        }

        private Func<Material, object> GetOrderByLambda(MaterialMembers member)
        {
            switch (member)
            {
                case MaterialMembers.Id:
                    return x => x.Id;
                case MaterialMembers.Name:
                    return x => x.Name;
                case MaterialMembers.Count:
                    return x => x.Count;
                default:
                    throw new ArgumentOutOfRangeException(nameof(member), member, null);
            }
        }
    }
}

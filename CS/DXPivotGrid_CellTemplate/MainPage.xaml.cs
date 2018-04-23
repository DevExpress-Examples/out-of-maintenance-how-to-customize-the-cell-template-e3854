using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.PivotGrid.Internal;

namespace DXPivotGrid_CellTemplate {
    public partial class MainPage : UserControl {
        string dataFileName = "DXPivotGrid_CellTemplate.nwind.xml";
        public MainPage() {
            InitializeComponent();

            // Parses an XML file and creates a collection of data items.
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(dataFileName);
            XmlSerializer s = new XmlSerializer(typeof(OrderData));
            object dataSource = s.Deserialize(stream);

            // Binds a pivot grid to this collection.
            pivotGridControl1.DataSource = dataSource;
        }

        private void cellShare_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            ProgressBarEdit bar = ((ProgressBarEdit)sender);
            CellsAreaItem item = bar.DataContext as CellsAreaItem;
            if (item == null)
                return;
            bar.Maximum = Convert.ToDouble(item.RowTotalValue);
            bar.Value = Convert.ToDouble(item.Value);
        }
    }
}

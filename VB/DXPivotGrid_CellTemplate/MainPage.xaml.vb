Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Controls
Imports System.Xml.Serialization
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.PivotGrid.Internal

Namespace DXPivotGrid_CellTemplate
	Partial Public Class MainPage
		Inherits UserControl
		Private dataFileName As String = "nwind.xml"
		Public Sub New()
			InitializeComponent()

			' Parses an XML file and creates a collection of data items.
			Dim [assembly] As System.Reflection.Assembly = _
				System.Reflection.Assembly.GetExecutingAssembly()
			Dim stream As Stream = [assembly].GetManifestResourceStream(dataFileName)
			Dim s As New XmlSerializer(GetType(OrderData))
			Dim dataSource As Object = s.Deserialize(stream)

			' Binds a pivot grid to this collection.
			pivotGridControl1.DataSource = dataSource
		End Sub

		Private Sub cellShare_DataContextChanged(ByVal sender As Object, _
			ByVal e As DependencyPropertyChangedEventArgs)
			Dim bar As ProgressBarEdit = (CType(sender, ProgressBarEdit))
			Dim item As CellsAreaItem = TryCast(bar.DataContext, CellsAreaItem)
			If item Is Nothing Then
				Return
			End If
			bar.Maximum = Convert.ToDouble(item.RowTotalValue)
			bar.Value = Convert.ToDouble(item.Value)
		End Sub
	End Class
End Namespace

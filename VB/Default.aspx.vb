Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.Web.Data
Imports DevExpress.Web
Imports System.Web.UI

Partial Public Class _Default
    Inherits System.Web.UI.Page

    #Region "Common settings"
    'see https://www.devexpress.com/Support/Center/Example/Details/E5045
    Protected ReadOnly Property GridData() As List(Of GridDataItem)
        Get
            Dim key = "34FAA431-CF79-4869-9488-93F6AAE81263"
            If Not IsPostBack OrElse Session(key) Is Nothing Then
                Session(key) = Enumerable.Range(0, 10).Select(Function(i) New GridDataItem With { _
                    .ID = i, _
                    .rate = i Mod 10+1, _
                    .num = i * 0.5 Mod 3 _
                }).ToList()
            End If
            Return DirectCast(Session(key), List(Of GridDataItem))
        End Get
    End Property
    Protected Sub Grid_BatchUpdate(ByVal sender As Object, ByVal e As ASPxDataBatchUpdateEventArgs)
        For Each args In e.InsertValues
            InsertNewItem(args.NewValues)
        Next args
        For Each args In e.UpdateValues
            UpdateItem(args.Keys, args.NewValues)
        Next args
        For Each args In e.DeleteValues
            DeleteItem(args.Keys, args.Values)
        Next args

        e.Handled = True
    End Sub
    Protected Function InsertNewItem(ByVal newValues As OrderedDictionary) As GridDataItem
        Dim item = New GridDataItem() With {.ID = GridData.Count}
        LoadNewValues(item, newValues)
        GridData.Add(item)
        Return item
    End Function
    Protected Function UpdateItem(ByVal keys As OrderedDictionary, ByVal newValues As OrderedDictionary) As GridDataItem

        Dim id_Renamed = Convert.ToInt32(keys("ID"))
        Dim item = GridData.First(Function(i) i.ID = id_Renamed)
        LoadNewValues(item, newValues)
        Return item
    End Function
    Protected Function DeleteItem(ByVal keys As OrderedDictionary, ByVal values As OrderedDictionary) As GridDataItem

        Dim id_Renamed = Convert.ToInt32(keys("ID"))
        Dim item = GridData.First(Function(i) i.ID = id_Renamed)
        GridData.Remove(item)
        Return item
    End Function
    Protected Sub LoadNewValues(ByVal item As GridDataItem, ByVal values As OrderedDictionary)
        item.rate = Convert.ToInt32(values("rate"))
        item.num = Convert.ToDouble(values("num"))

    End Sub
    Protected Sub CancelEditing(ByVal e As CancelEventArgs)
        e.Cancel = True
        ASPxGridView1.CancelEdit()
    End Sub
    Public Class GridDataItem
        Public Property ID() As Integer
        Public Property rate() As Integer
        Public Property num() As Double
    End Class
    #End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        ASPxGridView1.DataSource = GridData
        ASPxGridView1.DataBind()
        ASPxGridView1.SettingsEditing.BatchEditSettings.AllowRegularDataItemTemplate = supportDataItemTemplate.Checked
    End Sub

End Class
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DevExpress.Web.Data;
using DevExpress.Web;
using System.Web.UI;

public partial class _Default: System.Web.UI.Page {
    #region Common settings
    //see https://www.devexpress.com/Support/Center/Example/Details/E5045
    protected List<GridDataItem> GridData
    {
        get
        {
            var key = "34FAA431-CF79-4869-9488-93F6AAE81263";
            if (!IsPostBack || Session[key] == null)
                Session[key] = Enumerable.Range(0, 10).Select(i => new GridDataItem
                {
                    ID = i,
                    rate = i % 10+1,
                    num = i * 0.5 % 3,
                }).ToList();
            return (List<GridDataItem>)Session[key];
        }
    }
    protected void Grid_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e) {
        foreach (var args in e.InsertValues)
            InsertNewItem(args.NewValues);
        foreach (var args in e.UpdateValues)
            UpdateItem(args.Keys, args.NewValues);
        foreach (var args in e.DeleteValues)
            DeleteItem(args.Keys, args.Values);

        e.Handled = true;
    }
    protected GridDataItem InsertNewItem(OrderedDictionary newValues) {
        var item = new GridDataItem() { ID = GridData.Count };
        LoadNewValues(item, newValues);
        GridData.Add(item);
        return item;
    }
    protected GridDataItem UpdateItem(OrderedDictionary keys, OrderedDictionary newValues) {
        var id = Convert.ToInt32(keys["ID"]);
        var item = GridData.First(i => i.ID == id);
        LoadNewValues(item, newValues);
        return item;
    }
    protected GridDataItem DeleteItem(OrderedDictionary keys, OrderedDictionary values) {
        var id = Convert.ToInt32(keys["ID"]);
        var item = GridData.First(i => i.ID == id);
        GridData.Remove(item);
        return item;
    }
    protected void LoadNewValues(GridDataItem item, OrderedDictionary values) {
        item.rate = Convert.ToInt32(values["rate"]);
        item.num = Convert.ToDouble(values["num"]);
     
    }
    protected void CancelEditing(CancelEventArgs e) {
        e.Cancel = true;
        Grid.CancelEdit();
    }
    public class GridDataItem {
        public int ID { get; set; }
        public int rate { get; set; }
        public double num { get; set; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e) {
        Grid.DataSource = GridData;
        Grid.DataBind(); 
        Grid.SettingsEditing.BatchEditSettings.AllowRegularDataItemTemplate = supportDataItemTemplate.Checked;
    }
   
}
<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function onGridBatchEditEndEditing(s, e) {
            var visibleIndex = e.visibleIndex;
            var rowValues = e.rowValues;
            var rate1 = ASPx.GetControlCollection().GetByName("RATE" + visibleIndex);
            if (rate1)
                rate1.SetValue(e.rowValues[0].value);
        }
        function onGridBatchEditChangesCanceling(s, e) {
            setTimeout(function () {
                ASPxGridView1.Refresh();
            }, 200);
        }
    </script>
</head>
<body>
    <form id="frmMain" runat="server">
        <dx:ASPxCheckBox ID="supportDataItemTemplate" runat="server" AutoPostBack="true" Text="Support data item template" Checked="true" />

        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" KeyFieldName="ID" OnBatchUpdate="Grid_BatchUpdate">
            <Columns>
                <dx:GridViewDataSpinEditColumn Name="rate1" FieldName="rate" ReadOnly="false" Width="100px" Caption="Rate">
                    <PropertiesSpinEdit MinValue="0" MaxValue="10" NumberType="Integer"></PropertiesSpinEdit>
                    <DataItemTemplate>
                        <dx:ASPxRatingControl ID="ratingControl" runat="server" ClientInstanceName='<%#"RATE" & Container.VisibleIndex%>'
                            ReadOnly="true" ItemCount="10" Value='<%#Convert.ToInt32(Eval("rate"))%>'>
                        </dx:ASPxRatingControl>
                    </DataItemTemplate>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataColumn Name="num" FieldName="num" Caption="Num">
                </dx:GridViewDataColumn>
                <dx:GridViewCommandColumn ShowEditButton="true" />
            </Columns>
            <SettingsEditing Mode="Batch" />
            <ClientSideEvents BatchEditEndEditing="onGridBatchEditEndEditing" BatchEditChangesCanceling="onGridBatchEditChangesCanceling" />
        </dx:ASPxGridView>
    </form>
</body>
</html>
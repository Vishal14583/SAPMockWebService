<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SapInterfaceData.aspx.cs" Inherits="SAPMockWebService.SapInterfaceData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/GridView1.css" rel="stylesheet" />
    <link href="css/GridView2.css" rel="stylesheet" />
    <link href="css/GridView3.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:Label ID="lblamounttext" Font-Bold="true" Visible="false" ForeColor="#3333ff" Font-Size="X-Large" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lblamount" Font-Bold="true" Visible="false" ForeColor="#003399" Font-Size="XX-Large" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="transactionList" CssClass="transactionList" runat="server" AutoGenerateColumns="false" AutoGenerateSelectButton="true" 
                OnSelectedIndexChanged="transactionList_SelectedIndexChanged" OnPageIndexChanging="transactionList_PageIndexChanging" AllowPaging="true" PageSize="5">
                <Columns>
                    <asp:BoundField DataField="UNIQUEID" HeaderText="UNIQUEID"  />
                    <asp:BoundField DataField="NAME" HeaderText="NAME"  />
                    <asp:BoundField DataField="GROSSAMOUNT" HeaderText="GROSSAMOUNT"  />
                    <asp:BoundField DataField="TIMESTAMP" HeaderText="TIMESTAMP"  />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblMessage" Font-Bold="true" Visible="false" ForeColor="#0066cc" Font-Size="X-Large" runat="server" Text="Label"></asp:Label>
            <br />
            <hr />
            <br />
            <asp:Label runat="server" Visible="false" ForeColor="#ff5050" Font-Bold="true" Font-Size="X-Large" ID="lblOrder"></asp:Label>
            <asp:Label ID="lblOrderId" Visible="false" ForeColor="#cc0000" Font-Bold="true"  Font-Size="X-Large" runat="server" Text="Label"></asp:Label>
            <br />
            <br />

            <asp:GridView ID="GridView1" CssClass="gridview" runat="server" OnRowDataBound="GridView1_RowDataBound"></asp:GridView>
            <br />
            <br />
            <asp:GridView ID="GridView2" CssClass="productsgridview" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>

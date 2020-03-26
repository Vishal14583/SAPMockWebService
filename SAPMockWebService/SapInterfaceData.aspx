<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SapInterfaceData.aspx.cs" Inherits="SAPMockWebService.SapInterfaceData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/GridView1.css" rel="stylesheet" />
    <link href="css/GridView2.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <asp:Label ID="lblamounttext" Font-Bold="true" Visible="false" ForeColor="#3333ff" Font-Size="X-Large" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lblamount" Font-Bold="true" Visible="false" ForeColor="#003399" Font-Size="XX-Large" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMessage" Font-Bold="true" Visible="false" ForeColor="#0066cc" Font-Size="X-Large" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label runat="server" Visible="false" ForeColor="#ff5050" Font-Bold="true" Font-Size="X-Large" ID="lblOrder"></asp:Label>
            <asp:Label ID="lblOrderId" Visible="false" ForeColor="#cc0000" Font-Bold="true"  Font-Size="X-Large" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
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

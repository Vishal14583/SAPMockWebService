<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SapInterfaceData.aspx.cs" Inherits="SAPMockWebService.SapInterfaceData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" Font-Bold="true" Font-Size="X-Large" runat="server" Text="Label">Data passed to mock service</asp:Label>
            <br /><br />
            <asp:GridView ID="GridView1" runat="server">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:GridView ID="GridView2" runat="server"></asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
             <br /><br />
            <asp:Label ID="totalamount" Font-Bold="true" Font-Size="X-Large" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>

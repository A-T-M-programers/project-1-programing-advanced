<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Project-ControlPanel.aspx.cs" Inherits="project_1_programing_advanced.Project_ControlPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="ADD" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Update" />
            <br />
            <br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Delete" />
            <br />
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Move To Data" />
            <br />
            <br />
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click1" Text="Move By Entity FrameWork" />
        </div>
    </form>
</body>
</html>

<%@ Page Title="Tasks" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="WebApplicationPractice.Tasks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <br />
    <asp:Button Text="Add New Task" runat="server" onclick="submitSpolierAddTasks" />
    <div ID="divSpolier" runat="server" style="display:none"> 
    <asp:Table ID="tableAdd" GridLines="Both" Font-Names="Verdana" Font-Size="8pt" CellPadding="5" CellSpacing="0" Runat="server">
        <asp:TableRow >
            <asp:TableCell>
                Name of Task
            </asp:TableCell>
            <asp:TableCell>
                Duration (HH:MM:SS)
            </asp:TableCell>
            <asp:TableCell>
                Privacy (0 = private, 1 = public)
            </asp:TableCell>
            <asp:TableCell>
                Priority
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow >
            <asp:TableCell>
                <asp:TextBox ID="tbAddName" Text="name" runat="server"/>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="tbAddDuration" Text="00:00:00" runat="server"/>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="tbAddPrivacy" Text="0" runat="server"/>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="tbAddPriority" Text="0" runat="server"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Button ID="bAddTask" runat="server" Text="Add To Table" OnClick="submitAddToTable"/>
    </div>
    <asp:Label ID="lAddTask" runat="server" ForeColor="Red" />
    <br />
    <br />
    <asp:Table ID="tableTasks" GridLines="Both" Font-Names="Verdana" Font-Size="8pt" CellPadding="10" Runat="server">
    </asp:Table>
    
</asp:Content>
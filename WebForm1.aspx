<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Entwicklertage_2024_1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="de"> 
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="Content/design.css" type="text/css"/>
    <title>Routenplanung</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <h1>Routenplanung</h1>
        </div>

        <div class="body">
            Bitte wählen: <br><br>
            Bahnhof / Haltestelle: <br>
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="listbox"></asp:DropDownList> <br> <br>
            Endstation: <br> 
            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="listbox"></asp:DropDownList> <br> <br>
            <asp:Button ID="Button1" runat="server" Text="Suchen" CssClass="searchbox" onclick="BtnSuchen_Click"/> <br> <br> <br> <br>
            <p>Ihre Route:</p> <!-- Änderung hier -->
            <table class="table">
                
                <tr>
                    <th>Abfahrtszeit</th>
                    <th>Route</th>
                    <th>Ankunftszeit</th>
                </tr>
                <tr>
                    <%-- <td>Uhr</td> --%>
                    <%-- <td>Route</td> --%>
                    <%-- <td>Uhr</td> --%>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="ZeitInMinuten" HeaderText="Abfahrtszeit" />
                            <asp:BoundField DataField="VonHaltestelle" HeaderText="Route" />
                            <asp:BoundField DataField="BisHaltestelle" HeaderText="Ankunftszeit" />
                        </Columns>
                    </asp:GridView>
                </tr>
            </table>

        </div>

    </form>
</body>
</html>
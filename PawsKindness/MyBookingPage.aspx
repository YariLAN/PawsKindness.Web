<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="MyBookingPage.aspx.cs" Inherits="PawsKindness.MyBookingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Мои заявки на питомцев</title>
        <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f9f9f9;
        }

        .container {
            max-width: 800px;
            margin: 50px auto;
            padding: 10px;
            background: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        h3 {
            text-align: center;
            
        }

        .alert {
            padding: 2px;
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
            text-align: center;
        }

        .alert a {
            color: #0056b3;
            text-decoration: none;
            font-weight: bold;
        }

        .alert a:hover {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (Session["UserId"] == null)
       { %>
        <div class="container">
            <h3 class="alert">Необходимо авторизоваться или зарегистрироваться</h3>
        </div>
    <% }
       else
       { %>
        <h3>Мои заявки на питомцев</h3>
        <p>
            <asp:GridView ID="AdoptedGridView" runat="server" 
                HorizontalAlign="Center"
                CellPadding="8" 
                BorderColor="Black" 
                BorderWidth="2" 
                ForeColor="#ffffcc" 
                BackColor="LightBlue"
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="AdoptedId" >
                <Columns>
                    <asp:CommandField ShowSelectButton="False" />
                    <asp:BoundField DataField="AdoptedId" HeaderText="AdoptedId" InsertVisible="False" ReadOnly="True" SortExpression="AdoptedId" Visible="False" />
                    <asp:BoundField DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" DataField="BookingDate" HeaderText="Дата добавления заявки" />
                    <asp:BoundField DataField="Name" HeaderText="Кличка"/>
                    <asp:BoundField DataField="BreedName" HeaderText="Порода" />
                    <asp:BoundField DataField="SpeciesName" HeaderText="Вид" />
                    <asp:BoundField DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" DataField="DateBirth" HeaderText="Дата рождения"/>
                    <asp:BoundField DataField="Status" HeaderText="Статус брони" />
                    <asp:BoundField DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" DataField="DateAdoption" HeaderText="Дата приюта хозяину" />
                </Columns>
            </asp:GridView>
        </p>
    <% } %>
</asp:Content>

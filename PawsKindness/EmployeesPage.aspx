<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="EmployeesPage.aspx.cs" Inherits="PawsKindness.EmployeesPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Главная - Данные о сотрудниках</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p style="text-align: center;">Информация о сотрудниках</p>
    <p style="text-align: center;">
        <asp:TextBox
            ID="TextBox_FullName" 
            runat="server" 
            ForeColor="Black"
            OnTextChanged="SearchEmp"
            AutoPostBack="True"  
            DataTextField="Name" 
            DataValueField="Name" 
            Width="400px"
        />
        <asp:DropDownList
            ID="DropDownList_Role" 
            runat="server" 
            AutoPostBack="True"  
            DataTextField="RoleName" 
            DataValueField="RoleName" 
            Width="100px"
            OnSelectedIndexChanged="DropDownList_RoleIndexChanged"
        />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
            SelectCommand="SELECT [Name] FROM [Roles] WHERE ([Name] = @Name)">
            <SelectParameters>
                <asp:SessionParameter SessionField="IDN" DefaultValue="Админ" Name="Name" Type="String"></asp:SessionParameter>
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Button BackColor="White" ForeColor="Black" ID="ResetButton" runat="server" Text="Сброс" OnClick="Reset_Click"/>
    </p>
    <p>
        <asp:GridView ID="EmployeesGridView" runat="server" 
            HorizontalAlign="Center"
            CellPadding="8" 
            BorderColor="Black" 
            BorderWidth="2" 
            ForeColor="#ffffcc" 
            BackColor="LightBlue"
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="UserId" OnSelectedIndexChanged="EmployeesGridView_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="UserId" HeaderText="UserId" InsertVisible="False" ReadOnly="True" SortExpression="UserId" Visible="False" />
                <asp:BoundField DataField="Name" HeaderText="ФИО"/>
                <asp:BoundField DataField="Email" HeaderText="Электронная почта" />
                <asp:BoundField DataField="Phone" HeaderText="Телефон для связи" />
                <asp:BoundField DataField="RoleName" HeaderText="Должность" />
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:DetailsView ID="EmpDetails" runat="server" 
            AutoGenerateRows="False" 
            DataKeyNames="UserId"  
            Height="50px" 
            Width="600px"
            CellPadding="8"
            HorizontalAlign="Center">
            <Fields>
                <asp:BoundField DataField="UserId" HeaderText="UserId" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="Name" HeaderText="Полное имя"  />
                <asp:BoundField DataField="Email" HeaderText="Электронная почта"  />
                <asp:BoundField DataField="Phone" HeaderText="Телефон для связи" />
                <asp:BoundField DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" DataField="BirthDate" HeaderText="Дата рождения" />
                <asp:BoundField DataField="Address" HeaderText="Адрес" />
                <asp:BoundField DataField="RoleName" HeaderText="Должность сотрудника" />
            </Fields>
        </asp:DetailsView>
    </p>
</asp:Content>

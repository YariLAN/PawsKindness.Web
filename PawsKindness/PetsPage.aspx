<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="PetsPage.aspx.cs" Inherits="PawsKindness.PetsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Главная - Питомцы</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p style="text-align: center;">Питомцы</p>
    <p style="text-align: center;">
        <asp:DropDownList
            ID="DropDownList_Breed" 
            runat="server" 
            AutoPostBack="True"  
            DataTextField="BreedName" 
            DataValueField="BreedName" 
            Width="200px"
        />
        <asp:DropDownList
            ID="DropDownList_Status" 
            runat="server" 
            AutoPostBack="True"  
            DataTextField="StatusName" 
            DataValueField="StatusName" 
            Width="200px"
        />
    </p>
    <p style="text-align: center;">
        <asp:Button BackColor="White" ForeColor="Black" ID="Button2" runat="server" Text="Поиск по породе" OnClick="Breed_Click"/>
        <asp:Button BackColor="White" ForeColor="Black" ID="Button1" runat="server" Text="Поиск по статусу" OnClick="Status_Click"/>
        <asp:Button BackColor="White" ForeColor="Black" ID="Button3" runat="server" Text="Поиск по породе и статусу" OnClick="BreedStatus_Click"/>
        <asp:Button BackColor="White" ForeColor="Black" ID="ResetButton" runat="server" Text="Сброс" OnClick="Reset_Click"/>
    </p>
    <p>
        <asp:GridView ID="PetsGridView" runat="server" 
            HorizontalAlign="Center"
            CellPadding="8" 
            BorderColor="Black" 
            BorderWidth="2" 
            ForeColor="#ffffcc" 
            BackColor="LightBlue"
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PetId" OnSelectedIndexChanged="PetsGridView_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="PetId" HeaderText="PetId" InsertVisible="False" ReadOnly="True" SortExpression="PetId" Visible="False" />
                <asp:BoundField DataField="Name" HeaderText="Кличка"/>
                <asp:BoundField DataField="BreedName" HeaderText="Порода" />
                <asp:BoundField DataField="SpeciesName" HeaderText="Вид" />
                <asp:BoundField DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" DataField="DateBirth" HeaderText="Дата рождения" />
                <asp:BoundField DataField="Weight" HeaderText="Вес"/>
                <asp:BoundField DataField="UserName" HeaderText="Волонтер" />
                <asp:BoundField DataField="StatusName" HeaderText="Статус" />
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:DetailsView ID="PetDetails" runat="server" 
            AutoGenerateRows="False" 
            DataKeyNames="PetId"  
            Height="50px" 
            Width="500px"
            CellPadding="8"
            HorizontalAlign="Center"
        >
            <Fields>
                <asp:BoundField DataField="PetId" HeaderText="PetId" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="Name" HeaderText="Кличка"  />
                <asp:BoundField DataField="BreedName" HeaderText="Порода"  />
                <asp:BoundField DataField="SpeciesName" HeaderText="Вид"  />
                <asp:BoundField DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" DataField="DateBirth" HeaderText="Дата рождения" />
                <asp:BoundField DataField="Weight" HeaderText="Вес" />
                <asp:BoundField DataField="UserName" HeaderText="Добавил волонтер" />
                <asp:BoundField DataField="StatusName" HeaderText="Статус" />
            </Fields>
        </asp:DetailsView>
    </p>
</asp:Content>

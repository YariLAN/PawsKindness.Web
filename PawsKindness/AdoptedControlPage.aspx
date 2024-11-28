<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMainPage.Master" AutoEventWireup="true" CodeBehind="AdoptedControlPage.aspx.cs" Inherits="PawsKindness.AdoptedControlPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Главная - Управление заявками на бронь</title>
    <style>

        h3 {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Заявки на питомцев</h3>
    <div>
        <p>
            <asp:GridView ID="AdminAdoptedGridView" runat="server" 
                HorizontalAlign="Center"
                CellPadding="8" 
                BorderColor="Black" 
                BorderWidth="2" 
                ForeColor="#ffffcc" 
                BackColor="LightBlue"
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="AdoptedId" OnSelectedIndexChanged="AdminAdoptedGridView_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
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
    </div>
    <div style="display: flex; align-items: center; justify-content: center; margin-top: 30px;">
        <div>
            <asp:DetailsView ID="AdoptDetails" runat="server" 
                AutoGenerateRows="False" 
                DataKeyNames="AdoptedId"  
                Height="50px" 
                Width="500px"
                CellPadding="8"
                HorizontalAlign="Center">
                <Fields>
                    <asp:BoundField DataField="AdoptedId" HeaderText="AdoptedId" InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataFormatString="{0:dd.MM.yyyy}" ReadOnly="True" HtmlEncode="false" DataField="BookingDate" HeaderText="Дата добавления заявки" />
                    <asp:BoundField DataFormatString="{0:dd.MM.yyyy}" ReadOnly="True" HtmlEncode="false" DataField="DateAdoption" HeaderText="Дата усыновления" />
                    <asp:BoundField DataField="Name" HeaderText="Кличка"  ReadOnly="True"  />
                    <asp:BoundField DataField="BreedName" HeaderText="Порода"  ReadOnly="True"  />
                    <asp:BoundField DataField="SpeciesName" HeaderText="Вид"  ReadOnly="True"  />
                    <asp:BoundField DataFormatString="{0:dd.MM.yyyy}"  ReadOnly="True" HtmlEncode="false" DataField="DateBirth" HeaderText="Дата рождения"/>
                    <asp:BoundField DataField="Weight" HeaderText="Вес"  ReadOnly="True" />
                    <asp:BoundField DataField="UserName" HeaderText="Добавил волонтер"  ReadOnly="True" />
                    <asp:BoundField DataField="Status" HeaderText="Статус брони" />
                </Fields>
            </asp:DetailsView>
        </div>

        <div style="margin-left: 20px; display: flex; flex-direction: column; gap: 10px;">
            <asp:DropDownList
                ID="DropDownList_Status" 
                runat="server" 
                AutoPostBack="True"  
                DataTextField="Status" 
                DataValueField="Status" 
                Width="200px"
                Visible="false"
            />

            <asp:Button BackColor="White" ForeColor="Black" ID="SetStatusButton" runat="server" Visible="false" 
                Text="Установить статус" 
                OnClick="SetStatusButton_Click" />
        </div>
    </div>
    <p style="text-align: center; margin-top: 25px;">
        <asp:Label ID="LabelUser" runat="server" Visible="false">Данные о пользователе</asp:Label>
    </p>
    <p>
        <asp:DetailsView ID="UserDetails" runat="server" 
            AutoGenerateRows="False" 
            DataKeyNames="UserId"  
            Height="50px" 
            Width="600px"
            CellPadding="8"
            HorizontalAlign="Center">
            <Fields>
                <asp:BoundField DataField="UserId" HeaderText="UserId" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="Name" HeaderText="Полное имя" ReadOnly="True"  />
                <asp:BoundField DataField="Email" HeaderText="Электронная почта" ReadOnly="True"  />
                <asp:BoundField DataField="Phone" HeaderText="Телефон для связи" ReadOnly="True" />
                <asp:BoundField DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" DataField="BirthDate" HeaderText="Дата рождения" ReadOnly="True"/>
                <asp:BoundField DataField="Address" HeaderText="Адрес" ReadOnly="True" />
                <asp:BoundField DataField="RoleName" HeaderText="Должность сотрудника" ReadOnly="True" />
            </Fields>
        </asp:DetailsView>
    </p>
    <p style="text-align: center; margin-top: 25px;">
        <asp:Button BackColor="White" ForeColor="Black" ID="ExitButton" runat="server" Visible="false" Text="Сбросить" OnClick="ExitButton_Click" />
    </p>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMainPage.Master" AutoEventWireup="true" CodeBehind="PetsControlPage.aspx.cs" Inherits="PawsKindness.PetsControlPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Главная - Управлением карточками питомцев</title>
    <style>
        .styled-add-button {
            background-color: #4CAF50; /* Зеленый цвет кнопки */
            color: white; /* Белый текст */
            border: none; /* Без границ */
            padding: 10px 20px; /* Отступы */
            font-size: 16px; /* Размер текста */
            cursor: pointer; /* Указатель */
            border-radius: 5px; /* Скругленные края */
            transition: 0.3s ease-in-out; /* Анимация при наведении */
        }

        .styled-add-button:hover {
            background-color: #45a049; /* Темнее на hover */
        }

        .form-container {
            text-align: center;
        }

        .form-table {
            margin: 0 auto;
            padding: 10px;
            border-collapse: separate;
            border-spacing: 10px;
        }

        .form-label {
            font-weight: bold;
            text-align: right;
        }

        .form-input {
            width: 330px;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
        }
        .form-input::placeholder {
            color: gainsboro;
            font-size: 14px;
            font-style: italic;
            opacity: 1;
        }

        .form-error {
            color: #FF3300;
            font-size: 12px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4 style="text-align: center;">Питомцы</h4>

    <div style="margin-bottom: 80px; margin-top: 10px;">
        <p style="display: flex; justify-content: center; align-items: center;">
            <asp:Button 
                ID="AddPetButton" 
                runat="server" 
                Text="Добавить карточку питомца" 
                CssClass="styled-add-button" 
                OnClick="AddPetButton_Click"/>
        </p>
        <asp:Panel ID="AddPanel" runat="server" Visible="false">
            <table class="form-table">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="form-label">Кличка</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="NameBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredName" runat="server"
                            ErrorMessage="Поле обязательно"
                            EnableClientScript="False"
                            ControlToValidate="NameBox" CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" CssClass="form-label">Дата рождения</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Date" ID="BirthDayBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredDate" runat="server"
                            ErrorMessage="Поле обязательно"
                            EnableClientScript="False"
                            ControlToValidate="BirthDayBox" CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                            ErrorMessage="Не верный формат даты" 
                            EnableClientScript="False" 
                            ControlToValidate="BirthDayBox" 
                            CssClass="form-error"
                            ValidationExpression="[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" CssClass="form-label">Вес</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="WeightBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Поле обязательно"
                            EnableClientScript="False"
                            ControlToValidate="WeightBox" 
                            CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionWeight" runat="server"
                            ErrorMessage="Не верный формат веса" 
                            EnableClientScript="False" 
                            ControlToValidate="WeightBox" 
                            CssClass="form-error"
                            ValidationExpression="^[0-9]*[.]?[0-9]+$">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="form-label">Выберите породу</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList
                            ID="DropDownListBreedAdd" 
                            runat="server" 
                            AutoPostBack="True"  
                            DataTextField="BreedName" 
                            DataValueField="BreedName" 
                            Width="250px"
                        />   
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="form-label">Выберите волонтера</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList
                            ID="DropDownListVolunteer" 
                            runat="server" 
                            AutoPostBack="True"  
                            DataTextField="UserName" 
                            DataValueField="UserName" 
                            Width="300px"
                        />
                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td colspan="1">
                        <asp:Button style="margin-top: 15px;" BackColor="White" ForeColor="Black" ID="SaveBtn" runat="server" 
                            Text="Сохранить" OnClick="SaveBtn_Click" />

                        <asp:Button style="margin-top: 15px;" BackColor="White" ForeColor="Black" ID="CloseSaveBtn" runat="server" 
                            Text="Скрыть" OnClick="CloseSaveBtn_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <asp:Label Font-Size="16px" ID="ErrorLabel" CssClass="form-error" runat="server"></asp:Label>
        </asp:Panel>
    </div>

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
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PetId" 
            OnSelectedIndexChanged="PetsGridView_SelectedIndexChanged">
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
            AutoGenerateEditButton="true"
            DataKeyNames="PetId"  
            Height="50px" 
            Width="500px"
            CellPadding="8"
            HorizontalAlign="Center"
            OnModeChanging="PetDetails_ModeChanging"
            OnItemUpdating="PetDetails_ItemUpdating"
        >
            <Fields>
                <asp:BoundField DataField="PetId" HeaderText="PetId" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="Name" HeaderText="Кличка"  />
                <asp:TemplateField HeaderText="Порода">
                    <ItemTemplate>
                        <%# Eval("BreedName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="BreedNameDropDown" runat="server" 
                            AutoPostBack="True" 
                            DataValueField="BreedName"
                            DataTextField="BreedName" 
                        >
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Дата рождения">
                    <ItemTemplate>
                        <%# Eval("DateBirth", "{0:dd.MM.yyyy}") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="DateBox" runat="server" 
                            Text='<%# Bind("DateBirth", "{0:dd.MM.yyyy}") %>'/>
                        <asp:RequiredFieldValidator ID="RequiredDate" runat="server"
                            ErrorMessage="Поле обязательно"
                            EnableClientScript="False"
                            ControlToValidate="DateBox" CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Weight" DataFormatString="{0:F2}" HtmlEncode="false" HeaderText="Вес" />
                <asp:TemplateField HeaderText="Добавил волонтер" >
                    <ItemTemplate>
                        <%# Eval("UserName") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="UserDropDown" runat="server" 
                            AutoPostBack="True" 
                            DataValueField="UserName"
                            DataTextField="UserName" 
                        >
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Статус">
                    <ItemTemplate>
                        <%# Eval("StatusName") %> <!-- Отображает текст статуса -->
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="StatusDropDown" runat="server" 
                            AutoPostBack="True" 
                            DataValueField="StatusName"
                            DataTextField="StatusName" 
                        >
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>
    </p>    
    <p style="text-align: center;">
        <asp:Label ID="ErrorLabelPetDet" ForeColor="Red" runat="server"></asp:Label>
    </p>
</asp:Content>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="PawsKindness.UserControl.ProfilePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Профиль пользователя</title>
    <style>
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
    <div class="form-container">
    <h4><p>Данные пользователя</p></h4>
    <table class="form-table">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="form-label">Имя</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="NameBox" runat="server" CssClass="form-input"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredName" runat="server"
                    ErrorMessage="Имя | Фамилию обязательно"
                    EnableClientScript="False"
                    ControlToValidate="NameBox" CssClass="form-error">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" CssClass="form-label">Электронная почта</asp:Label>
            </td>
            <td>
                <asp:TextBox ReadOnly="true" TextMode="Email" ID="EmailBox" runat="server" CssClass="form-input"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="form-label">Мобильный телефон</asp:Label>
            </td>
            <td>
                <asp:TextBox TextMode="Phone" ID="PhoneBox" runat="server" CssClass="form-input"></asp:TextBox>
            </td>
            <td>
                <asp:RegularExpressionValidator ID="RegularExpressionPhone" runat="server"
                    ErrorMessage="Не верный формат мобильного телефона" 
                    EnableClientScript="False" 
                    ControlToValidate="PhoneBox" 
                    CssClass="form-error"
                    ValidationExpression="^((\+7|7|8)+([0-9]){10})$">
                </asp:RegularExpressionValidator>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredPhone" runat="server"
                    ErrorMessage="Телефон обязателен"
                    EnableClientScript="False"
                    ControlToValidate="PhoneBox" CssClass="form-error">
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
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                    ErrorMessage="Не верный формат даты" 
                    EnableClientScript="False" 
                    ControlToValidate="BirthDayBox" 
                    CssClass="form-error"
                    ValidationExpression="[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])">
                </asp:RegularExpressionValidator>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredDate" runat="server"
                    ErrorMessage="Поле обязательно"
                    EnableClientScript="False"
                    ControlToValidate="BirthDayBox" CssClass="form-error">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" CssClass="form-label">Адрес</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="AddressBox" runat="server" CssClass="form-input"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" CssClass="form-label">Роль</asp:Label>
            </td>
            <td>
                <asp:RadioButtonList style="height: 50px; width: 140px;" ID="RoleRadioList" runat="server" CssClass="form-input" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ErrorMessage="Выбор роли обязателен"
                    EnableClientScript="False"
                    ControlToValidate="RoleRadioList"
                    CssClass="form-error">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button style="margin-top: 15px;" BackColor="White" ForeColor="Black" ID="SaveBtn" runat="server" 
                    Text="Сохранить" OnClick="SaveBtn_Click" />
            </td>
        </tr>
    </table>
    <asp:Label Font-Size="16px" ID="ErrorLabel" CssClass="form-error" runat="server"></asp:Label>
</div>
</asp:Content>

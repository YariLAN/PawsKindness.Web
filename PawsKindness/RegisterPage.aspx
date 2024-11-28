<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="PawsKindness.RegisterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Регистрация пользователя</title>
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
    <asp:Panel runat="server" ID="RegisterPanel">
        <div class="form-container">
            <h4><p>Регистрация пользователя</p></h4>
            <table class="form-table">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="form-label">Имя полностью</asp:Label>
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
                        <asp:Label ID="Label2" runat="server" CssClass="form-label">Email</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Email" ID="EmailBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server"
                            ErrorMessage="Почта обязательна"
                            EnableClientScript="False"
                            ControlToValidate="EmailBox" CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RequiredEmail" runat="server"
                            ErrorMessage="Не верный формат почты" 
                            EnableClientScript="False" 
                            ControlToValidate="EmailBox" 
                            CssClass="form-error"
                            ValidationExpression="^\S+@\S+\.\S+$">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="form-label">Телефон</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Phone" ID="PhoneBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ErrorMessage="Не верный формат мобильного телефона" 
                            EnableClientScript="False" 
                            ControlToValidate="PhoneBox" 
                            CssClass="form-error"
                            ValidationExpression="^((\+7|7|8)+([0-9]){10})$">
                        </asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
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
                        <asp:RequiredFieldValidator ID="RequiredBirthDay3" runat="server"
                            ErrorMessage="Дату рождения обязательно"
                            EnableClientScript="False"
                            ControlToValidate="BirthDayBox" CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" CssClass="form-label">Адрес (необязательно)</asp:Label>
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
            </table>
            <h4><p>Данные для входа</p></h4>
            <table  class="form-table">
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" CssClass="form-label">Логин</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="LoginBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredLogin" runat="server"
                            ErrorMessage="Логин обязателен"
                            EnableClientScript="False"
                            ControlToValidate="LoginBox"
                            CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" CssClass="form-label">Пароль</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Password" ID="PasswordBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Пароль обязателен"
                            EnableClientScript="False"
                            ControlToValidate="PasswordBox"
                            CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" CssClass="form-label">Подтверждение пароля</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Password" ID="ConfirmedPassword" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CompareValidator 
                            ID="CompareValidatorPassword" runat="server"
                            ErrorMessage="Не совпадают"
                            EnableClientScript="False"
                            ControlToValidate="PasswordBox"
                            ControlToCompare="ConfirmedPassword"
                            Type="String"
                            Display="dynamic"
                            CssClass="form-error">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button style="margin-top: 15px;" BackColor="White" ForeColor="Black" ID="RegisterBtn" runat="server" 
                            Text="Зарегистрироваться"
                            OnClick="RegisterBtn_Click"/>
                    </td>
                </tr>
            </table>
            <asp:Label Font-Size="16px" ID="ErrorLabel" CssClass="form-error" runat="server"></asp:Label>
        </div>
    </asp:Panel>
</asp:Content>
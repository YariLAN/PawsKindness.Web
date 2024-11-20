using System;
using System.Linq;
using System.Web.UI;

namespace PawsKindness
{
    public enum RoleEnum
    {
        User = 1,
        Volunteer = 2,
    };

    public class RoleMapper
    {
        public static int ToMap(string roleText)
        {
            switch (roleText)
            {
                case RoleConstants.ROLE_USER: return (int)RoleEnum.User;
                case RoleConstants.ROLE_VOLUNTEER: return (int)RoleEnum.Volunteer;
                default: return (int)RoleEnum.User;
            };
        }

        public static string ToMap(RoleEnum roleEnum)
        {
            switch (roleEnum)
            {
                case RoleEnum.User: return RoleConstants.ROLE_USER; 
                case RoleEnum.Volunteer: return RoleConstants.ROLE_VOLUNTEER;
                default: return RoleConstants.ROLE_USER;
            };
        }
    }

    public static class RoleConstants
    {
        public const string ROLE_USER = "Пользователь";
        public const string ROLE_VOLUNTEER = "Волонтер";
    }

    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            NameBox.Attributes.Add("placeholder", "(отчество при наличие)");
            EmailBox.Attributes.Add("placeholder", "Ivanov@mail.ru");
            PhoneBox.Attributes.Add("placeholder", "89000000000");
            AddressBox.Attributes.Add("placeholder", "г. Москва, ул. Есенина, д. 10");

            RoleRadioList.Items.Add(RoleConstants.ROLE_USER);
            RoleRadioList.Items.Add(RoleConstants.ROLE_VOLUNTEER);
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";

            if (!Page.IsValid)
            {
                return;
            }

            var login = LoginBox.Text;
            var email = EmailBox.Text;

            var sqlGetUser = $"SELECT * FROM Users WHERE Login = '{login}' OR Email = '{email}'";
            int? userId = SqlHelper.ExecuteScalar(sqlGetUser);

            if (userId != null)
            {
                ErrorLabel.Text = "Пользователь с таким логином или почтой уже существует";
                return;
            }

            var names = NameBox.Text.Split(' ').ToList();
            var surname = names[0];
            var name = names[1];
            var middleName = names.Count == 2 ? "" : names[2];

            var phone = PhoneBox.Text;
            var birthDate = string.Format(BirthDayBox.Text, "yyyy-MM-dd HH:mm:ss.fff");
            var address = AddressBox.Text ?? "";
            var role = RoleMapper.ToMap(RoleRadioList.Text);

            var password = PasswordBox.Text;

            string insertUser = "INSERT INTO Users " +
                "(Surname, FirstName, MiddleName, Email, Phone, BirthDate, Address, RoleId, Login, Password, InBlackList) " +
             $"VALUES ('{surname}', '{name}', '{middleName}', '{email}', '{phone}', '{birthDate}', '{address}', {role}, '{login}', '{password}', 0)";

            int rowsAffected = SqlHelper.ExecuteNotQuery(insertUser);

            if (rowsAffected > 0)
            {
                Session["LabelMessage"] = "Вы успешно зарегистрировались. Войдите!";

                Response.Redirect("MainPage.aspx");
            }
            else
            {
                ErrorLabel.Text = "Ошибка регистрации. Попробуйте снова";
                return;
            }
        }
    }
}
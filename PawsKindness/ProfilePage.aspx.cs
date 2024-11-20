using PawsKindness.DbModels;
using System;
using System.Linq;

namespace PawsKindness.UserControl
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }
            
            if (Session["UserId"] is null)
                return;

            var id = (int)Session["UserId"];
            string getUser = $"SELECT * FROM Users WHERE UserId = '{id}'";

            User user = new User();

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(getUser);
                user = UserMapper.ToMap(reader);
            });

            if (user is null) return;

            NameBox.Text = string.IsNullOrEmpty(user.MiddleName)
                ? string.Concat(user.Surname, " ", user.FirstName)
                : string.Concat(user.Surname, " ", user.FirstName, " ", user.MiddleName);
            EmailBox.Text = user.Email;
            PhoneBox.Text = user.Phone;
            BirthDayBox.Text = user.BirthDate.ToString("yyyy-MM-dd");
            AddressBox.Text = user.Address;

            if (RoleRadioList.Items.Count <= 0)
            {
                RoleRadioList.Items.Add(RoleConstants.ROLE_USER);
                RoleRadioList.Items.Add(RoleConstants.ROLE_VOLUNTEER);
            }
            RoleRadioList.SelectedIndex = (int)user.Role - 1;    
        }

        private string UPD_USER = @"
                UPDATE Users 
                SET Surname = '{0}', FirstName = '{1}', MiddleName='{2}', 
                    Phone='{3}', BirthDate='{4}', Address='{5}', RoleId={6}
                WHERE UserId = {7};";

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
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

            var id = Session["UserId"].ToString();
            var updUser = string.Format(UPD_USER, surname, name, middleName, phone, birthDate, address, role, id);

            int rowsAffected = SqlHelper.ExecuteNotQuery(updUser);

            if (rowsAffected > 0)
            {
                ErrorLabel.Text = "Вы успешно обновили даннные";
            }
            else
            {
                ErrorLabel.Text = "Ошибка сохранения. Попробуйте снова";
                return;
            }
        }
    }
}
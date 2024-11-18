using PawsKindness.DbModels;
using System;

namespace PawsKindness.UserControl
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            NameBox.Text = string.Concat(user.Surname, " ", user.FirstName, " ", user.MiddleName);
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

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
        }
    }
}
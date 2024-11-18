using PawsKindness.DbModels;
using System;
using System.Data.SqlClient;

namespace PawsKindness
{
    public partial class MainPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LabelMessage"] != null)
            {
                LabelMessage.Text = Session["LabelMessage"].ToString();
                Session.Remove("LabelMessage");
            }

            if (Session["UserId"] != null)
            {
                DynamicNameLabel.Text = $"Hi, {Session["UserName"].ToString()}!";
                LoginLabel.Visible = false;
                PasswordLabel.Visible = false;

                TextBox1.Visible = false;
                TextBox2.Visible = false;
                RequiredLog.Enabled = false;
                RequiredPassWord.Enabled = false;
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            var login = TextBox1.Text;
            var password = TextBox2.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return;

            var getUser = $"SELECT * FROM Users WHERE Login = '{login}' OR Email = '{login}'";

            var id = SqlHelper.ExecuteScalar(getUser);

            if (id is null)
            {
                LabelMessage.Text = "Неверный логин";
                return;
            }

            User user = new User();

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(getUser);
                user = UserMapper.ToMap(reader);
            });

            if (user.Password != password)
            {
                LabelMessage.Text = "Неверный пароль";
                return;
            }

            Session["UserId"] = id.Value;
            Session["UserName"] = user.FirstName;

            DynamicNameLabel.Text = $"Hi, {Session["UserName"].ToString()}!";
            LoginLabel.Visible = false;
            PasswordLabel.Visible = false;

            TextBox1.Visible = false;
            TextBox2.Visible = false;
            RequiredLog.Enabled = false;
            RequiredPassWord.Enabled = false;
        }
        protected void ExitBtn_Click(object sender, EventArgs e)
        {
            Session.Remove("UserId");
            Session.Remove("UserName");

            TextBox1.Visible = true;
            TextBox2.Visible = true;
            LoginLabel.Visible = true;
            PasswordLabel.Visible = true;

            LabelMessage.Text = "";

            Response.Redirect("MainPage.aspx");
        } 

        protected void ProfileBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
            {
                return;
            }

            Response.Redirect("ProfilePage.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("PetsPage.aspx");
        }  

        
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }  
        
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeesPage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPage.aspx");
        }
    }
}

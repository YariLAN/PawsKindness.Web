using System;

namespace PawsKindness
{
    public partial class AdminMainPage : System.Web.UI.MasterPage
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
                AdminLabel.Text = $"Hi, {Session["UserName"]} (админ)";
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("PetsControlPage.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPage.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeesPage.aspx");
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            AdminLabel.Text = "";

            Session.Remove("UserId");
            Session.Remove("UserName");

            Response.Redirect("MainPage.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdoptedControlPage.aspx");
        }
    }
}
using System;

namespace PawsKindness
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack || Session["UserId"] != null) return;
        }
    }
}
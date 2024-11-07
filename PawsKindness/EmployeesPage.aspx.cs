using System;
using System.Web.UI.WebControls;

namespace PawsKindness
{
    public partial class EmployeesPage : System.Web.UI.Page
    {
        private const string SQL_EMPLOYEES_ALL = @"
            SELECT
                u.UserId,
	            CONCAT(u.Surname, ' ', u.FirstName, ' ', u.MiddleName) as Name,
	            u.Email,
	            u.Phone,
	            u.BirthDate,
	            Roles.Name as RoleName
            FROM Users u
            Inner join Roles on u.RoleId = Roles.RoleId
            WHERE (u.RoleId = 2 OR u.RoleId = 3) AND u.InBlackList != 1;";

        private const string SQL_EMPLOYEE_BY_ID = @"
            SELECT
	            u.UserId,
	            CONCAT(u.Surname, ' ', u.FirstName, ' ', u.MiddleName) as Name,
	            u.Email,
	            u.Phone,
	            u.BirthDate,
	            u.Address,
	            Roles.Name as RoleName
            FROM Users u
            Inner join Roles on u.RoleId = Roles.RoleId
            WHERE (u.RoleId = 2 OR u.RoleId = 3) AND u.InBlackList != 1 AND u.UserId = {0};";

        private const string SQL_ROLES_SELECT = "SELECT Name as RoleName FROM Roles WHERE RoleId != 1;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(SQL_EMPLOYEES_ALL);
                EmployeesGridView.DataSource = reader;
                EmployeesGridView.DataBind();
            });

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(SQL_ROLES_SELECT);
                DropDownList_Role.DataSource = reader;
                DropDownList_Role.DataBind();
            });
        }

        protected void SearchEmp(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            var value = box.Text;

            string sqlSearchEmpByFullName = SQL_EMPLOYEES_ALL.Substring(0, SQL_EMPLOYEES_ALL.Length - 1) +
                $" AND CONCAT(u.Surname, ' ', u.FirstName, ' ', u.MiddleName) LIKE '%' + '{value}' + '%';";

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlSearchEmpByFullName);
                EmployeesGridView.DataSource = reader;
                EmployeesGridView.DataBind();
            });
        }

        protected void EmployeesGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (GridView)sender;
            var value = (int)list.SelectedValue;

            string sqlEmployeeById = string.Format(SQL_EMPLOYEE_BY_ID, value);

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlEmployeeById);
                EmpDetails.DataSource = reader;
                EmpDetails.DataBind();
            });
        }   

        protected void DropDownList_RoleIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            string value = (string)list.SelectedValue;

            string sqlCmd = SQL_EMPLOYEES_ALL.Substring(0, SQL_EMPLOYEES_ALL.Length - 1) + $" AND (Roles.Name = '{value}');";

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlCmd);
                EmployeesGridView.DataSource = reader;
                EmployeesGridView.DataBind();
            });

        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(SQL_EMPLOYEES_ALL);
                EmployeesGridView.DataSource = reader;
                EmployeesGridView.DataBind();
            });

            TextBox_FullName.Text = "";

            EmpDetails.DataSource = "";
            EmpDetails.DataBind();
        }    
    }
}
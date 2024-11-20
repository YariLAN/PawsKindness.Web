using System;
using System.Web.UI.WebControls;

namespace PawsKindness
{
    public partial class PetsPage : System.Web.UI.Page
    {
        private const string SQL_SELECT_ALL = @"
            SELECT 
              Pets.PetId,
              Pets.Name,
              br.Name as BreedName,
              sp.Name as SpeciesName,
              Pets.DateBirth,
              Pets.Weight,
              CONCAT(u.Surname, ' ', u.FirstName, ' ', u.MiddleName) as UserName,
              st.Name as StatusName
            FROM Pets
            Inner join users u on u.UserId = Pets.UserId 
            Inner join Breeds br on br.BreedId = Pets.BreedId
            Inner join Species sp on br.SpeciesId = sp.SpeciesId
            Inner join Status st on st.StatusId = Pets.StatusId;";

        private const string SQL_BREED_OPTIONS = "SELECT br.Name as BreedName FROM Breeds br;";
        private const string SQL_STATUS_OPTIONS = "SELECT st.Name as StatusName FROM Status st;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            SqlHelper.CompleteConnect(() =>
            {
                string sqlPetsByStatus = 
                    SQL_SELECT_ALL.Substring(0, SQL_SELECT_ALL.Length - 1) + $" WHERE (st.StatusId = 1);";

                var reader = SqlHelper.CompleteCommand(sqlPetsByStatus);
                PetsGridView.DataSource = reader;
                PetsGridView.DataBind();
            });

            SqlHelper.CompleteConnect(() =>
            {
                var readerDropDown = SqlHelper.CompleteCommand(SQL_BREED_OPTIONS);
                DropDownList_Breed.DataSource = readerDropDown;
                DropDownList_Breed.DataBind();
            });     

            SqlHelper.CompleteConnect(() =>
            {
                var readerDropDown = SqlHelper.CompleteCommand(SQL_STATUS_OPTIONS);
                DropDownList_Status.DataSource = readerDropDown;
                DropDownList_Status.DataBind();
            });

            ErrorLabelPetDet.Text = "";
        }

        protected void PetsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (GridView)sender;
            var value = (int)list.SelectedValue;
            
            string sqlPetById = SQL_SELECT_ALL.Substring(0, SQL_SELECT_ALL.Length - 1) + $" WHERE (PetId = {value});";

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlPetById);
                PetDetails.DataSource = reader;
                PetDetails.DataBind();
            });

            var id = Session["UserId"];

            AdoptPet.Visible = (id != null && DropDownList_Status.SelectedValue == "В поиске жилья");
        }

        protected void Adopted_Click(object sender, EventArgs e)
        {
            var userId = Session["UserId"];

            if (userId is null)
            {
                ErrorLabelPetDet.Text = "Вы не авторизованы!";
                return;
            }

            var petId = (int)PetDetails.SelectedValue;

            var updt = $"UPDATE Pets SET StatusId = 3 WHERE PetId = {petId}";
            var resultUpdate = SqlHelper.ExecuteNotQuery(updt);
            if (resultUpdate <= 0)
            {
                ErrorLabelPetDet.Text = "Ошибка. Попробуйте снова";
                return;
            }
            
            var bookingdate = string.Format(DateTime.Now.ToString(), "yyyy-MM-dd HH:mm:ss.fff");

            var insertAdopted = $"INSERT INTO Adopted (BookingDate, StateAdoptedId, PetId, UserId)" +
                $" VALUES ('{bookingdate}', 1, {petId}, {userId});";

            var resultInsert = SqlHelper.ExecuteNotQuery(insertAdopted);
            if (resultInsert > 0)
            {
                Session["LabelMessage"] = "Вы успешно создали заявку!";

                Response.Redirect("MyBookingPage.aspx");
            }
            else
            {
                ErrorLabelPetDet.Text = "Ошибка создания заявки. Попробуйте заново";
                return;
            }
        }

        protected void Breed_Click(object sender, EventArgs e)
        {
            var value = DropDownList_Breed.SelectedValue;
            string sqlPetsByBreed = SQL_SELECT_ALL.Substring(0, SQL_SELECT_ALL.Length - 1) + $" WHERE (br.Name = '{value}');";

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlPetsByBreed);
                PetsGridView.DataSource = reader;
                PetsGridView.DataBind();
            });
        }    

        protected void Status_Click(object sender, EventArgs e)
        {
            var value = DropDownList_Status.SelectedValue;
            string sqlPetsByStatus = SQL_SELECT_ALL.Substring(0, SQL_SELECT_ALL.Length - 1) + $" WHERE (st.Name = '{value}');";

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlPetsByStatus);
                PetsGridView.DataSource = reader;
                PetsGridView.DataBind();
            });
        }   

        protected void BreedStatus_Click(object sender, EventArgs e)
        {
            var valueBr = DropDownList_Breed.SelectedValue;
            var valueSt = DropDownList_Status.SelectedValue;
            string sqlPetsByStatusBreed = SQL_SELECT_ALL.Substring(0, SQL_SELECT_ALL.Length - 1) 
                + $" WHERE (st.Name = '{valueSt}') AND (br.Name = '{valueBr}');";

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlPetsByStatusBreed);
                PetsGridView.DataSource = reader;
                PetsGridView.DataBind();
            });
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(SQL_SELECT_ALL);
                PetsGridView.DataSource = reader;
                PetsGridView.DataBind();
            });

            PetDetails.DataSource = "";
            PetDetails.DataBind();
        }
    }
}

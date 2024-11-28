using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PawsKindness
{
    public partial class AdoptedControlPage : System.Web.UI.Page
    {
        private string SQL_BOOKINGS = @"
            SELECT 
	            Adopted.AdoptedId,
	            Adopted.BookingDate,
	            Pets.Name as Name,
	            Breeds.Name as BreedName,
	            Species.Name as SpeciesName,
	            Pets.DateBirth,
	            StateAdopted.Name as Status,
	            Adopted.DateAdoption
            FROM Adopted
            Join StateAdopted on Adopted.StateAdoptedId = StateAdopted.StateAdoptedId
            Inner Join Pets on Adopted.PetId = Pets.PetId
            Join Breeds on Pets.BreedId = Breeds.BreedId
            Join Species on Breeds.SpeciesId = Species.SpeciesId";

        private string SQL_ADOPT_DETAILS = @"
            SELECT 
                Adopted.AdoptedId,
                Adopted.BookingDate,
                Adopted.DateAdoption,
                Pets.Name as Name,
                Breeds.Name as BreedName,
                Species.Name as SpeciesName,
                Pets.DateBirth,
	            Pets.Weight,
	            CONCAT(us.Surname,' ', us.FirstName,' ',us.MiddleName) as UserName,
                StateAdopted.Name as Status
            FROM Adopted
            Join StateAdopted on Adopted.StateAdoptedId = StateAdopted.StateAdoptedId
            Inner Join Pets on Adopted.PetId = Pets.PetId
            Inner JOIN Users us on Pets.UserId = us.UserId
            Join Breeds on Pets.BreedId = Breeds.BreedId
            Join Species on Breeds.SpeciesId = Species.SpeciesId WHERE Adopted.AdoptedId = '{0}';
        ";

        private string SQL_USER_DETAILS = @"
            SELECT
                u.UserId,
                CONCAT(u.Surname, ' ', u.FirstName, ' ', u.MiddleName) as Name,
                u.Email,
                u.Phone,
                u.BirthDate,
                u.Address,
                Roles.Name as RoleName
            FROM Adopted a
            Inner join Users u on u.UserId = a.UserId 
            Inner join Roles on u.RoleId = Roles.RoleId
            WHERE (u.RoleId = 1 OR u.RoleId = 2) AND a.AdoptedId = {0};
        ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(SQL_BOOKINGS);
                AdminAdoptedGridView.DataSource = reader;
                AdminAdoptedGridView.DataBind();
            });

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand("SELECT Name as Status FROM StateAdopted WHERE StateAdoptedId != 1;");
                DropDownList_Status.DataSource = reader;
                DropDownList_Status.DataBind();
            });
        }

        protected void AdminAdoptedGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (GridView)sender;
            var value = (int)list.SelectedValue;

            var sqlGetAdoptById = string.Format(SQL_ADOPT_DETAILS, value);

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlGetAdoptById);
                AdoptDetails.DataSource = reader;
                AdoptDetails.DataBind();
            });

            //var id = Session["UserId"];
            //AdoptPet.Visible = (id != null && DropDownList_Status.SelectedValue == "В поиске жилья");

            var sqlGetUserDetailsByAdoptId = string.Format(SQL_USER_DETAILS, value);

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlGetUserDetailsByAdoptId);
                UserDetails.DataSource = reader;
                UserDetails.DataBind();
            });             
            
            LabelUser.Visible = true;
            ExitButton.Visible = true;

            DropDownList_Status.Visible = true;
            SetStatusButton.Visible = true;
        }

        protected void ExitButton_Click(object sender, EventArgs e)
        {
            AdoptDetails.DataSource = "";
            AdoptDetails.DataBind();

            UserDetails.DataSource = "";
            UserDetails.DataBind();

            LabelUser.Visible = false;
            ExitButton.Visible = false;

            DropDownList_Status.Visible = false;
            SetStatusButton.Visible = false;
        }

        private string SQL_GET_PET_ID = @"
            SELECT 
	            *
            FROM Pets
            JOIN Adopted On Pets.PetId = Adopted.PetId
            WHERE Adopted.AdoptedId = {0};
        ";
        protected void SetStatusButton_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            var value = DropDownList_Status.SelectedValue;

            var getStatusId = SqlHelper.ExecuteScalar($"SELECT * FROM StateAdopted WHERE Name = '{value}'");

            if (getStatusId is null)
                return;

            var adoptId = Convert.ToInt32(AdoptDetails.DataKey.Value);
            var userId = Convert.ToInt32(UserDetails.DataKey.Value);

            var updAdopt = $"UPDATE Adopted SET StateAdoptedId = {getStatusId} WHERE AdoptedId = {adoptId};";

            var rowAdoptedUpd = SqlHelper.ExecuteNotQuery(updAdopt);

            if (rowAdoptedUpd <= 0)
                return;

            var setPetStatus = "";
            int rowPetsUpd = 0;
            var petId = SqlHelper.ExecuteScalar(string.Format(SQL_GET_PET_ID, adoptId));
            
            switch (value)
            {
                case StateAdoptedConstants.STATE_FOR_REVIEW:
                    break;

                case StateAdoptedConstants.STATE_ADOPTION:
                    setPetStatus = $"UPDATE Pets SET StatusId = 4 WHERE Pets.PetId = {petId};";

                    var dateAdoption = string.Format(DateTime.Now.ToString(), "yyyy-MM-dd HH:mm:ss.fff");
                    var updAdoptin = $"UPDATE Adopted SET DateAdoption = '{dateAdoption}' WHERE AdoptedId = {adoptId};";

                    rowPetsUpd = SqlHelper.ExecuteNotQuery(setPetStatus);

                    if (rowPetsUpd <= 0)
                        return;

                    var rowAdoptionUpd = SqlHelper.ExecuteNotQuery(updAdoptin);

                    if (rowAdoptionUpd <= 0)
                        return;

                    break;      
                    
                case StateAdoptedConstants.STATE_CANCELED:
                    setPetStatus = $"UPDATE Pets SET StatusId = 1 WHERE Pets.PetId = {petId};";
                    rowPetsUpd = SqlHelper.ExecuteNotQuery(setPetStatus);
                    break;
            }

            Response.Redirect(Request.RawUrl);
        }
    }

    public static class StateAdoptedConstants
    {
        public const string STATE_FOR_REVIEW = "Ожидание";
        public const string STATE_ADOPTION = "Усыновление";
        public const string STATE_CANCELED = "Отмена";
    }

}
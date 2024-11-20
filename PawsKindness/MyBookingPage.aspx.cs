using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PawsKindness
{
    public partial class MyBookingPage : System.Web.UI.Page
    {
        private string SQL_BOOKING_BY_User = @"
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
            Join Species on Breeds.SpeciesId = Species.SpeciesId WHERE Adopted.UserId = {0};";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            var id = Session["UserId"];

            if (id is null)
            {
                return;
            }

            var userBookingPets = string.Format(SQL_BOOKING_BY_User, id);

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(userBookingPets);
                AdoptedGridView.DataSource = reader;
                AdoptedGridView.DataBind();
            });
        }
    }
}
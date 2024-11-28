using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PawsKindness
{
    public partial class PetsControlPage : System.Web.UI.Page
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

        private const string SQL_BREED_OPTIONS_SPECIES = @"
            SELECT 
                CONCAT(br.Name, ' | ', Species.Name) as BreedName 
            FROM Breeds br 
            Inner JOIN Species on br.SpeciesId = Species.SpeciesId;";

        private const string SQL_STATUS_OPTIONS = "SELECT st.Name as StatusName FROM Status st;";

        private const string SQL_VOLUNTEERS = @"
            SELECT 
	            CONCAT(us.Surname, ' ', us.FirstName, ' ', us.MiddleName) as UserName 
            FROM Users us
            WHERE us.RoleId = 2 AND us.InBlackList != 1;
        ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(SQL_SELECT_ALL);
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

            Session["PetDetailsId"] = value;

            string sqlPetById = SQL_SELECT_ALL.Substring(0, SQL_SELECT_ALL.Length - 1) + $" WHERE (PetId = {value});";

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlPetById);
                PetDetails.DataSource = reader;
                PetDetails.DataBind();
            });

            PetDetailsEdit_UpdateDropDownStatus();
        }

        protected string GetSelectedValueFromDropDown(string dropDownName)
        {
            DropDownList dropDown = (DropDownList)PetDetails.FindControl(dropDownName);

            return dropDown.SelectedValue;
        }

        protected void PetDetailsEdit_UpdateDropDownStatus()
        {
            if (PetDetails.CurrentMode == DetailsViewMode.Edit)
            {
                DropDownList ddListStatus = (DropDownList)PetDetails.FindControl("StatusDropDown");
                DropDownList ddListUser = (DropDownList)PetDetails.FindControl("UserDropDown");
                DropDownList ddListBreed = (DropDownList)PetDetails.FindControl("BreedNameDropDown");

                if (ddListStatus != null)
                {
                    SqlHelper.CompleteConnect(() =>
                    {
                        var readerDropDown = SqlHelper.CompleteCommand(SQL_STATUS_OPTIONS);
                        ddListStatus.DataSource = readerDropDown;
                        ddListStatus.DataBind();
                    });   
                    
                    SqlHelper.CompleteConnect(() =>
                    {
                        var readerDropDown = SqlHelper.CompleteCommand(SQL_VOLUNTEERS);
                        ddListUser.DataSource = readerDropDown;
                        ddListUser.DataBind();
                    });    
                    
                    SqlHelper.CompleteConnect(() =>
                    {
                        var readerDropDown = SqlHelper.CompleteCommand(SQL_BREED_OPTIONS_SPECIES);
                        ddListBreed.DataSource = readerDropDown;
                        ddListBreed.DataBind();
                    });

                    ddListStatus.SelectedValue = DataBinder.Eval(PetDetails.DataItem, "StatusName")?.ToString();
                    ddListUser.SelectedValue = DataBinder.Eval(PetDetails.DataItem, "UserName")?.ToString();
                    ddListBreed.SelectedValue = DataBinder.Eval(PetDetails.DataItem, "BreedName")?.ToString();
                }
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

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid || Session["UserId"] is null)
            {
                return;
            }

            var name = NameBox.Text;
            var birthDate = string.Format(BirthDayBox.Text, "yyyy-MM-dd HH:mm:ss.fff");
            var weight = WeightBox.Text;

            var breedName = DropDownListBreedAdd.SelectedValue.Split(' ', '|').ToList()[0];
            var names = DropDownListVolunteer.SelectedValue.Split(' ').ToList();
            var surname = names[0];
            var firstName = names[1];
            var middleName = (string.IsNullOrEmpty(names[2])) ? " IS NULL" : $"= '{names[2]}'";

            var getIdVolunteer = $"SELECT * FROM Users WHERE " +
                $"Surname='{surname}' " +
                $"AND FirstName='{firstName}' " +
                $"AND MiddleName{middleName};";

            var userId = SqlHelper.ExecuteScalar(getIdVolunteer);

            var breedId = SqlHelper.ExecuteScalar($"SELECT * FROM Breeds WHERE Name='{breedName}';");

            if (userId == null || breedId == null)
            {
                ErrorLabel.Text = "Такого волонтера или породы не существует";
                return;
            }

            var insertPet = "INSERT INTO Pets (Name, DateBirth, Weight, PathPhoto, UserId, BreedId, StatusId) " +
                $"VALUES ('{name}', '{birthDate}', {weight}, NULL, {userId}, {breedId}, 1);";

            var rowsAdded = SqlHelper.ExecuteNotQuery(insertPet);

            if (rowsAdded > 0)
            {
                Response.Redirect(Request.RawUrl);
                return;
            }
            else
            {
                ErrorLabel.Text = "Ошибка добавления. Попробуйте снова";
                return;
            }
        }

        protected void CloseSaveBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            AddPanel.Visible = false;
        }

        protected void AddPetButton_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            WeightBox.Attributes.Add("placeholder", "10.0");
            AddPanel.Visible = true;

            SqlHelper.CompleteConnect(() =>
            {
                var readerDropDown = SqlHelper.CompleteCommand(SQL_BREED_OPTIONS_SPECIES);
                DropDownListBreedAdd.DataSource = readerDropDown;
                DropDownListBreedAdd.DataBind();
            });  

            SqlHelper.CompleteConnect(() =>
            {
                var readerDropDown = SqlHelper.CompleteCommand(SQL_VOLUNTEERS);
                DropDownListVolunteer.DataSource = readerDropDown;
                DropDownListVolunteer.DataBind();
            });
        }

        protected void PetDetails_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            PetDetails.ChangeMode(e.NewMode);

            var value = (int)Session["PetDetailsId"];

            string sqlPetById = SQL_SELECT_ALL.Substring(0, SQL_SELECT_ALL.Length - 1) + $" WHERE (PetId = {value});";

            SqlHelper.CompleteConnect(() =>
            {
                var reader = SqlHelper.CompleteCommand(sqlPetById);
                PetDetails.DataSource = reader;
                PetDetails.DataBind();
            });

            PetDetailsEdit_UpdateDropDownStatus();
        }

        protected void PetDetails_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            int petId = Convert.ToInt32(PetDetails.DataKey.Value);

            string name = e.NewValues["Name"].ToString();
            string weight = e.NewValues["Weight"].ToString().Replace(",", ".");
            string dateBirth = string.Format(e.NewValues["DateBirth"].ToString(), "yyyy-MM-dd HH:mm:ss.fff");

            string breedName = GetSelectedValueFromDropDown("BreedNameDropDown").Split(' ', '|').ToList()[0];
            var status = GetSelectedValueFromDropDown("StatusDropDown");
            var names = GetSelectedValueFromDropDown("UserDropDown").Split(' ').ToList();

            var surname = names[0];
            var firstName = names[1];
            var middleName = (string.IsNullOrEmpty(names[2])) ? " IS NULL" : $"= '{names[2]}'";

            var getIdVolunteer = $"SELECT * FROM Users WHERE " +
                $"Surname='{surname}' " +
                $"AND FirstName='{firstName}' " +
                $"AND MiddleName{middleName};";

            var userId = SqlHelper.ExecuteScalar(getIdVolunteer);

            var breedId = SqlHelper.ExecuteScalar($"SELECT * FROM Breeds WHERE Name='{breedName}';");

            var statusId = SqlHelper.ExecuteScalar($"Select * FROM Status WHERE Name = '{status}';");

            if (userId == null || breedId == null || statusId is null)
            {
                ErrorLabel.Text = "Такого волонтера, породы или статуса не существует";
                return;
            }

            var updPet = $"UPDATE Pets " +
                $"SET Name = '{name}', DateBirth = '{dateBirth}', Weight = {weight}, UserId = {userId}, BreedId = {breedId}, StatusId = {statusId} " +
                $"WHERE PetId = '{petId}';";

            var rowsUpd = SqlHelper.ExecuteNotQuery(updPet);

            if (rowsUpd > 0)
            {
                Session["LabelMessage"] = $"Данные питомца с кличкой {name} обновлены";
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                ErrorLabel.Text = "Ошибка добавления. Попробуйте снова";
                return;
            }
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
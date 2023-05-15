using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static PetShop.Pages.Pets.viewModel;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace PetShop.Pages.Pets
{
    public class createModel : PageModel
    {
        public PetInfo info = new PetInfo();

        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            info.PetCategory = Request.Form["category"];
            info.PetAge = Request.Form["age"];
            info.PetPrice = Request.Form["price"];
            info.PetPhoto = Request.Form["photo"];


            if ((info.PetCategory).Length == 0 || (info.PetAge).Length == 0 || (info.PetPrice).Length == 0|| (info.PetPhoto).Length == 0)
            {
                errorMessage = "All Fields required";
                return;
            }
            try
            {

                String conString = "Data Source=BLAISEM\\BLAISE;Initial Catalog=PetShop;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String query = "INSERT INTO PetInfo(Pet_Category,Pet_Age,Pet_Price,Pet_Photo) VALUES(@category,@age,@price,@photo)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@category", info.PetCategory);
                        cmd.Parameters.AddWithValue("@age", info.PetAge);
                        cmd.Parameters.AddWithValue("@price", info.PetPrice);
                        cmd.Parameters.AddWithValue("@photo", info.PetPhoto);


                        cmd.ExecuteNonQuery();




                    }

                }


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }
            info.PetCategory = "";
            info.PetAge = "";
           info.PetPrice = "";
            

            successMessage = "New Pet added successfully";
            Response.Redirect("/Pets/view");
        }
    }
}
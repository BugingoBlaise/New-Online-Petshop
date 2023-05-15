using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static PetShop.Pages.Pets.viewModel;

namespace PetShop.Pages.Pets
{
    public class updateModel : PageModel
    {
        public PetInfo info = new PetInfo();
        
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"]; 
            try
            {
                info.PetId = Request.Form["id"];

                String conString = "Data Source=BLAISEM\\BLAISE;Initial Catalog=PetShop;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM PetInfo WHERE PetId=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@id",id);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                           while (reader.Read())
                            {

                                PetInfo Info = new PetInfo();

                                Info.PetId = "" + reader.GetInt32(0);
                                Info.PetCategory = reader.GetString(1);
                                Info.PetAge = "" + reader.GetInt32(2);
                                Info.PetPrice = "" + reader.GetInt32(3);
                                Info.PetPhoto = "" + reader.GetString(4);


                               
                            }



                        }

                    }


                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message.ToString());
            }


        }
        public void OnPost() {

            String id = Request.Query["id"];
// info.PetId = Request.Form["id"];
            info.PetCategory = Request.Form["category"];
            info.PetAge = Request.Form["age"];
            info.PetPrice = Request.Form["price"];
            info.PetPhoto = Request.Form["photo"];

            if (info.PetCategory.Length==0 || info.PetAge.Length==0 || info.PetPrice.Length==0) {
                errorMessage = "All fields required";
                return;
            }

            try
            {
                String conString = "Data Source=SHEDAV\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
       string sqlQuery = "UPDATE Pet set Pet_Category=@category,Pet_Age=@age,Pet_Price=@price,Pet_Photo=@photo WHERE Pet_Id=@id";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {

                        cmd.Parameters.AddWithValue("@category", info.PetCategory);
                        cmd.Parameters.AddWithValue("@age", info.PetAge);
                        cmd.Parameters.AddWithValue("@price", info.PetPrice);
                        cmd.Parameters.AddWithValue("@photo", info.PetPhoto);
                        cmd.Parameters.AddWithValue("@id", id);
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
            info.PetPhoto = "";

            successMessage = "New Pet Updated successfully";
            Response.Redirect("/Pets/view");

        }
    }
}

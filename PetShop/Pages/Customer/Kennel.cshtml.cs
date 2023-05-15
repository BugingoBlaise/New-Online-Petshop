using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace PetShop.Pages.Customer
{
    public class KennelModel : PageModel
    {
        public List<PetInfo> petsList = new List<PetInfo>();
        public void OnGet()
        {
            petsList.Clear();
            try
            {

                String conString = "Data Source=SHEDAV\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Pet";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
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


                                petsList.Add(Info);
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

        public class PetInfo
        {

            public String PetId;
            public String PetCategory;
            public String PetAge;
            public String PetPrice;
            public String PetPhoto;


        }
    }
}

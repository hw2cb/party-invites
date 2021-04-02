using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyInvites.Models;
using System.Data;
using System.Data.SqlClient;
namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Доброе утро" : "Доброго дня";
            return View();
        }

        public ViewResult ViewGuests()
        {
            List<GuestResponse> guests = new List<GuestResponse>();
            string str = System.Configuration.ConfigurationManager.ConnectionStrings["myDataBase"].ConnectionString;
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand com = new SqlCommand("SELECT *FROM Guests", con);
                con.Open();
                SqlDataReader r = com.ExecuteReader();
                while(r.Read())
                {
                    GuestResponse g = new GuestResponse();
                    g.Name = r["Name"].ToString();
                    g.Email = r["Email"].ToString();
                    g.Phone = r["Phone"].ToString();
                    g.WillAttend = (bool)r["WillAttend"];
                    guests.Add(g);
                }

            }
            ViewBag.item = guests;
            return View();
        }



        [HttpGet]
        public ViewResult RsvpForm() 
        {
            return View();
        }
        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guest)
        {
            if(ModelState.IsValid)
            {
                string str = System.Configuration.ConfigurationManager.ConnectionStrings["myDataBase"].ConnectionString;
                using (SqlConnection con = new SqlConnection(str))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Guests (Name, Email, Phone, WillAttend) VALUES(@name, @email, @phone, @willattend)", con);
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = guest.Name;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = guest.Email;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = guest.Phone;
                    cmd.Parameters.Add("@willattend", SqlDbType.Bit).Value = guest.WillAttend;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        View("Ошибка");
                    }
                }
                return View("Thanks", guest);
            }
            else
            {
                return View();
            }
            
        }
    }
}
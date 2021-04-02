using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication_service
{
    /// <summary>
    /// Сводное описание для WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Привет всем!";
        }
        [WebMethod]
        public DataSet getGuests()
        {
            SqlConnection conn;
            SqlDataAdapter adapter;
            DataSet dataset;
            string cmdStr = "SELECT * FROM Guests";
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["myDataBase"].ConnectionString;
            conn = new SqlConnection(connStr);
            adapter = new SqlDataAdapter(cmdStr, conn);
            dataset = new DataSet();
            adapter.Fill(dataset, "Guests");
            return dataset;
        }
    }
}

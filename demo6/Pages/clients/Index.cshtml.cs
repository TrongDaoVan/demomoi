using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace demo6.Pages.clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-OVPEISB\\SQLEXPRESS;Initial Catalog=Mystore;User ID=sa;Password=***********";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    // lệnh trên cho phép thực hiện truy vấn
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        // lệnh trên lấy trình đọc dữ liệu
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();
                                // lệnh trên dùng vòng lặp để in ra danh sách.

                                listClients.Add(clientInfo);
                                Console.WriteLine(clientInfo.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex ) 
            {
                Console.WriteLine("Exception: " + ex.ToString());
            } 
        }
    }

    // dưới là xây dựng class chứ dữ liệu bên db
    public class ClientInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string created_at { get; set; }
    }
}

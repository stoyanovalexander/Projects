using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _05.RecieveImageFromDB
{
    class Program
    {
        static void Main()
        {
            // ПРЕДИ ПРОВЕРКА ВИЖ КАК Е НАПИСАНА ПРИ ТЕБ БАЗАТА ДАННИ ПРИ МЕН Е NORTHWND (без I), Промени го 
            // в стринга отдолу според както е при теб(пиша това защото на мен ми се случи да се мотая заради това)
            SqlConnection dbCon = new SqlConnection("Server=.; " +
                                "Database=NORTHWND; Integrated Security=true");
            dbCon.Open();
            using (dbCon)
            {
                SqlCommand cmdSelect = new SqlCommand(
                    "SELECT Picture FROM Categories", dbCon);
                SqlDataReader reader = cmdSelect.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        byte[] image = (byte[])reader["Picture"];
                        string file = Convert.ToString(DateTime.Now.ToFileTime());
                        FileStream writer = new FileStream(file, FileMode.CreateNew, FileAccess.Write);
                        writer.Write(image, 0, image.Length);
                        writer.Flush();
                        writer.Close();
                        //pictureBox1.Image = Image.FromFile(file);
                    }
                }
            }
        }
    }
}

using Shared.BP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWS.Shared.Model;

namespace XWS.Shared.BP
{
    public class ObracunskiRacunDB
    {

        public static ObracunskiRacun GetObracunskiRacunByRacun(Int64 racun)
        {
            ObracunskiRacun obracunskiRacun = null;

            using (SqlConnection conn = MySQLUtils.NapraviCBConn())
            {
                conn.Open();
                string sql = "SELECT * FROM obracunskiracun WHERE brojobracunskogracuna = @racun";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@racun", racun);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        obracunskiRacun = new ObracunskiRacun();
                        obracunskiRacun.BrojObracunskogRacuna = (Int64)(decimal)reader["brojobracunskogracuna"];
                        obracunskiRacun.IDBanke = (int)reader["idbanke"];
                        obracunskiRacun.IDCentralneBanke = (int)reader["idcb"];
                        obracunskiRacun.IDObracunskogRacuna = (int)reader["idobracunskogracuna"];
                        obracunskiRacun.Stanje = (double)(decimal)reader["stanje"];
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return obracunskiRacun;
        }


        public static void UpdateObracunskogRacunaStanje(long id, double stanje)
        {
            string sql = "UPDATE obracunskiracun SET stanje = @stanje WHERE idobracunskogracuna = @id";
            using (SqlConnection conn = MySQLUtils.NapraviCBConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@stanje", stanje);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

    }
}

using Shared.BP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWS.Shared.Model;

namespace XWS.Shared
{
    public class RacunDB
    {
        public static Racun GetRacunByRacun(Int64 brRacuna)
        {
            Racun racun = null;

            using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
            {
                conn.Open();
                string sql = "SELECT * FROM racun WHERE idracuna = @racun";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@racun", brRacuna);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        racun = new Racun();
                        racun.Datum = (DateTime)reader["datum"];
                        racun.IDBanke = (int)reader["idbanke"];
                        racun.IDFirme = (int)reader["idfirme"];
                        racun.IDRacuna = (int)reader["idracuna"];
                        racun.PredhodnoStanje = (double)reader["prethodnostanje"];
                        racun.TrenutnoStanje = (double)reader["trenutnostanje"];
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return racun;
        }


        public static void UpdateRacunaStanje(long id, double stanje)
        {
            string sql = "UPDATE racun SET trenutnostanje = @stanje WHERE idracuna = @id";
            using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
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

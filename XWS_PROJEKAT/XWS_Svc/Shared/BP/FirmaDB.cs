using Shared.BP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWS_Svc.Shared.Model;

namespace XWS_Svc.Shared.BP
{
	public static class FirmaDB
	{
		public static Firma GetFirmaByName(string firmName)
		{
			Firma firma = null;

			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = "SELECT * FROM firma WHERE naziv = @naziv";
				using (SqlCommand cmd = new SqlCommand(sql,conn))
				{
					cmd.Parameters.AddWithValue("@naziv", firmName);
					SqlDataReader reader = cmd.ExecuteReader();

					if(reader.Read())
					{
						firma = new Firma();
						firma.AdresaFirme = (string)reader["adresa"];
						firma.IDFirme = (Int32)reader["idfirme"];
						firma.Racun = (Int64)(decimal)reader["brojracuna"];
						firma.NazivFirme = (string)reader["naziv"];
						firma.PIB = (string)reader["pib"];
					}
					reader.Close();
				}
				conn.Close();
			}
			return firma;
		}
	}
}

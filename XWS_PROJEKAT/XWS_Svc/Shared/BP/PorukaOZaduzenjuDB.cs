using Shared.BP;
using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS.Shared.BP
{
	public class PorukaOZaduzenjuDB
	{
		//
		public static List<PorukaOZaduzenju> GetAllPorukaOZaduzenju()
		{
			List<PorukaOZaduzenju> poruke = new List<PorukaOZaduzenju>();
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM porukaozaduzenju";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						PorukaOZaduzenju ret = new PorukaOZaduzenju();
						ret = ReadFromReader(reader);
						poruke.Add(ret);
					}
					reader.Close();
				}
				conn.Close();
			}

			return poruke;
		}//

		//
		public static PorukaOZaduzenju GetPorukaOZaduzenju(int idPoruke)
		{
			PorukaOZaduzenju ret;
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM porukaozaduzenju WHERE idporukeozaduzenju = @idporukeozaduzenju";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idporukeozaduzenju", idPoruke);
					ret = new PorukaOZaduzenju();
					SqlDataReader reader = cmd.ExecuteReader();

					reader.Read();
					ret = ReadFromReader(reader);
					reader.Close();
				}
				conn.Close();
			}

			return ret;
		}//

		//
		public static void InsertIntoPorukaOZaduzenju(PorukaOZaduzenju f)
		{
			using (SqlConnection conn = MySQLUtils.NapraviCBConn())
			{
				string sql = @"INSERT INTO [dbo].[porukaozaduzenju]
													   ([idporuke]
													   ,[swiftbankeduznika]
													   ,[obracunskiracunbankeduznika]
													   ,[idporukenaloga]
													   ,[datumvalute]
													   ,[iznos]
													   ,[sifravalute])
												 VALUES
													   (@idporuke
													   ,@swiftbankeduznika
													   ,@obracunskiracunbankeduznika
													   ,@idporukenaloga
													   ,@datumvalute
													   ,@iznos
													   ,@sifravalute)";
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idporuke", f.IDPPoruke);
					cmd.Parameters.AddWithValue("@swiftbankeduznika", f.SWIFTBankeDuznika);
					cmd.Parameters.AddWithValue("@obracunskiracunbankeduznika", f.ObracunskiRacunBankeDuznika);
					cmd.Parameters.AddWithValue("@idporukenaloga", f.IDPorukeNaloga);
					cmd.Parameters.AddWithValue("@datumvalute", f.DatumValute);
					cmd.Parameters.AddWithValue("@iznos", f.Iznos);
					cmd.Parameters.AddWithValue("@sifravalute", f.SifraValute);

					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}
		}//

		private static PorukaOZaduzenju ReadFromReader(SqlDataReader reader)
		{
			PorukaOZaduzenju ret = new PorukaOZaduzenju();

			ret.IDPorukeOZaduzenju = (int)reader["idporukeoodobrenju"];
			ret.IDPPoruke = (string)reader["idporuke"];
			ret.SWIFTBankeDuznika = (string)reader["swiftbankeduznika"];
			ret.ObracunskiRacunBankeDuznika = (string)reader["obracunskiracunbankeduznika"];
			ret.IDPorukeNaloga = (string)reader["idporukenaloga"];
			ret.DatumValute = (DateTime)reader["datumvalute"];
			ret.Iznos = (double)(decimal)reader["iznos"];
			ret.SifraValute = (string)reader["sifravalute"];

			return ret;
		}
	}
}

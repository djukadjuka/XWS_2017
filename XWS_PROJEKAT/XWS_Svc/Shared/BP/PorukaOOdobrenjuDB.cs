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
	public class PorukaOOdobrenjuDB
	{
		//
		public static List<PorukaOOdobrenju> GetAllPorukaOOdobrenju()
		{
			List<PorukaOOdobrenju> poruke = new List<PorukaOOdobrenju>();
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM porukaoodobrenju";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						PorukaOOdobrenju ret = new PorukaOOdobrenju();
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
		public static PorukaOOdobrenju GetPorukaOOdobrenju(int idPoruke)
		{
			PorukaOOdobrenju ret;
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM porukaoodobrenju WHERE idporukeoodobrenju = @idporukeoodobrenju";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idporukeoodobrenju", idPoruke);
					ret = new PorukaOOdobrenju();
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
		public static void InsertIntoPorukaOOdobrenju(PorukaOOdobrenju f)
		{
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				string sql = @"INSERT INTO [dbo].[porukaoodobrenju]
													   ([idporuke]
													   ,[swiftbankepoverioca]
													   ,[obracunskiracunbankepoverioca]
													   ,[idporukenaloga]
													   ,[datumvalute]
													   ,[iznos]
													   ,[sifravalute])
												 VALUES
													   (@idporuke
													   ,@swiftbankepoverioca
													   ,@obracunskiracunbankepoverioca
													   ,@idporukenaloga
													   ,@datumvalute
													   ,@iznos
													   ,@sifravalute)";
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idporuke"							, f.IDPoruke);
					cmd.Parameters.AddWithValue("@swiftbankepoverioca"				, f.SWIFTBankePoverioca);
					cmd.Parameters.AddWithValue("@obracunskiracunbankepoverioca"	, f.ObracunskiRacunBankePoverioca);
					cmd.Parameters.AddWithValue("@idporukenaloga"					, f.IDPorukeNaloga);
					cmd.Parameters.AddWithValue("@datumvalute"						, f.DatumValute);
					cmd.Parameters.AddWithValue("@iznos"							, f.Iznos);
					cmd.Parameters.AddWithValue("@sifravalute"						, f.SifraValute);

					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}
		}//

		private static PorukaOOdobrenju ReadFromReader(SqlDataReader reader)
		{
			PorukaOOdobrenju ret = new PorukaOOdobrenju();

			ret.IDPorukeOOdobrenju = (int)reader["idporukeoodobrenju"];
			ret.IDPoruke = (string)reader["idporuke"];
			ret.SWIFTBankePoverioca = (string)reader["swiftbankepoverioca"];
			ret.ObracunskiRacunBankePoverioca = (string)reader["obracunskiracunbankepoverioca"];
			ret.IDPorukeNaloga = (string)reader["idporukenaloga"];
			ret.DatumValute = (DateTime)reader["datumvalute"];
			ret.Iznos = (double)(decimal)reader["iznos"];
			ret.SifraValute = (string)reader["sifravalute"];

			return ret;
		}
	}
}

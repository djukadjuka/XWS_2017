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
	public class RTGSNalogDB
	{
		//
		public static List<RTGSNalog> GetAllRTGSNalog()
		{
			List<RTGSNalog> poruke = new List<RTGSNalog>();
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM rtgsnalog";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						RTGSNalog ret = new RTGSNalog();
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
		public static RTGSNalog GetRTGSNalog(int idNaloga)
		{
			RTGSNalog ret;
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM rtgsnalog WHERE idrtgsnaloga = @idrtgsnaloga";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idrtgsnaloga", idNaloga);
					ret = new RTGSNalog();
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
		public static RTGSNalog InsertIntoRTGSNalog(RTGSNalog f)
		{
			using (SqlConnection conn = MySQLUtils.NapraviCBConn())
			{
				string sql = @"INSERT INTO [dbo].[rtgsnalog]
													   ([idporuke]
													   ,[swiftbankaduznika]
													   ,[obracunskiracunbankeduznika]
													   ,[swiftbankapoverioca]
													   ,[obracunskiracunbankepoverioca]
													   ,[duznik]
													   ,[svrhaplacanja]
													   ,[primalac]
													   ,[datumnaloga]
													   ,[datumvalute]
													   ,[racunduznika]
													   ,[modelzaduzenja]
													   ,[pozivnabrzaduzenja]
													   ,[racunpoverioca]
													   ,[modelodobrenja]
													   ,[pozivnabrodobrenja]
													   ,[iznos]
													   ,[sifravalute])
												 VALUES
													   (@idporuke
													   ,@swiftbankaduznika
													   ,@obracunskiracunbankeduznika
													   ,@swiftbankapoverioca
													   ,@obracunskiracunbankepoverioca
													   ,@duznik
													   ,@svrhaplacanja
													   ,@primalac
													   ,@datumnaloga
													   ,@datumvalute
													   ,@racunduznika
													   ,@modelzaduzenja
													   ,@pozivnabrzaduzenja
													   ,@racunpoverioca
													   ,@modelodobrenja
													   ,@pozivnabrodobrenja
													   ,@iznos
													   ,@sifravalute) SELECT SCOPE_IDENTITY()";
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idporuke", f.IDPoruke);
					cmd.Parameters.AddWithValue("@swiftbankaduznika", f.SWIFTBankaDuznika);
					cmd.Parameters.AddWithValue("@obracunskiracunbankeduznika", f.ObracunskiRacunBankeDuznika);
					cmd.Parameters.AddWithValue("@swiftbankapoverioca", f.SWIFTBankaPoverioca);
					cmd.Parameters.AddWithValue("@obracunskiracunbankepoverioca", f.ObracunskiRacunBankePoverioca);
					cmd.Parameters.AddWithValue("@duznik", f.Duznik);
					cmd.Parameters.AddWithValue("@svrhaplacanja", f.SvrhaPlacanja);
					cmd.Parameters.AddWithValue("@primalac", f.Primalac);
					cmd.Parameters.AddWithValue("@datumnaloga", f.DatumNaloga);
					cmd.Parameters.AddWithValue("@datumvalute", f.DatumValute);
					cmd.Parameters.AddWithValue("@racunduznika", f.RacunDuznika);
					cmd.Parameters.AddWithValue("@modelzaduzenja", f.ModelZaduzenja);
					cmd.Parameters.AddWithValue("@pozivnabrzaduzenja", f.PozivNaBrZaduzenja);
					cmd.Parameters.AddWithValue("@racunpoverioca", f.RacunPoverioca);
					cmd.Parameters.AddWithValue("@modelodobrenja", f.ModelOdobrenja);
					cmd.Parameters.AddWithValue("@pozivnabrodobrenja", f.PozivNaBrOdobrenja);
					cmd.Parameters.AddWithValue("@iznos", f.Iznos);
					cmd.Parameters.AddWithValue("@sifravalute", f.SifraValute);
					
					object x = cmd.ExecuteScalar();
					f.IDRTGSNaloga = (int)(decimal)x;
					
				}
				conn.Close();
			}
			return f;
		}//

		private static RTGSNalog ReadFromReader(SqlDataReader reader)
		{
			RTGSNalog ret = new RTGSNalog();

			ret.IDPoruke = (string)reader["idporuke"];
			ret.SWIFTBankaDuznika = (string)reader["swiftbankaduznika"];
			ret.ObracunskiRacunBankeDuznika = (string)reader["obracunskiracunbankeduznika"];
			ret.SWIFTBankaPoverioca = (string)reader["swiftbankapoverioca"];
			ret.ObracunskiRacunBankePoverioca = (string)reader["obracunskiracunbankepoverioca"];
			ret.Duznik = (string)reader["duznik"];
			ret.SvrhaPlacanja = (string)reader["svrhaplacanja"];
			ret.Primalac = (string)reader["primalac"];
			ret.DatumNaloga = (DateTime)reader["datumnaloga"];
			ret.DatumValute = (DateTime)reader["datumvalute"];
			ret.RacunDuznika = (string)reader["racunduznika"];
			ret.ModelZaduzenja = (double)(decimal)reader["modelzaduzenja"];
			ret.PozivNaBrZaduzenja = (string)reader["pozivnabrzaduzenja"];
			ret.RacunPoverioca = (string)reader["racunpoverioca"];
			ret.ModelOdobrenja = (double)(decimal)reader["modelodobrenja"];
			ret.PozivNaBrOdobrenja = (string)reader["pozivnabrodobrenja"];
			ret.Iznos = (double)(decimal)reader["iznos"];
			ret.SifraValute = (string)reader["sifravalute"];
			ret.IDRTGSNaloga = (int)reader["idrtgsnaloga"];
			
			return ret;
		}
	}
}

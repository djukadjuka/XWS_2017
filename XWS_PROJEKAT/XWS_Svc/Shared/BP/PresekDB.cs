using Shared.BP;
using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS_Svc.Shared.BP
{
	public class PresekDB
	{
		//
		public static List<Presek> GetAllPresek()
		{
			List<Presek> preseci = new List<Presek>();
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM presek";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						Presek ret = new Presek();
						ret = ReadFromReader(reader);
						preseci.Add(ret);
					}
					reader.Close();
				}
				conn.Close();
			}

			return preseci;
		}//

		//
		public static Presek GetPresek(int idPreseka)
		{
			Presek ret;
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM presek WHERE idpreseka= @idpreseka";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idpreseka", idPreseka);
					ret = new Presek();
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
		public static void InsertIntoPresek(Presek f)
		{
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				string sql = @"INSERT INTO [dbo].[presek]
											   ([brracuna]
											   ,[datumnaloga]
											   ,[brpreseka]
											   ,[prethodnostanje]
											   ,[brpromenaukorist]
											   ,[ukupnoukorist]
											   ,[brpromenanateret]
											   ,[ukupnonateret]
											   ,[novostanje])
										 VALUES
											   (@brracuna
											   ,@datumnaloga
											   ,@brpreseka 
											   ,@prethodnostanje
											   ,@brpromenaukorist
											   ,@ukupnoukorist
											   ,@brpromenanateret
											   ,@ukupnonateret
											   ,@novostanje			)";
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@brracuna", f.BrRacuna);
					cmd.Parameters.AddWithValue("@datumnaloga", f.DatumNaloga);
					cmd.Parameters.AddWithValue("@brpreseka", f.BrPreseka);
					cmd.Parameters.AddWithValue("@prethodnostanje", f.PrethodnoStanje);
					cmd.Parameters.AddWithValue("@brpromenaukorist", f.BrPromenaUKorist);
					cmd.Parameters.AddWithValue("@ukupnoukorist", f.UkupnoUKorist);
					cmd.Parameters.AddWithValue("@brpromenanateret", f.BrPromenaNaTeret);
					cmd.Parameters.AddWithValue("@ukupnonateret", f.UkupnoNaTeret);
					cmd.Parameters.AddWithValue("@novostanje", f.NovoStanje);
					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}
		}//

		private static Presek ReadFromReader(SqlDataReader reader)
		{
			Presek ret = new Presek();

			ret.IDPreseka					= (int)reader["idpreseka"];
			ret.BrRacuna					= (string)reader["brracuna"];
			ret.DatumNaloga					= (DateTime)reader["datumnaloga"];
			ret.BrPreseka					= (double)(decimal)reader["brpreseka "];
			ret.PrethodnoStanje				= (double)(decimal)reader["prethodnostanje"];
			ret.BrPromenaUKorist			= (double)(decimal)reader["brpromenaukorist"];
			ret.UkupnoUKorist				= (double)(decimal)reader["ukupnoukorist"];
			ret.BrPromenaNaTeret			= (double)(decimal)reader["brpromenanateret"];
			ret.UkupnoNaTeret				= (double)(decimal)reader["ukupnonateret"];
			ret.NovoStanje					= (double)(decimal)reader["novostanje"];

			ret.StavkePreseka = (ListaStavkiPreseka)StavkaPresekaDB.GetAllStavkaPresekaByPresekId(ret.IDPreseka);
			
			return ret;
		}
	}
}

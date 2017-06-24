using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerStartup
{
	public class StartAllServers
	{
		public static void Main(string []args){
			try
			{
				var svh_BANKA_A = ServerStartup.ServerUtils.CreateBankServer("BankaA");
				//var svh_BANKA_B = ServerStartup.ServerUtils.CreateBankServer("BankaB");
				var svh_FIRMA_A = ServerStartup.ServerUtils.CreateFirmaServer("FirmaA");
				//var svh_FIRMA_B = ServerStartup.ServerUtils.CreateFirmaServer("FirmaB");
				var svh_CB = ServerUtils.CreateCentralnaBankaServer();

				svh_BANKA_A.Open();
				//svh_BANKA_B.Open();
				svh_FIRMA_A.Open();
				//svh_FIRMA_B.Open();
				svh_CB.Open();

				var re = new ManualResetEvent(false);
				re.WaitOne();

				svh_FIRMA_A.Close();
				svh_BANKA_A.Close();
				//svh_FIRMA_B.Close();
				//svh_BANKA_B.Close();
				svh_CB.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception");
				Console.WriteLine(e);
				var re2 = new ManualResetEvent(false);
				re2.WaitOne();
			}
		}
	}
}

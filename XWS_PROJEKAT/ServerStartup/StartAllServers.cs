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
				var svh_BANKA = ServerStartup.ServerUtils.CreateBankServer("BankaA");
				var svh_FIRMA = ServerStartup.ServerUtils.CreateFirmaServer("FirmaA");
				
				svh_BANKA.Open();
				svh_FIRMA.Open();

				var re = new ManualResetEvent(false);
				re.WaitOne();

				svh_FIRMA.Close();
				svh_BANKA.Close();
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

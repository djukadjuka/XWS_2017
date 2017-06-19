using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Model
{
	/// <summary>
	/// Svaka klasa koja predstavlja model podataka treba ovu klasu da implementira.
	/// Sadrzi sve stvari bitne za MVVM.
	/// U svakom propu, pozvati OnPropertyChanged(ime_propertija)
	/// npr:
	/// public String Ime{
	///		get{return ime;}
	///		set{	ime = value; OnPropertyChanged("Ime");}
	/// }
	/// </summary>
	public abstract class VMShared : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Metoda za MVVM. Pozvati svaki put kada se promeni property. (set{x = value; OnPropertyChanged(...)})
		/// </summary>
		/// <param name="propName"> Ime Propertija koji se menja.</param>
		public void OnPropertyChanged(String propName)
		{
			if(PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}
	}
}

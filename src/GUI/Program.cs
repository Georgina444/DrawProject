using System;
using System.Windows.Forms;

namespace Draw
{

	internal sealed class Program
	{
		/// Входна точка. Създава и показва главната форма на програмата.
		// The Main Method is responsible for creating the main form(an instance of the main form) and starting the application.

		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();  // enables visual styles for the application which allows controls to be rendered 
			                                   // with the current windows theme(makes the app more modern)

			Application.SetCompatibleTextRenderingDefault(false);  // sets the defailt text rendering engine for the application
			                                                       // true -> GDI+ text rendering engine(more accurate but slower)
																   // false -> GDI text rendering engine(faster but less accurate)

			Application.Run(new MainForm());   // displays the main form on the screen 
		}
		
	}
}

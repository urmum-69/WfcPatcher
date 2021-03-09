using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WfcPatcher {
	static class CommandLineArguments {
		public static string[] Filenames { get; private set; }
		public static string Domain = null;
		public static string Substitute = null;

		public static bool ParseCommandLineArguments( string[] args ) {
			bool parseSuccess = true;
			int maxLength = "nintendowifi.net".Length;

			try {
				List<string> filenames = new List<string>();

				for ( int i = 0; i < args.Length; ++i ) {
					switch ( args[i] ) {
						case "-d":
						case "--domain":
							string domain = args[++i];
							if ( domain.Length <= maxLength ) {
								Domain = domain;
							} else {
								Console.WriteLine( "Replacement domain cannot be longer than original domain ({0} characters).", maxLength );
								parseSuccess = false;
							}
							break;
						case "-s":
						case "--substitute":
							string substitute = args[++i];
							if ( substitute.Length <= maxLength ) {
								Substitute = substitute;
							} else {
								Console.WriteLine( "Substituted domain cannot be longer than original domain ({0} characters).", maxLength );
								parseSuccess = false;
							}
							break;
						default:
							if ( System.IO.File.Exists( args[i] ) ) {
								filenames.Add( args[i] );
							}
							break;
					}
				}
				Filenames = filenames.ToArray();
				if ( Filenames.Length == 0 ) {
					Console.WriteLine( "No valid file to patch was provided!" );
					parseSuccess = false;
				}
			} catch ( IndexOutOfRangeException ) {
				Console.WriteLine( "Last given option needs more parameters!" );
				parseSuccess = false;
			}

			return parseSuccess;
		}
	}
}

namespace LinuxMonitoringConsole.Helpers
{
	public static class StringHelper
	{
		public static IEnumerable<string> SplitToLines(this string input)
		{
			if (input == null)
			{
				yield break;
			}

			using (System.IO.StringReader reader = new System.IO.StringReader(input))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					yield return line;
				}
			}
		}

		public static string ToLiteral(this string valueTextForCompiler)
		{
			return Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(valueTextForCompiler, false);
		}
	}
}

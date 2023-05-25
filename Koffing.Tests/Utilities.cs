namespace Koffing.Tests;

public static class Utilities
{
	public static void PrepareExtraBecauseContext(ref string extraBecauseContext)
	{
		if (!string.IsNullOrWhiteSpace(extraBecauseContext) && !extraBecauseContext.StartsWith(" "))
		{
			extraBecauseContext = $" {extraBecauseContext}";
		}
	}
}
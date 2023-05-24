using System.Text;
using System.Text.RegularExpressions;

namespace Koffing;

public static class Extensions
{
	public static readonly Regex TileNotationRegex =
		new Regex(@"(?<suitOfTiles>((?<ranks>[0-9]+)(?<suit>[mps])|(?<ranks>[1-7]+)(?<suit>z)))");

	public static IEnumerable<Tile> ToTiles(this string input)
	{
		foreach (Match match in TileNotationRegex.Matches(input))
		{
			var stringSuit = match.Groups["suit"].Value;
			var suit = Suit.Man;
			switch (stringSuit)
			{
				case "m":
					suit = Suit.Man;
					break;

				case "p":
					suit = Suit.Pin;
					break;

				case "s":
					suit = Suit.Sou;
					break;

				case "z":
					suit = Suit.Zi;
					break;

				default:
					throw new InvalidOperationException($"Unknown suit encountered: {stringSuit}");
			}

			var stringRanks = match.Groups["ranks"].Value;
			foreach (var charRank in stringRanks)
			{
				var rank = int.Parse(charRank.ToString());
				yield return new Tile(suit, rank);
			}
		}
		yield break;
	}

	public static string NotationFromTiles(this IEnumerable<Tile> input)
	{
		var builder = new StringBuilder();
		var tiles = input.ToList();
		if (tiles.Count > 0)
		{
			var currentSuit = tiles[0].Suit;
			foreach (var tile in tiles)
			{
				if (tile.Suit != currentSuit)
				{
					AppendSuit(currentSuit, builder);
					currentSuit = tile.Suit;
				}
				builder.Append(tile.Rank);
			}
			AppendSuit(currentSuit, builder);
		}

		return builder.ToString();
	}

	private static void AppendSuit(Suit suit, StringBuilder builder)
	{
		switch (suit)
		{
			case Suit.Man:
				builder.Append("m");
				break;

			case Suit.Pin:
				builder.Append("p");
				break;

			case Suit.Sou:
				builder.Append("s");
				break;

			case Suit.Zi:
				builder.Append("z");
				break;
		}
	}
}
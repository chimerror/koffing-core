using Koffing;
using FluentAssertions;

namespace Koffing.Tests;

public class TileTests
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void RawRanksAreCorrect()
	{
		foreach (var suit in Enum.GetValues<Suit>())
		{
			for (var rank = 0; rank <= 9; rank++)
			{
				if (suit == Suit.Zi && (rank == 0 || rank > 7))
				{
					continue;
				}

				var tile = new Tile(suit, rank);

				var expectedRawRank = rank == 0 ? 5 : rank;
				tile.RawRank.Should().Be(expectedRawRank, "raw rank should be correctly determined");
			}
		}
	}

	[Test]
	public void RawEqualsIsCorrect()
	{
		foreach (var suit in Enum.GetValues<Suit>())
		{
			for (var rank = 0; rank <= 9; rank++)
			{
				if (suit == Suit.Zi && (rank == 0 || rank > 7))
				{
					continue;
				}

				var tileA = new Tile(suit, rank);
				var tileB = new Tile(suit, rank);

				tileA.RawEquals(tileB).Should().BeTrue("tile A should rawly equal identical tile B");
				tileB.RawEquals(tileA).Should().BeTrue("tile B should rawly equal identical tile A");
				if (rank == 0)
				{
					var tileC = new Tile(suit, 5);
					tileA.RawEquals(tileC).Should().BeTrue("red five A should rawly equal non-red five C");
					tileC.RawEquals(tileA).Should().BeTrue("non-red five C should rawly equal red five A");
				}
			}
		}
	}

	[TestCaseSource(nameof(CompareTileTestCases))]
	public void CompareTilesIsCorrect(
		Tile tileA, Tile? tileB, int expectedComparisonValue, string extraBecauseContext = "")
	{
		PrepareExtraBecauseContext(ref extraBecauseContext);
		tileA.CompareTo(tileB).Should()
			.Be(expectedComparisonValue, $"tile comparison should be correct{extraBecauseContext}");
	}

	[TestCaseSource(nameof(ToTilesTestCases))]
	public void ToTilesIsCorrect(string input, List<Tile> expectedOutput, string extraBecauseContext = "")
	{
		var actualOutput = input.ToTiles().ToList();
		PrepareExtraBecauseContext(ref extraBecauseContext);
		actualOutput.Should().Equal(expectedOutput, $"parsing to tiles should be correct{extraBecauseContext}");
	}

	private static void PrepareExtraBecauseContext(ref string extraBecauseContext)
	{
		if (!string.IsNullOrWhiteSpace(extraBecauseContext) && !extraBecauseContext.StartsWith(" "))
		{
			extraBecauseContext = $" {extraBecauseContext}";
		}
	}

	private static IEnumerable<object> CompareTileTestCases()
	{
		yield return new object?[]
		{
			new Tile(),
			null,
			1,
			"when comparing to null tile"
		};
		yield return new object[]
		{
			new Tile(Suit.Man),
			new Tile(Suit.Pin),
			-1,
			"when comparing man to pin"
		};
		yield return new object[]
		{
			new Tile(Suit.Pin),
			new Tile(Suit.Man),
			1,
			"when comparing man to pin"
		};
		yield return new object[]
		{
			new Tile(Suit.Pin),
			new Tile(Suit.Sou),
			-1,
			"when comparing pin to sou"
		};
		yield return new object[]
		{
			new Tile(Suit.Sou),
			new Tile(Suit.Pin),
			1,
			"when comparing sou to pin"
		};
		yield return new object[]
		{
			new Tile(Suit.Sou),
			new Tile(Suit.Zi),
			-1,
			"when comparing sou to zi"
		};
		yield return new object[]
		{
			new Tile(Suit.Zi),
			new Tile(Suit.Sou),
			1,
			"when comparing zi to sou"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 3),
			new Tile(Suit.Man, 3),
			0,
			"when comparing identical non-red 5s"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 0),
			new Tile(Suit.Man, 0),
			0,
			"when comparing identical red 5s"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 0),
			new Tile(Suit.Man, 5),
			1,
			"when comparing red 5 to non-red 5"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 0),
			new Tile(Suit.Man, 2),
			1,
			"when comparing red 5 to 2"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 0),
			new Tile(Suit.Man, 7),
			-1,
			"when comparing red 5 to 7"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 5),
			new Tile(Suit.Man, 0),
			-1,
			"when comparing non-red 5 to red 5"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 2),
			new Tile(Suit.Man, 0),
			-1,
			"when comparing 2 to red 5"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 7),
			new Tile(Suit.Man, 0),
			1,
			"when comparing 7 to red 5"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 2),
			new Tile(Suit.Man, 7),
			-1,
			"when comparing 2 to 7"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 7),
			new Tile(Suit.Man, 2),
			1,
			"when comparing 7 to 2"
		};
	}

	private static IEnumerable<object> ToTilesTestCases()
	{
		yield return new object[]
		{
			"1p",
			new List<Tile> {
				new Tile(Suit.Pin, 1)
			},
			"when provided single number tile"
		};
		yield return new object[]
		{
			"4z",
			new List<Tile> {
				new Tile(Suit.Zi, 4)
			},
			"when provided single honor tile"
		};
		yield return new object[]
		{
			"123p",
			new List<Tile> {
				new Tile(Suit.Pin, 1),
				new Tile(Suit.Pin, 2),
				new Tile(Suit.Pin, 3)
			},
			"when provided multiple tiles in one suit"
		};
		yield return new object[]
		{
			"729p",
			new List<Tile> {
				new Tile(Suit.Pin, 7),
				new Tile(Suit.Pin, 2),
				new Tile(Suit.Pin, 9)
			},
			"when keeping order and provided multiple tiles in one suit"
		};
		yield return new object[]
		{
			"0555p",
			new List<Tile> {
				new Tile(Suit.Pin, 0),
				new Tile(Suit.Pin, 5),
				new Tile(Suit.Pin, 5),
				new Tile(Suit.Pin, 5)
			},
			"when provided with kong of 5s (one red 5)"
		};
		yield return new object[]
		{
			"123p3z5m",
			new List<Tile> {
				new Tile(Suit.Pin, 1),
				new Tile(Suit.Pin, 2),
				new Tile(Suit.Pin, 3),
				new Tile(Suit.Zi, 3),
				new Tile(Suit.Man, 5)
			},
			"when provided multiple suits of tiles, some with single tiles"
		};
		yield return new object[]
		{
			"123p333z45m",
			new List<Tile> {
				new Tile(Suit.Pin, 1),
				new Tile(Suit.Pin, 2),
				new Tile(Suit.Pin, 3),
				new Tile(Suit.Zi, 3),
				new Tile(Suit.Zi, 3),
				new Tile(Suit.Zi, 3),
				new Tile(Suit.Man, 4),
				new Tile(Suit.Man, 5)
			},
			"when provided multiple suits of tiles, all with multiple tiles"
		};
	}
}
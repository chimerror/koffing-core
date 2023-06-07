namespace Koffing.Tests;

public class BlockTests
{
	[SetUp]
	public void Setup()
	{
	}

	[TestCaseSource(nameof(AttemptKongTestCases))]
	public void AttemptKongIsCorrect(
		Tile tile,
		IEnumerable<Tile> otherTiles,
		bool expectedSuccess,
		List<List<Block>> expectedOutput,
		string extraBecauseContext = "")
	{
		var actualSuccess = Kong.AttemptWith(tile, otherTiles, out var actualOutput);
		actualSuccess
			.Should().Be(expectedSuccess, $"attempting kongs should return correct success value{extraBecauseContext}");
		actualOutput
			.Should().BeEquivalentTo(expectedOutput, $"attempting kongs should be correct{extraBecauseContext}");
	}

	[TestCaseSource(nameof(AttemptPungTestCases))]
	public void AttemptPungIsCorrect(
		Tile tile,
		IEnumerable<Tile> otherTiles,
		bool expectedSuccess,
		List<List<Block>> expectedOutput,
		string extraBecauseContext = "")
	{
		var actualSuccess = Pung.AttemptWith(tile, otherTiles, out var actualOutput);
		actualSuccess
			.Should().Be(expectedSuccess, $"attempting kongs should return correct success value{extraBecauseContext}");
		actualOutput
			.Should().BeEquivalentTo(expectedOutput, $"attempting kongs should be correct{extraBecauseContext}");
	}

	[TestCaseSource(nameof(AttemptChowTestCases))]
	public void AttemptChowIsCorrect(
		Tile tile,
		IEnumerable<Tile> otherTiles,
		bool expectedSuccess,
		List<List<Block>> expectedOutput,
		string extraBecauseContext = "")
	{
		var actualSuccess = Chow.AttemptWith(tile, otherTiles, out var actualOutput);
		actualSuccess
			.Should().Be(expectedSuccess, $"attempting chows should return correct success value{extraBecauseContext}");
		actualOutput
			.Should().BeEquivalentTo(expectedOutput, $"attempting chows should be correct{extraBecauseContext}");
	}

	private static IEnumerable<object> AttemptKongTestCases()
	{
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m55z".ToTiles(),
			false,
			new List<List<Block>> {
				new List<Block> {
					new UnsortedTiles("2p3s11m555z".ToTiles())
				}
			},
			"when there are only two matching tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m5z".ToTiles(),
			false,
			new List<List<Block>> {
				new List<Block> {
					new UnsortedTiles("2p3s11m55z".ToTiles())
				}
			},
			"when there is only one matching tile"
		};
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m".ToTiles(),
			false,
			new List<List<Block>> {
				new List<Block> {
					new UnsortedTiles("2p3s11m5z".ToTiles())
				}
			},
			"when there are no matching tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Pin, 7),
			"777p2s11m66z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Kong("7777p".ToTiles()),
					new UnsortedTiles("2s11m66z".ToTiles())
				}
			},
			"when there are enough matching pin tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Sou, 4),
			"067p12344456s11m66z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Kong("4444s".ToTiles()),
					new UnsortedTiles("067p12356s11m66z".ToTiles())
				}
			},
			"when there are enough matching sou tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 1),
			"2p3s111m66z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Kong("1111m".ToTiles()),
					new UnsortedTiles("2p3s66z".ToTiles())
				}
			},
			"when there are enough matching man tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m555z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Kong("5555z".ToTiles()),
					new UnsortedTiles("2p3s11m".ToTiles())
				}
			},
			"when there are enough matching zi tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 0),
			"2p3s555m77z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Kong("0555m".ToTiles()),
					new UnsortedTiles("2p3s77z".ToTiles())
				}
			},
			"when given red five and there are enough matching tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 5),
			"2p3s550m77z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Kong("0555m".ToTiles()),
					new UnsortedTiles("2p3s77z".ToTiles())
				}
			},
			"when given non-red five and there are enough matching tiles"
		};
	}

	private static IEnumerable<object> AttemptPungTestCases()
	{
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m5z".ToTiles(),
			false,
			new List<List<Block>> {
				new List<Block> {
					new UnsortedTiles("2p3s11m55z".ToTiles())
				}
			},
			"when there is only one matching tile"
		};
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m".ToTiles(),
			false,
			new List<List<Block>> {
				new List<Block> {
					new UnsortedTiles("2p3s11m5z".ToTiles())
				}
			},
			"when there are no matching tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Pin, 7),
			"777p3s11m66z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Pung("777p".ToTiles()),
					new UnsortedTiles("7p3s11m66z".ToTiles())
				}
			},
			"when there are more than enough matching pin tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Sou, 4),
			"067p1234456s11m66z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Pung("444s".ToTiles()),
					new UnsortedTiles("067p12356s11m66z".ToTiles())
				}
			},
			"when there are enough matching sou tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 1),
			"2p3s11m66z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Kong("111m".ToTiles()),
					new UnsortedTiles("2p3s66z".ToTiles())
				}
			},
			"when there are enough matching man tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m55z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Pung("555z".ToTiles()),
					new UnsortedTiles("2p3s11m".ToTiles())
				}
			},
			"when there are enough matching zi tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 0),
			"2p3s55m77z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Pung("055m".ToTiles()),
					new UnsortedTiles("2p3s77z".ToTiles())
				}
			},
			"when given red five and there are enough matching tiles"
		};
		// TODO: Ordering seems to matter here when using BeEquivalent, perhaps make own?
		yield return new object[]
		{
			new Tile(Suit.Sou, 5),
			"067p12344550s11m66z".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Pung("555s".ToTiles()),
					new UnsortedTiles("067p12344s11m66z0s".ToTiles())
				},
				new List<Block> {
					new Pung("550s".ToTiles()),
					new UnsortedTiles("067p12344s11m66z5s".ToTiles())
				},
			},
			"when given non-red five and there are more than enough matching tiles with a red five"
		};
	}

	private static IEnumerable<object> AttemptChowTestCases()
	{
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m34z".ToTiles(),
			false,
			new List<List<Block>> {
				new List<Block> {
					new UnsortedTiles("2p3s11m345z".ToTiles())
				}
			},
			"when given an honor tile"
		};
		yield return new object[]
		{
			new Tile(Suit.Pin, 7),
			"46p3s1134z".ToTiles(),
			false,
			new List<List<Block>> {
				new List<Block> {
					new UnsortedTiles("46p3s1134z7p".ToTiles())
				}
			},
			"when only one relevant tile is available"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 3),
			"1245m".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Chow("123m".ToTiles()),
					new UnsortedTiles("45m".ToTiles())
				},
				new List<Block> {
					new Chow("234m".ToTiles()),
					new UnsortedTiles("15m".ToTiles())
				},
				new List<Block> {
					new Chow("345m".ToTiles()),
					new UnsortedTiles("12m".ToTiles())
				},
			},
			"when there are multiple possible chows with no five tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Sou, 4),
			"34067s".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Chow("340s".ToTiles()),
					new UnsortedTiles("467s".ToTiles())
				},
				new List<Block> {
					new Chow("406s".ToTiles()),
					new UnsortedTiles("347s".ToTiles())
				},
			},
			"when multiple chows can be made with a red five from the other tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Pin, 0),
			"34567p".ToTiles(),
			true,
			new List<List<Block>> {
				new List<Block> {
					new Chow("340p".ToTiles()),
					new UnsortedTiles("567p".ToTiles())
				},
				new List<Block> {
					new Chow("406p".ToTiles()),
					new UnsortedTiles("357p".ToTiles())
				},
				new List<Block> {
					new Chow("067p".ToTiles()),
					new UnsortedTiles("345p".ToTiles())
				},
			},
			"when multiple chows can be made with a chosen red five"
		};
	}
}
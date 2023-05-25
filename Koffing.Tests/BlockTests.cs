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
		List<Block> expectedOutput,
		string extraBecauseContext = "")
	{
		var actualSuccess = Kong.AttemptWith(tile, otherTiles, out var actualOutput);
		actualSuccess
			.Should().Be(expectedSuccess, $"attempting kongs should return correct success value{extraBecauseContext}");
		actualOutput
			.Should().BeEquivalentTo(expectedOutput, $"attempting kongs should be correct{extraBecauseContext}");
	}

	private static IEnumerable<object> AttemptKongTestCases()
	{
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m55z".ToTiles(),
			false,
			new List<Block> {
				new UnsortedTiles("2p3s11m555z".ToTiles())
			},
			"when there are only two matching tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m5z".ToTiles(),
			false,
			new List<Block> {
				new UnsortedTiles("2p3s11m55z".ToTiles())
			},
			"when there is only one matching tile"
		};
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m".ToTiles(),
			false,
			new List<Block> {
				new UnsortedTiles("2p3s11m5z".ToTiles())
			},
			"when there are no matching tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Pin, 7),
			"777p2s11m66z".ToTiles(),
			true,
			new List<Block> {
				new Kong("7777p".ToTiles()),
				new UnsortedTiles("2s11m66z".ToTiles())
			},
			"when there are enough matching pin tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Sou, 4),
			"067p12344456s11m66z".ToTiles(),
			true,
			new List<Block> {
				new Kong("4444s".ToTiles()),
				new UnsortedTiles("067p12356s11m66z".ToTiles())
			},
			"when there are enough matching sou tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 1),
			"2p3s111m66z".ToTiles(),
			true,
			new List<Block> {
				new Kong("1111m".ToTiles()),
				new UnsortedTiles("2p3s66z".ToTiles())
			},
			"when there are enough matching man tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Zi, 5),
			"2p3s11m555z".ToTiles(),
			true,
			new List<Block> {
				new Kong("5555z".ToTiles()),
				new UnsortedTiles("2p3s11m".ToTiles())
			},
			"when there are enough matching zi tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 0),
			"2p3s555m77z".ToTiles(),
			true,
			new List<Block> {
				new Kong("0555m".ToTiles()),
				new UnsortedTiles("2p3s77z".ToTiles())
			},
			"when given red five and there are enough matching tiles"
		};
		yield return new object[]
		{
			new Tile(Suit.Man, 5),
			"2p3s550m77z".ToTiles(),
			true,
			new List<Block> {
				new Kong("0555m".ToTiles()),
				new UnsortedTiles("2p3s77z".ToTiles())
			},
			"when given non-red five and there are enough matching tiles"
		};
	}
}
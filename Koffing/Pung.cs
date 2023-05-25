namespace Koffing;

public class Pung : Meld
{
	public Pung(IEnumerable<Tile>? tiles = null) : base(tiles)
	{
	}

	public static bool AttemptWith(Tile tile, IEnumerable<Tile> otherTiles, out List<List<Block>> foundBlockLists)
	{
		foundBlockLists = new List<List<Block>>();
		var otherTilesList = otherTiles.ToList();
		var matchingTiles = otherTilesList.Where(t => t.RawEquals(tile)).ToList();
		var nonMatchingTiles = otherTilesList.Where(t => !t.RawEquals(tile)).ToList();
		if (matchingTiles.Count < 2)
		{
			nonMatchingTiles.AddRange(matchingTiles);
			nonMatchingTiles.Add(tile);
			foundBlockLists.Add(new List<Block> {
				new UnsortedTiles(nonMatchingTiles)
			});
			return false;
		}
		else if (tile.Suit != Suit.Zi && tile.Rank == 5 && matchingTiles.Count == 3)
		{
			foundBlockLists.Add(new List<Block> {
				new Pung(new List<Tile> {
					new Tile(tile.Suit, 5),
					new Tile(tile.Suit, 5),
					new Tile(tile.Suit, 5)
				}),
				new UnsortedTiles(nonMatchingTiles.Append(new Tile(tile.Suit, 0)))
			});
			foundBlockLists.Add(new List<Block> {
				new Pung(new List<Tile> {
					new Tile(tile.Suit, 0),
					new Tile(tile.Suit, 5),
					new Tile(tile.Suit, 5)
				}),
				new UnsortedTiles(nonMatchingTiles.Append(new Tile(tile.Suit, 5)))
			});
			return true;
		}
		else
		{
			if (matchingTiles.Count == 3)
			{
				nonMatchingTiles.Add(matchingTiles.Last());
			}
			foundBlockLists.Add(new List<Block> {
				new Pung(matchingTiles.Take(2).Append(tile)),
				new UnsortedTiles(nonMatchingTiles)
			});
			return true;
		}
	}
}
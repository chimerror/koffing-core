namespace Koffing;

public class Chow : Meld
{
	public Chow(IEnumerable<Tile>? tiles = null) : base(tiles)
	{
	}

	public static bool AttemptWith(Tile tile, IEnumerable<Tile> otherTiles, out List<List<Block>> foundBlockLists)
	{
		var madeChow = false;
		var otherTilesList = otherTiles.ToList();
		foundBlockLists = new List<List<Block>>();

		if (tile.Suit != Suit.Zi)
		{
			int firstStartingRank;
			if (tile.RawRank <= 2)
			{
				firstStartingRank = 1;
			}
			else if (tile.RawRank >= 8)
			{
				firstStartingRank = 7;
			}
			else
			{
				firstStartingRank = tile.RawRank - 2;
			}
			int lastStartingRank = firstStartingRank > 5 ? 7 : firstStartingRank + 2;
			for (int startingRank = firstStartingRank; startingRank <= lastStartingRank; startingRank++)
			{
				var lowTiles = GetChowTilesByRank(startingRank, tile, otherTilesList);
				var middleTiles = GetChowTilesByRank(startingRank + 1, tile, otherTilesList);
				var highTiles = GetChowTilesByRank(startingRank + 2, tile, otherTilesList);
				if (lowTiles.Count == 0 || middleTiles.Count == 0 || highTiles.Count == 0)
				{
					continue;
				}
				foreach (var lowTile in lowTiles)
				{
					foreach (var middleTile in middleTiles)
					{
						foreach (var highTile in highTiles)
						{
							var unsortedTileList = new List<Tile>(otherTilesList);
							RemoveChowTile(lowTile, tile, unsortedTileList);
							RemoveChowTile(middleTile, tile, unsortedTileList);
							RemoveChowTile(highTile, tile, unsortedTileList);
							foundBlockLists.Add(new List<Block> {
							new Chow(new List<Tile> { lowTile, middleTile, highTile }),
							new UnsortedTiles(unsortedTileList)
						});
							madeChow = true;
						}
					}
				}
			}
		}

		if (!madeChow)
		{
			foundBlockLists.Add(new List<Block> { new UnsortedTiles(otherTilesList.Append(tile)) });
		}
		return madeChow;
	}

	private static List<Tile> GetChowTilesByRank(int desiredRank, Tile tile, List<Tile> otherTiles)
	{
		return tile.RawRank == desiredRank ?
			new List<Tile> { tile } :
			otherTiles.Where(t => t.Suit == tile.Suit && t.RawRank == desiredRank).Distinct().ToList();
	}

	private static void RemoveChowTile(Tile chosenTile, Tile originalTile, List<Tile> unsortedTilelist)
	{
		if (!object.ReferenceEquals(chosenTile, originalTile))
		{
			unsortedTilelist.Remove(chosenTile);
		}
	}
}
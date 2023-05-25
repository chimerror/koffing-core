namespace Koffing;

public class Kong : Meld
{
	public Kong(IEnumerable<Tile>? tiles = null) : base(tiles)
	{
	}

	public static bool AttemptWith(Tile tile, IEnumerable<Tile> otherTiles, out List<Block> foundBlocks)
	{
		foundBlocks = new List<Block>();
		var otherTilesList = otherTiles.ToList();
		var matchingTiles = otherTilesList.Where(t => t.RawEquals(tile)).ToList();
		var nonMatchingTiles = otherTilesList.Where(t => !t.RawEquals(tile)).ToList();
		if (matchingTiles.Count == 3)
		{
			foundBlocks.Add(new Kong(matchingTiles.Append(tile)));
			foundBlocks.Add(new UnsortedTiles(nonMatchingTiles));
			return true;
		}
		else
		{
			nonMatchingTiles.AddRange(matchingTiles);
			nonMatchingTiles.Add(tile);
			foundBlocks.Add(new UnsortedTiles(nonMatchingTiles));
			return false;
		}
	}
}
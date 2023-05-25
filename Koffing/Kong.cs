namespace Koffing;

public class Kong : Meld
{
	public Kong(IEnumerable<Tile>? tiles = null) : base(tiles)
	{
	}

	public static bool AttemptWith(Tile tile, IEnumerable<Tile> otherTiles, out List<List<Block>> foundBlocks)
	{
		foundBlocks = new List<List<Block>>();
		var pungBlocks = new List<Block>();
		foundBlocks.Add(pungBlocks);
		var otherTilesList = otherTiles.ToList();
		var matchingTiles = otherTilesList.Where(t => t.RawEquals(tile)).ToList();
		var nonMatchingTiles = otherTilesList.Where(t => !t.RawEquals(tile)).ToList();
		if (matchingTiles.Count == 3)
		{
			pungBlocks.Add(new Kong(matchingTiles.Append(tile)));
			pungBlocks.Add(new UnsortedTiles(nonMatchingTiles));
			return true;
		}
		else
		{
			nonMatchingTiles.AddRange(matchingTiles);
			nonMatchingTiles.Add(tile);
			pungBlocks.Add(new UnsortedTiles(nonMatchingTiles));
			return false;
		}
	}
}
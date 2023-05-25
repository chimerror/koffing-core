using System.Collections;

namespace Koffing;

public abstract class Block : IEnumerable<Tile>
{
	private List<Tile> _tiles;

	public Block(IEnumerable<Tile>? tiles = null)
	{
		if (tiles != null)
		{
			_tiles = new List<Tile>(tiles);
		}
		else
		{
			_tiles = new List<Tile>();
		}
	}

	public Tile this[int index]
	{
		get => _tiles[index];
	}

	public IEnumerator<Tile> GetEnumerator()
	{
		return _tiles.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}
}
namespace Koffing;

public class Tile : IComparable<Tile>
{
	public Suit Suit { get; set; } = Suit.Man;
	public int Rank { get; set; } = 1;
	public int RawRank
	{
		get => Rank == 0 ? 5 : Rank;
	}

	public Tile(Suit suit = Suit.Man, int rank = 1)
	{
		Suit = suit;
		Rank = rank;
	}

	public override bool Equals(object? that)
	{
		if (that == null)
		{
			return false;
		}

		var thatTile = that as Tile;
		if (thatTile == null)
		{
			return false;
		}

		return (Suit == thatTile.Suit) && (Rank == thatTile.Rank);
	}

	public override int GetHashCode()
	{
		var suitInt = (int)Suit;
		var rankInt = Rank + 1; // Increment so the range is 1-10 instead of 0-9, to keep each suit with unique hashes
		return suitInt ^ rankInt;
	}

	public bool RawEquals(Tile that)
	{
		if (that == null)
		{
			return false;
		}

		return (Suit == that.Suit) && (RawRank == that.RawRank);
	}

	public int CompareTo(Tile? that)
	{
		if (that == null)
		{
			return 1;
		}

		if (this.Suit != that.Suit)
		{
			return this.Suit.CompareTo(that.Suit);
		}
		else if (this.Rank == that.Rank)
		{
			return 0;
		}
		else if (this.Rank == 0)
		{
			return that.Rank <= 5 ? 1 : -1;
		}
		else if (that.Rank == 0)
		{
			return this.Rank <= 5 ? -1 : 1;
		}
		else
		{
			return this.Rank.CompareTo(that.Rank);
		}
	}
}

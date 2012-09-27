namespace GildedRose.Console
{
	public class DoesNotAge : IItemAging
	{
		public int CalculateNewSellIn(int sellIn)
		{
			return sellIn;
		}
	}
}
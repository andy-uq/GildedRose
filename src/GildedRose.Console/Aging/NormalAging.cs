namespace GildedRose.Console
{
	public class NormalAging : IItemAging
	{
		public int CalculateNewSellIn(int sellIn)
		{
			return sellIn - 1;
		}
	}
}
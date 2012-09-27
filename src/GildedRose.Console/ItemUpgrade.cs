using System;
using GildedRose.Console.Quality;

namespace GildedRose.Console
{
	public class ItemUpgrade
	{
		private readonly Func<Item, bool> _match;
		private readonly IItemAging _itemAging;
		private readonly IQualityStrategy _qualityStrategy;

		public ItemUpgrade(Func<Item, bool> match, IItemAging itemAging = null, IQualityStrategy qualityStrategy = null)
		{
			_match = match;
			_qualityStrategy = qualityStrategy ?? new NormalQuality();
			_itemAging = itemAging ?? new NormalAging();
		}

		public bool IsMatch(Item item)
		{
			return _match(item);
		}

		public Item Upgrade(Item item)
		{
			var result = Clone(item);

			result.SellIn = CalculateSellInDate(item.SellIn);
			result.Quality = CalculateQuality(item.SellIn, item.Quality);

			return result;
		}

		private int CalculateSellInDate(int sellIn)
		{
			return _itemAging.CalculateNewSellIn(sellIn);
		}

		private int CalculateQuality(int sellIn, int quality)
		{
			return _qualityStrategy.CalculateQuality(sellIn, quality);
		}

		public Item Clone(Item item)
		{
			return new Item { Name = item.Name, SellIn = item.SellIn, Quality = item.Quality };
		}
	}
}
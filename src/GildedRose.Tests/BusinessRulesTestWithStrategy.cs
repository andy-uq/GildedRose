using System;
using System.Collections.Generic;
using System.Linq;
using GildedRose.Console;
using GildedRose.Console.Quality;
using NUnit.Framework;

namespace GildedRose.Tests
{
	public class BusinessRulesTestWithStrategy : BusinessRulesTest
	{
		private List<ItemUpgrade> _itemUpgrades;
		private ItemUpgrade _defaultStrategy;

		[SetUp]
		public void CreateInstance()
		{
			_defaultStrategy = new ItemUpgrade(item => true);
			_itemUpgrades = new List<ItemUpgrade>
			{
				new ItemUpgrade(_ => _.Name == SULFURAS_HAND_OF_RAGNAROS, new DoesNotAge(), new LegendaryQuality()),
				new ItemUpgrade( _ => _.Name == AGED_BRIE, itemAging: null, qualityStrategy: new QualityIncreases()),
				new ItemUpgrade( _ => _.Name == BACKSTAGE_PASSES, itemAging: null, qualityStrategy: new BackStagePassQuality()),
				new ItemUpgrade( _ => _.Name != null && _.Name.StartsWith("Conjured"), itemAging: null, qualityStrategy: new QualityDegradesTwiceAsFast()),
			};
		}

		protected override Item UpdateItemQuality(Item item)
		{
			foreach ( var x in _itemUpgrades.Where(x => x.IsMatch(item)) )
			{
				return x.Upgrade(item);
			}

			return _defaultStrategy.Upgrade(item);
		}
	}
}
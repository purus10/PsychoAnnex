using UnityEngine;
using System.Collections;
using Database;

public class Shop : MonoBehaviour {

	public enum ShopType {Apothecary,Weapon,General,Jewelry};
	public ShopType Type;
	public int DayofStockChange;
	public int MaxSelection;
	public int StockAmount {get{return Random.Range(1,MaxSelection);}}

	public Item[] Inventory;

	void Start()
	{
		Inventory = new Item[StockAmount];
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Database;

public class WeaponsList : MonoBehaviour {

	public static List <Item> weapons = new List<Item>();
	public string[] Knuckles;
	public string[] Chakram;
	public string[] Katana;
	public string[] Sword;
	public string[] Dagger;
	public string[] Book;
	public string[] HSword;
	public string[] Cane;


	// Use this for initialization
	void Awake () {
		if (weapons.Count == 0)
		{
		Add("Knuckle Blade",1);
		Add("Chakram",2);
		Add("Tachi",3);
		Add("Schiavona",4);
		Add("Book of Shadows",5);
		Add("Spatha",6);
		Add("Royal Cane",7);
		}
		//generateWeapon();
	}
	#region hit table
	int hit (int b,int w)
	{
		//if (w > 7 && w > 0) b = 0;
		switch(w)
		{
		case 1:
			return Random.Range(7,5) + b;
		case 2:
			return Random.Range(6,4) + b;
		case 3:
			return Random.Range(5,3) + b;
		case 4:
			return Random.Range(4,2) + b;
		case 5:
			return Random.Range(3,1) + b;
		case 6:
			return Random.Range(2,1) + b;
		case 7:
			return b;
		case 8:
			return Random.Range(-1,2) + b;
		case 9:
			return Random.Range(-2,1) + b;
		case 10:
			return Random.Range(-3,0) + b;
		case 11:
			return Random.Range(-4,-1) + b;
		case 12:
			return Random.Range(-5,-2) + b;
		case 13:
			return Random.Range(-6,-3) + b;
		default:
			return b;
		}
	}
	#endregion
	#region dmg table
	int dmg (int b, int w)
	{
		//if (w < 7 && w > 0) b = 0;

		switch(w)
		{
		case 1:
			return Random.Range(-6,-3) + b;
		case 2:
			return Random.Range(-5,-2) + b;
		case 3:
			return Random.Range(-4,-1) + b;
		case 4:
			return Random.Range(-3,0) + b;
		case 5:
			return Random.Range(-2,1) + b;
		case 6:
			return Random.Range(-1,2) + b;
		case 7:
			return b;
		case 8:
			return Random.Range(2,1) + b;
		case 9:
			return Random.Range(3,1) + b;
		case 10:
			return Random.Range(4,2) + b;
		case 11:
			return Random.Range(5,3) + b;
		case 12:
			return Random.Range(6,4) + b;
		case 13:
			return Random.Range(7,5) + b;
		default:
			return b;
		}
	}
	#endregion

	void SetBook(Item gen, int h, int d, int b)
	{
		gen.hit = h;
		gen.damage = d;
		gen.max = b;
	}

	void SetWeightandMax(Item gen, int m, int w)
	{
		gen.weight = Random.Range(w-1,w+1);
		if (m <= 5 && m > 1) gen.max = Random.Range(m-1,m);
		else gen.max = m;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			generateWeapon();
		}
	}

	public void generateWeapon()
	{
		Add ("",1);
		Add ("",1);
		Add ("",2);
		Add ("",3);
		Add ("",4);
		Add ("",6);
		Add ("",7);
	}



	public void Add(string Name, int ID)
	{
		Item gen = new Item();
		
		if (ID == 0) ID = Random.Range(1,8);
			
		if (Name == "")
		{
			switch (ID)
			{
			case 1:
				Name = Knuckles[Random.Range(1,Knuckles.Length)];
				break;
			case 2:
				Name = Chakram[Random.Range(1,Chakram.Length)];
				break;
			case 3:
				Name = Katana[Random.Range(1,Katana.Length)];
				break;
			case 4:
				Name = Sword[Random.Range(1,Sword.Length)];
				break;
			case 5:
				Name = Book[Random.Range(1,Book.Length)];
				break;
			case 6:
				Name = HSword[Random.Range(1,HSword.Length)];
				break;
			case 7:
				Name = Cane[Random.Range(1,Cane.Length)];
				break;
			case 8:
				Name = Dagger[Random.Range(1,Dagger.Length)];
				break;
			}
		}
		#region Zen's Weapons
		if (ID == 1)
		{
			if (Name == "Knuckles") SetWeightandMax(gen,2,7);
			if (Name == "Adustum") SetWeightandMax(gen,6,7);
			if (Name == "Dragon Claw")SetWeightandMax(gen,5,9);
			if (Name == "Eagle Talon")SetWeightandMax(gen,3,5);
			if (Name == "Finger Nails")SetWeightandMax(gen,3,9);
			if (Name == "Knuckle Blade")SetWeightandMax(gen,3,7);
			if (Name == "Lady Fingers")SetWeightandMax(gen,5,6);
			if (Name == "Puppeteer")SetWeightandMax(gen,4,10);
			if (Name == "Serpent Fang")SetWeightandMax(gen,4,4);
			if (Name == "Tiger Paw")SetWeightandMax(gen,4,5);
			if (Name == "Ungula") SetWeightandMax(gen,3,9);
			if (Name == "Wolverine") SetWeightandMax(gen,5,5);
			gen.hit = hit(2,gen.weight);
			gen.damage = dmg(2,gen.weight);
		}
		#endregion
		#region Serenity's Weapons
		else if (ID == 2)
		{
			if (Name == "Chakram") SetWeightandMax(gen,2,8);
			if (Name == "Circle of Life") SetWeightandMax(gen,4,8);
			if (Name == "Corona")SetWeightandMax(gen,5,11);
			if (Name == "Ecliptic")SetWeightandMax(gen,3,4);
			if (Name == "Giant's Bracelet")SetWeightandMax(gen,4,8);
			if (Name == "Gold Ring")SetWeightandMax(gen,4,6);
			if (Name == "Ouroboros")SetWeightandMax(gen,5,3);
			if (Name == "Wind & Fire")SetWeightandMax(gen,3,7);
			if (Name == "Zafric's Halo")SetWeightandMax(gen,6,7);
			gen.hit = hit(2,gen.weight);
			gen.damage = dmg(2,gen.weight);
		}
		#endregion
		#region Sky's Weapons
		else if (ID == 3)
		{
			if (Name == "Katana") SetWeightandMax(gen,1,7);
			if (Name == "Tachi") SetWeightandMax(gen,2,7);
			if (Name == "Amakumi") SetWeightandMax(gen,3,10);
			if (Name == "Fudo") SetWeightandMax(gen,4,10);
			if (Name == "Hocho") SetWeightandMax(gen,4,4);
			if (Name == "Honjo") SetWeightandMax(gen,4,8);
			if (Name == "Kiku-Ichimanji") SetWeightandMax(gen,7,1);
			if (Name == "Kotgiri") SetWeightandMax(gen,6,9);
			if (Name == "Kutetsu") SetWeightandMax(gen,3,1);
			if (Name == "Muramasa") SetWeightandMax(gen,7,13);
			if (Name == "Shintago") SetWeightandMax(gen,5,4);
			if (Name == "Wakizashi") SetWeightandMax(gen,3,4);
			gen.hit = hit(3,gen.weight);
			gen.damage = dmg(3,gen.weight);
		}
		#endregion
		#region Hena's Weapons
		else if (ID == 4)
		{
			if (Name == "Sword") SetWeightandMax(gen,2,7);
			if (Name == "Biblo") SetWeightandMax(gen,3,4);
			if (Name == "Colada") SetWeightandMax(gen,6,6);
			if (Name == "Cutlass") SetWeightandMax(gen,3,3);
			if (Name == "Flamberge") SetWeightandMax(gen,5,1);
			if (Name == "Kampilan") SetWeightandMax(gen,5,4);
			if (Name == "Martuary Sword") SetWeightandMax(gen,4,9);
			if (Name == "Rapier") SetWeightandMax(gen,4,2);
			if (Name == "Schiavona") SetWeightandMax(gen,3,7);
			if (Name == "Sinclair Hilt") SetWeightandMax(gen,5,7);
			if (Name == "Walloon") SetWeightandMax(gen,4,3);
			gen.hit = hit(4,gen.weight);
			gen.damage = dmg(0,gen.weight);
		}

		else if (ID == 8)
		{
			if (Name == "Dagger") SetWeightandMax(gen,1,1);
			if (Name == "Stiletto") SetWeightandMax(gen,1,6);
			if (Name == "Bastion Rive") SetWeightandMax(gen,3,1);
			if (Name == "Kalis") SetWeightandMax(gen,1,2);
			if (Name == "Kestrel Dagger") SetWeightandMax(gen,1,3);
			if (Name == "Parade a gauche") SetWeightandMax(gen,3,2);
			if (Name == "Pogniard") SetWeightandMax(gen,2,1);
			if (Name == "Sinistram") SetWeightandMax(gen,2,3);
			if (Name == "Carrwennan") SetWeightandMax(gen,6,7);
			if (Name == "Sword Breaker") SetWeightandMax(gen,3,8);
			gen.hit = hit(3,gen.weight);
			gen.damage = dmg(1,gen.weight);
		}
		#endregion
		#region Rose's Weapons
		else if (ID == 5)
		{// PLace Bonuses Here
			if (Name == "Black Pullet") SetBook(gen,2,0,0);
			if (Name == "Book of Abramelin") SetBook(gen,0,1,1);
			if (Name == "Book of Life") SetBook(gen,1,1,1);
			if (Name == "Book of Rose") SetBook(gen,2,2,2);
			if (Name == "Book of Shadows") SetBook(gen,0,1,1);
			if (Name == "Grand Grimoire") SetBook(gen,1,1,0);
			if (Name == "Key of Solomn") SetBook(gen,2,2,0);
			if (Name == "Necronomicon") SetBook(gen,0,5,-1);
			if (Name == "The King in Yellow") SetBook(gen,3,2,-1);
		}
		#endregion
		#region Ann's Weapons
		else if (ID == 6)
		{
			if (Name == "Heavy Sword") SetWeightandMax(gen,0,10);
			if (Name == "Balmong") SetWeightandMax(gen,6,7);
			if (Name == "Bastard Sword") SetWeightandMax(gen,1,13);
			if (Name == "Claymore") SetWeightandMax(gen,2,11);
			if (Name == "Estoc") SetWeightandMax(gen,1,12);
			if (Name == "Flameschwert") SetWeightandMax(gen,3,11);
			if (Name == "Gram") SetWeightandMax(gen,3,12);
			if (Name == "Kaskara") SetWeightandMax(gen,4,11);
			if (Name == "Spatha") SetWeightandMax(gen,1,7);
			if (Name == "Viking Sword") SetWeightandMax(gen,1,10);
			if (Name == "Zweihander") SetWeightandMax(gen,2,12);
			gen.hit = hit(0,gen.weight);
			gen.damage = dmg(4,gen.weight);
		}
		#endregion
		#region Xeres's Weapons
		else if (ID == 7)
		{
			if (Name == "Cane") SetWeightandMax(gen,2,7);
			if (Name == "Basilisk") SetWeightandMax(gen,5,7);
			if (Name == "Deimos") SetWeightandMax(gen,5,5);
			if (Name == "Domination") SetWeightandMax(gen,6,7);
			if (Name == "Raven Claw") SetWeightandMax(gen,5,9);
			if (Name == "Royal Cane") SetWeightandMax(gen,4,7);
			if (Name == "Villian's Clutch") SetWeightandMax(gen,4,10);
			gen.hit = hit(1,gen.weight);
			gen.damage = dmg(1,gen.weight);
		}
		#endregion
		#region Pistol
		else if (ID == 9)
		{
		}
		#endregion
		gen.type = ID;
		gen.name = Name;

		weapons.Add(gen);
	}
}

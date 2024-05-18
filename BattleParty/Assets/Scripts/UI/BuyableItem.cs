using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyableItem : MonoBehaviour 
{
	public UnityBilling billing;
	public int itemId = 0;
	public Image itemImage;
	public Text itemPrice;

	public void Buy()
	{
		if(billing != null)
		{
			billing.BuyConsumable ();
		}
	}
}

using UnityEngine;

[System.Serializable]
public class CardsInHandData 
{
    public string playerName;
    public CardInfo[] cardsinHand = new CardInfo[6];

    public CardsInHandData()
    {
        playerName = "";
        //cardsinHand = new CardInfo[4];
    }
    
    public CardsInHandData(string playerName, CardInfo[] cardsinHand)
    {
        this.playerName = playerName;
        this.cardsinHand = cardsinHand;
    }
}

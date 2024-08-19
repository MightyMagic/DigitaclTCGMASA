[System.Serializable]
public class CardInfo 
{
    public int Id; 
    public string CardName;

    public CardInfo(int id, string name)
    {
        this.Id = id;
        this.CardName = name;
    }

    public CardInfo()
    {
        this.Id = -1;
        this.CardName = "";
    }
}

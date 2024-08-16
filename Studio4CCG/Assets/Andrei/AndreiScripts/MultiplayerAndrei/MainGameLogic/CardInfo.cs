public class CardInfo 
{
    public int Id; public string Name;

    public CardInfo(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public CardInfo()
    {
        this.Id = -1;
        this.Name = "";
    }
}

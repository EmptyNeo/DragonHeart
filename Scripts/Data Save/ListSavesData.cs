[System.Serializable]
public class ListSavesData 
{
    private int _amount_save;
    public int AmountSave => _amount_save;

    public ListSavesData(int amount_save)
    {
        _amount_save = amount_save;
    }

}
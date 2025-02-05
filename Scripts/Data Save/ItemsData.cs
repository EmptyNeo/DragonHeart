[System.Serializable]
public class ItemsData 
{
    //items
    private float[] _items_x,_items_y;
    private int[] _items_amount;
    private string[] _items_name;

    public float[] ItemsX => _items_x;
    public float[] ItemsY => _items_y;
    public int[] ItemsAmount => _items_amount;
    public string[] ItemsName => _items_name;

    //coins
    private int[] _coins_amount, _coins_id;
    private float[] _coins_x, _coins_y;

    public int[] CoinsAmount => _coins_amount;
    public int[] CoinsId => _coins_id;
    public float[] CoinsX => _coins_x;
    public float[] CoinsY => _coins_y;

    public ItemsData(Item[] items, Coin[] coins)
    {
        int length = items.Length;
        _items_x = new float[length];
        _items_y = new float[length];
        _items_name = new string[length];
        _items_amount = new int[length];

        for(int i = 0; i < length; i++)
        {
            _items_x[i] = items[i].transform.position.x;
            _items_y[i] = items[i].transform.position.y;
            _items_amount[i] = items[i].Amount;
            _items_name[i] = items[i].ItemObject.name;
        }

        length = coins.Length;

        _coins_x = new float[length];
        _coins_y = new float[length];
        _coins_id = new int[length];
        _coins_amount = new int[length];

        for(int i = 0; i < length; i++)
        {
            _coins_x[i] = coins[i].transform.position.x; 
            _coins_y[i] = coins[i].transform.position.y; 
            _coins_id[i] = coins[i].Id;
            _coins_amount[i] = coins[i].Amount;
        }

    }
}
namespace Lab2Algo
{
    public class Element
    {
        public int Key { get; }
        public string Value { get; set; }
        public Element(int key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
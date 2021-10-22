namespace Lab2Algo
{
    public interface IDataBase
    {
        public string Find(int key);
        public void Add(int key, string data);
        public void Delete(int key);
        public void ChangeData(int key, string data);
    }
}
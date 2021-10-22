using System;

namespace Lab2Algo
{
    public class DataBase : IDataBase
    {
        private readonly IndexFiles processor;
        public DataBase(string pathIndex, string pathMain)
        {
            processor = new IndexFiles(pathIndex,pathMain);
        }
        public string Find(int key)
        {
            try
            {
                return processor.Find(key).Value;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Add(int key, string data)
        {
            try
            {
                processor.Add(key, data);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(int key)
        {    
            try{
                processor.Delete(key);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ChangeData(int key, string data)
        {
            try{
                processor.Change(key,data);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
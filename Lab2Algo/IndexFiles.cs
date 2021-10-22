using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;

namespace Lab2Algo
{
    public class IndexFiles
    {
        private readonly BinaryFormatter serializer;
        private readonly string pathIndex;
        private readonly string pathMain;
        private int[] indexArea;
        public Block[] mainArea;

        public IndexFiles(string pathIndex,string pathMain)
        {
            serializer = new BinaryFormatter();
            this.pathIndex = pathIndex;
            this.pathMain = pathMain;
            GenerateAreas();
        }
        private void GenerateAreas()
        {
            //for testing
            //indexArea
            indexArea = new int[10000];
            for (int i = 0, j = 0; i < indexArea.Length; i++)
            {
                if (i % 100 == 99)
                {
                    indexArea[i] = j;
                    j++;
                    continue;
                }
                indexArea[i] = j;
            }
            //writeToIndexArea
            WriteToIndexArea();
            //mainArea
            mainArea = new Block[100];
            for (var i = 0; i < mainArea.Length; i++)
                mainArea[i] = new Block(i);
            //writeToMainArea
            WriteToMainArea();
            
            /*//main
            indexArea = GetIndexArea();
            mainArea = GetMainArea();*/
        }

        public void Change(int key, string data)
        {
            indexArea = GetIndexArea();
            mainArea = GetMainArea();
            //do
            int numberOfBlock = indexArea[key];
            mainArea[numberOfBlock].Change(key,data);
            
            WriteToMainArea();
        }
        public void Delete(int key)
        {
            indexArea = GetIndexArea();
            mainArea = GetMainArea();
            //do
            int numberOfBlock = indexArea[key];
            mainArea[numberOfBlock].Delete(key);
            
            WriteToMainArea();
        }
        public Element Find(int key)
        {
            indexArea = GetIndexArea();
            mainArea = GetMainArea();
            //do
            int numberOfBlock = indexArea[key];
            Element result =  mainArea[numberOfBlock].Find(key);
            if(result == null)
                throw new ArgumentException("Such index doesnt exists");
            return result;
        }
        public void Add(int key , string data)
        {
            if (key < 0)
                throw new ArgumentException("Incorrect index");
            if(key > indexArea.Length - 1)
                RebuildIndexArea(key);
            indexArea = GetIndexArea();
            mainArea = GetMainArea();
            
            int numberOfBlock = indexArea[key];
            mainArea[numberOfBlock].Add(key,data);

            WriteToMainArea();
        }

        private int[] GetIndexArea()
        {
            using (FileStream fs = new FileStream(pathIndex, FileMode.OpenOrCreate))
            {
                return (int[])serializer.Deserialize(fs);
            }
        }

        private Block[] GetMainArea()
        {
            using (var sr = File.OpenText(pathMain))
            {
                return JsonConvert.DeserializeObject<Block[]>(sr.ReadToEnd());
            }
        }
        private void WriteToMainArea()
        {
            using (FileStream fs = new FileStream(pathMain, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);
                var bytes = new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(mainArea));
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        private void WriteToIndexArea()
        {
            using (FileStream fs = new FileStream(pathIndex, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);
                serializer.Serialize(fs,indexArea);
            }
        }
        private void RebuildIndexArea(int key)
        {
            int len = key + 99 - key % 100 + 1;
            indexArea = new int[len];
            for (int i = 0, j = 0; i < indexArea.Length; i++)
            {
                if (i % 100 == 99)
                {
                    indexArea[i] = j;
                    j++;
                    continue;
                }
                indexArea[i] = j;
            }
            WriteToIndexArea();

            Block[] temp = new Block[len/100];
            for (var i = 0; i < temp.Length; i++)
            {
                if (i < mainArea.Length)
                    temp[i] = mainArea[i];
                else
                    temp[i] = new Block(i);
            }
            mainArea = temp;
            WriteToMainArea();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ISCore.Models;
using System.IO;

namespace ISCore
{
    sealed public class Repository<T>: IEnumerable<T>
    {
        private List<T> DataList;
        private String Path;
        public bool IsLoad { get; private set; }

        /// <summary>
        /// Count of items in List
        /// </summary>
        public int Count
        {
            get
            {
                return DataList.Count;
            }
        }
  
        /// <summary>
        /// Default constructor
        /// </summary>
        public Repository()
        {
            DataList = new List<T>();
        }

        /// <summary>
        /// Constructor with params 
        /// </summary>
        /// <param name="path">path to file</param>
        public Repository(String path)
        {
            DataList = new List<T>();
            SetPath(path);
        }

        /// <summary>
        /// Method that set path to file
        /// </summary>
        /// <param name="path">path to file</param>
        public void SetPath(String path)
        {
            Path = path;
        }
        
        /// <summary>
        /// Indexator
        /// </summary>
        /// <param name="index">Index of item that will be returned</param>
        /// <returns>Item that have "index" index</returns>
        public T this[int index]
        {
            get
            {
                return DataList[index];
            }
            set
            {
                DataList[index] = value;
            }
        }

        /// <summary>
        /// Method that add item to List
        /// </summary>
        /// <param name="data">data that will be added</param>
        public void Add(T data)
        {
            DataList.Add(data);
        }

        /// <summary>
        /// Method that delete items from List by Data
        /// </summary>
        /// <param name="data">Data that will be deleted from List</param>
        public void Remove(T data)
        {
            if (DataList.Contains(data))
            {
                DataList.Remove(data);
            }
            else
            {
                throw new Exception("Error. data is not exist in Repository");
            }
        }

        /// <summary>
        /// Method that delete items from List by Id 
        /// </summary>
        /// <param name="id">Id of item that will be deleted</param>
        public void Remove(int id)
        {
            if ((id >= 0) && (id < Count))
                DataList.RemoveAt(id);
            else
                throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Method that save data's to file
        /// </summary>
        public void Save()
        {
            FileStream writeFileStream;
            if (IsLoad)
            {
                writeFileStream = new FileStream(Path, FileMode.Create);
                XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
                formatter.Serialize(writeFileStream, DataList);
                writeFileStream.Close();
            }
            else
            {
                throw new Exception("Data don't loaded!");
            }

        }

        /// <summary>
        /// Method that load data from file
        /// </summary>
        public void Load()
        {
            FileStream readFileStream;
            try
            {
                readFileStream = new FileStream(Path, FileMode.Open);
                XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
                try
                {
                    DataList = (List<T>)formatter.Deserialize(readFileStream);
                }
                catch (Exception)
                {
                    readFileStream.Close();
                    readFileStream = new FileStream(Path, FileMode.Create);
                }
                finally
                {
                    readFileStream.Close();
                }
                
            }
            catch (FileNotFoundException)
            {
                readFileStream = new FileStream(Path, FileMode.CreateNew);
                readFileStream.Close();
            }
            IsLoad = true;
        }

        /// <summary>
        /// Implementation for foreach
        /// </summary>
        /// <returns>Items of List<T></returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach(var item in DataList)
            {
                yield return item;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

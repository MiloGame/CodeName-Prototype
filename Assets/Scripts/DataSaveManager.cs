using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Assets.Scripts
{
    public class DataSaveManager<T>
    {
        //private T _playerData ;
        private string _json;
        private string _savejsonPath;
        private static DataSaveManager<T> _instance;

        private DataSaveManager() { }
        public static DataSaveManager<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataSaveManager<T>();
                }
                return _instance;
            }
        }
        
        //public void Init(ref T data)
        //{
        //    _playerData= data;
        //    Debug.Log("data == _playerData?"+ReferenceEquals(data, _playerData));
            
        //}

        public void SaveData(string foldername,string filename,ref T _playerData)
        {

                _savejsonPath = Path.Combine(Application.dataPath, foldername, filename);
                var directorypath = Path.Combine(Application.dataPath, foldername);
                _json = JsonConvert.SerializeObject(_playerData);
                if (!Directory.Exists(directorypath))
                {
                    Directory.CreateDirectory(directorypath);
                }

                File.WriteAllText(_savejsonPath,_json);
        }

        public bool LoadData(string foldername,string filename,ref T _playerData)
        {
            bool loadsuccess = true;
            string loadpath = Path.Combine(Application.dataPath, foldername, filename);

            if (File.Exists(loadpath))
            {
                string json = File.ReadAllText(loadpath);

                _playerData = JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                //_playerData = default (T);
                loadsuccess = false;
                //Debug.Log("Json File Doesn‘t Exist");
            }
            //Debug.Log("data == _playerData?" + ReferenceEquals(data, _playerData)); 反序列化创建新对象，二者指向不是同一对象
            return loadsuccess;
        }
        public bool LoadData(string loadpath, ref T _playerData)
        {
            bool loadsuccess = true;

            loadpath = Path.Combine(Application.dataPath+loadpath);
            if (File.Exists(loadpath))
            {
                string json = File.ReadAllText(loadpath);

                _playerData = JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                //_playerData = default (T);
                loadsuccess = false;
                ////Debug.Log("Json File Doesn‘t Exist");
            }
            //Debug.Log("data == _playerData?" + ReferenceEquals(data, _playerData)); 反序列化创建新对象，二者指向不是同一对象
            return loadsuccess;
        }
    } 
}
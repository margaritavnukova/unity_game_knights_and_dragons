using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Forms;

namespace прототип1
{
    [Serializable]
    public class Login
    {
        byte[] login;
        int record;
        static readonly int length = 20;

        [NonSerialized]
        static Encoding encoding = Encoding.GetEncoding(1251);

        public Login()   // конструктор по умолчанию
        {
            login = StringToByte("rita", length);
            record = 0;
        }

        public Login(string login)
        {
            this.login = StringToByte(login, length);
            this.record = 0;
        }

        //public Login(string login, int record)
        //{
        //    this.login = StringToByte(login, length);
        //    this.record = record;
        //}

        public int Record { get => record; set => record = value; }

        public string GetLogin => ByteArrayToString(login); 
       

        /// <summary>
        /// преобразование из массива байт в строку
        /// </summary>
        /// <param name="ar">массив байт</param>
        /// <returns>строка</returns>
        private static string ByteArrayToString(byte[] b)
        {
            char[] buff = encoding.GetChars(b);  // раскодирование массива байт
            string result = "";
            for (int i = 0; i < buff.Length; i++)
            {
                result += (char)buff[i];  // добавление каждого символа в строку
            }
            return result;
        }

        /// <summary>
        /// преобразование из строки в массив байт
        /// </summary>
        /// <param name="s">строка переменной длины</param>
        /// <param name=" lengthFild ">фиксированная длина поля</param>
        /// <returns>массив байт фиксированной длины</returns>
        private static byte[] StringToByte(string s, int lengthFild)
        {
            char[] ar = new char[lengthFild];
            for (int i = 0; i < lengthFild; i++)
            {
                if (i < s.Length)
                    ar[i] = s[i];
                // преобразование в символьный массив фиксированного размера
                else
                    ar[i] = ' ';
                // дополнение строки пробелами до нужного размера
            }
            byte[] buff = encoding.GetBytes(ar); // кодирование в массив байт
            return buff;
        }
        

        /// <summary>
        /// Метод добавления данных в конец файла
        /// </summary>
        /// <param name="filename">имя файла</param>
        public void AddNewRecord(string filename)
        {
            FileStream fa = null;
            try
            {
                fa = new FileStream(filename, FileMode.Append);
                BinaryFormatter bw = new BinaryFormatter();
                bw.Serialize(fa, this);
            }
            catch { }
            finally { fa.Close(); } //закрытие потока чтения
        }

        /// <summary>
        /// Метод корректировки отдельной записи (следует с байта num)
        /// </summary>
        /// <param name="filename">имя файла</param>
        /// <param name="num">номер байта, позиция записи</param>
        /// <returns>удалась ли корректировка (true/false)</returns>
        public bool CorrectResult(string filename)
        {
            long num = FindPosition(filename);
            bool result = false;
            if (num >= 0)
            {
                FileStream fw = null;
                try
                {
                    fw = new FileStream(filename, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    fw.Seek(num, SeekOrigin.Begin);
                    // установим указатель файла на запись с позицией num
                    bf.Serialize(fw, this);    
                    // записываем в файл изменения                     
                    result = true;
                }
                catch { }
                finally { fw.Close(); }    //закрытие потока записи         
            }
            return result;
        }

        private long FindPosition(string filename)
        {
            long position = -1;
            long currentPosition = -1;
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();

                Login l = new Login();
                if (fs.Length > 0)
                {
                    while (fs.Position <fs.Length)
                    {
                        position = fs.Position;
                        l = (Login)bf.Deserialize(fs);
                        if (l.login == login)
                        {
                            currentPosition = position;
                            break;
                        }
                    }
                }
            }
            catch { }
            finally { fs.Close(); }             //закрытие потока чтения

            return currentPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="dg"></param>
        public static void RecordToTable(string filename, DataGridView dg)
        {
            List<Login> list = Result(filename);

            //list.Sort();

            for (int i = 0; i < list.Count; i++)
            {
                string login = ByteArrayToString(list[i].login).TrimEnd();
                int record = list[i].record;

                dg.Rows.Add(login, record);
            }
        }

        /// <summary>
        /// Метод добавления данных из файла в список
        /// </summary>
        /// <param name="filename">название файла</param>
        /// <returns>список, содержащий логины</returns>
        static public List<Login> Result(string filename)
        {
            List<Login> list = new List<Login>();
            FileStream fs = null;

            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();

                Login l = new Login();
                if (fs.Length > 0)
                {
                    while ((l = (Login)bf.Deserialize(fs)) != null)
                    {
                        list.Add(l);
                    }
                }
            }
            catch { }
            finally { fs.Close(); }             //закрытие потока записи

            return list;
        }
    }
}
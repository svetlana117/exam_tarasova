using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace экзтарасова
{
    public class Exam
    {
        string s = "";//Переменная путей
        /// <summary>
        /// метод для путей и длины
        /// </summary>
        public void zzz()
        {
            List<Str> Put;//пути
            List<Str> StQ = Input();//исходные данные
            Put = StQ.FindAll(x => x.p1 == StQ[MinElem(StQ)].p1);//запись точки начала
            List<List<Str>> PutFunc = new List<List<Str>>();//лист путей и функций
            foreach (Str rb in Put)//перебор возможных перемещений для построения путей
            {
                CreatePath(StQ, rb);//строятся пути
                PutFunc.Add(Branches(StQ, s));//и ветви
                s = "";
            }
            OutputLog(PutFunc);
            int max = PutFunc[0][0].length, maxind = 0;
            for (int i = 0; i < Put.Count; i++)// подсчет цены путей
            {
                if (LenFunc(PutFunc[i]) >= max)// выбор наибольшего пути
                {
                    max = LenFunc(PutFunc[i]);
                    maxind = i;
                }
            }
            Debug.WriteLine("Максимальный путь " + max);
            Debug.WriteLine("Номер максимального пути " + maxind);
            Output(PutFunc, maxind, max);//Запись выходных данных
            Debug.Listeners.Clear();
        }
        /// <summary>
        /// метод для записи решения
        /// </summary>
        /// <param name="PutFunc"></param>
        /// <param name="maxind"></param>
        /// <param name="max"></param>
        public void Output(List<List<Str>> PutFunc, int maxind, int max)
        {
            using (StreamWriter sr = new StreamWriter(@"вывод.csv", false, Encoding.Default, 10))
            {
                foreach (Str Put in PutFunc[maxind])
                {
                    sr.Write(Put.p1 + " -- " + Put.p2 + ";  (" + Put.length + ") ");
                }
                sr.WriteLine("Длина пути: " + max);
            }
        }
        public void OutputLog(List<List<Str>> PutFunc)
        {
            Debug.WriteLine("Пути: ");
            for (int i = 0; i < PutFunc.Count; i++)
            {
                foreach (Str put in PutFunc[i])
                {
                    Debug.Write(put.p1 + " -> " + put.p2 + ";  (" + put.length + ") ");
                }
                Debug.WriteLine("");
            }
        }
        /// <summary>
        /// структура путей и цены перемещения
        /// </summary>
        public struct Str
        {
            public int p1;
            public int p2;
            public int length;
            public bool Equals(Str obj)
            {
                if (this.p1 == obj.p1 && this.p2 == obj.p2 && this.length == obj.length) return true;
                else return false;
            }
            public override string ToString()
            {
                return p1.ToString() + " -> " + p2.ToString() + " " + length.ToString();
            }
        }
        /// <summary>
        /// поиск начальной точки, ищется самое минимальное из первого столбца, но его не должно быть во втором
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int MinElem(List<Str> StQ)
        {
            int min = StQ[0].p1, minind = 0;
            foreach (Str Put in StQ)
            {
                if (Put.p1 <= min)
                {
                    min = Put.p1;
                    minind = StQ.IndexOf(Put);
                }
            }
            return minind;
        }
        /// <summary>
        /// Поиск конечной точки, по такому же принципу что и начальную точку.
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int MaxElem(List<Str> StQ)
        {
            int min = StQ[0].p2, maxind = 0;
            foreach (Str Put in StQ)
            {
                if (Put.p2 >= min)
                {
                    min = Put.p1;
                    maxind = StQ.IndexOf(Put);
                }
            }
            return maxind;
        }
        /// <summary>
        /// Метод построения пути. Работает рекурсивно.
        /// </summary>
        /// <param name="StQ"></param>
        /// <param name="minel"></param>
        /// <returns></returns>
        public int CreatePath(List<Str> StQ, Str minel)
        {
            int Lenght = 0;
            Str MoveVar = StQ.Find(x => x.p1 == minel.p1 && x.p2 == minel.p2);//Поиск возможных вариантов передвижения
            s += MoveVar.p1.ToString() + "-" + MoveVar.p2.ToString();//Пишем передвижение
            if (MoveVar.p2 == StQ[MaxElem(StQ)].p2)//Смотрим не в конце ли мы
            {
                s += ";";
                return MoveVar.length;
            }
            else
            {
                for (int i = 0; i < StQ.Count; i++)//Ищем стоимость перемещения в ту точку в которую мы пришли
                {
                    if (StQ[i].p1 == MoveVar.p2)
                    {
                        s += ",";
                        Lenght = CreatePath(StQ, StQ[i]) + MoveVar.length;
                    }
                }
            }
            return Lenght;
        }/// <summary>
         /// Чтение из файла
         /// </summary>
         /// <param name="put"></param>
         /// <returns></returns>
        public List<Str> Input()
        {
            Debug.WriteLine("Чтение исходных данных");
            List<Str> StQ = new List<Str>();
            using (StreamReader sr = new StreamReader("ввод.csv"))
            {
                while (sr.EndOfStream != true)
                {
                    string[] s1 = sr.ReadLine().Split(';');
                    string[] s2 = s1[0].Split('-');
                    Debug.WriteLine(s2[0] + " - " + s2[1] + "; " + s1[1]);
                    StQ.Add(new Str { p1 = Convert.ToInt32(s2[0]), p2 = Convert.ToInt32(s2[1]), length = Convert.ToInt32(s1[1]) });

                }
            }
            return StQ;
        }
        /// <summary>
        /// Построение ветвлений и доставляющий в начало первую половину пути до ветвления, подсчет стоимостей.
        /// </summary>
        /// <param name="PutFunc"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<Str> Branches(List<Str> StQ, string s)
        {
            List<List<Str>> LBr = new List<List<Str>>();
            string[] s1 = s.Split(';');
            foreach (string st1 in s1)
            {
                if (st1 != "")
                {
                    LBr.Add(new List<Str>());
                    string[] s2 = st1.Split(',');
                    foreach (string str2 in s2)
                    {
                        if (str2 != "")
                        {
                            string[] str3 = str2.Split('-');
                            LBr[LBr.Count - 1].Add(StQ.Find(x => x.p1 == Convert.ToInt32(str3[0]) && x.p2 == Convert.ToInt32(str3[1])));
                        }
                    }
                }
            }
            foreach (List<Str> l in LBr)
            {
                if (l[0].p1 != StQ[MinElem(StQ)].p1)
                {
                    foreach (List<Str> l1 in LBr)
                    {
                        if (l1[0].p1 == StQ[MinElem(StQ)].p1)
                        {
                            l.InsertRange(0, l1.FindAll(x => l1.IndexOf(x) <= l1.FindIndex(y => y.p2 == l[0].p1)));
                        }
                    }
                }
            }
            int max = LBr[0][0].length, maxind = 0;
            for (int i = 0; i < LBr.Count; i++)
            {
                if (LenFunc(LBr[i]) >= max)
                {
                    max = LenFunc(LBr[i]);
                    maxind = i;
                }
            }
            return LBr[maxind];
        }
        /// <summary>
        /// Подсчет длины пути.
        /// </summary>
        /// <param name="StQ"></param>
        /// <returns></returns>
        public int LenFunc(List<Str> StQ)
        {
            int Lenght = 0;
            foreach (Str rb in StQ)
            {
                Lenght += rb.length;
            }
            return Lenght;
        }




    }
}

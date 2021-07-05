using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace экзтарасова
{
    class Program
    {
        static void Main(string[] args)
        {

            Debug.Listeners.Add(new TextWriterTraceListener(File.CreateText("1.txt")));
            Debug.AutoFlush = true;
            Exam Cpit = new Exam();
            Cpit.zzz();

        }
    }
}

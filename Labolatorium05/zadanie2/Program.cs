using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;




class Program
{

    void Ex2()
    {
        FileWatch fileWatcher = new FileWatch("C:\\PZ2\\Labolatorium05\\zadanie2");
        fileWatcher.Start();
    }

    void Ex3()
    {
        Search search = new Search("./", "test");
        search.Begin();
    }

    void Ex4()
    {
        Manager manager = new Manager(10);
        manager.Start();
        
    }
    private static void Main(string[] args)
    {
        Program program = new Program();
        program.Ex2();
        //program.Ex3();
        //program.Ex4();
        
    }
}
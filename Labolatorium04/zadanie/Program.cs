using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;
using System.Globalization;




/*1.Wczytaj wszystkie dane do czterech kolekcji typu List zawierajÄcych obiekty tych klas.*/

class Reader<T> 
{
    public List<T> readList(String path, Func<String[], T> mapCsvToModel)
    {
        List<T> values = File.ReadAllLines(path).Skip(1).Select(v => mapCsvToModel(v.Split(','))).ToList();
        return values;
    }
}

/*1.Wczytaj wszystkie dane do czterech kolekcji typu List zawierajÄcych obiekty tych klas.*/

class Program
{
    public static void Main(string[] args)
    {
        /*1.Wczytaj wszystkie dane do czterech kolekcji typu List zawierajÄcych obiekty tych klas. */

        List<Region> regions = new Reader<Region>().readList("data/regions.csv", Region.MapCsvToModel);
        List<Territory> territories = new Reader<Territory>().readList("data/territories.csv", Territory.MapCsvToModel);
        List<EmployeeTerritory> employeesTerritories = new Reader<EmployeeTerritory>().readList("data/employee_territories.csv", EmployeeTerritory.MapCsvToModel);
        List<Employee> employees = new Reader<Employee>().readList("data/employees.csv", Employee.MapCsvToModel);

        List<OrderDetail> ordersDetails = new Reader<OrderDetail>().readList("data/orders_details.csv", OrderDetail.MapCsvToModel);
        List<Order> orders = new Reader<Order>().readList("data/orders.csv", Order.MapCsvToModel);

        /*2. [1 punkt] wybierz nazwiska wszystkich pracownikĂłw.*/
        
        System.Console.WriteLine("\n1. Nazwiska pracownikow");
        foreach (Employee employee in employees)
        {
            Console.WriteLine(employee.lastname);
        }
        

        /*3. [1 punkt] wypisz nazwiska pracownikĂłw oraz dla kaĹźdego z nich nazwÄ regionu i terytorium gdzie pracuje. Rezultatem kwerendy LINQ bÄdzie "pĹaska" lista, wiÄc nazwiska mogÄ siÄ powtarzaÄ (ale kaĹźdy rekord bÄdzie unikalny).*/

        System.Console.WriteLine("\n2. Nazwisko | Region | Terytorium gdzie pracuje");
        var query2 = from e in employees
                     join et in employeesTerritories on e.employeeid equals et.employeeid
                     join t in territories on et.territoryid equals t.territoryid
                     join r in regions on t.regionid equals r.regionid
                     select new { S = e.lastname, R = r.regiondescription, T = t.territorydescription };
        foreach (var i in query2)
            Console.WriteLine(i.S + " - " + i.R + ", " + i.T);


        /*4. [1 punkt] wypisz nazwy regionĂłw oraz nazwiska pracownikĂłw, ktĂłrzy pracujÄ w tych regionach, pracownicy majÄ 
        byÄ zagregowani po regionach, rezultatem ma byÄ lista regionĂłw z podlistÄ pracownikĂłw (odpowiednik groupjoin).*/
        
        System.Console.WriteLine("\n3. Nazwa regionu | Nazwiska pracowinków którzy pracują w tych regionach");
        var query3 = from e in (from ee in employees
                                join et in employeesTerritories on ee.employeeid equals et.employeeid
                                join t in territories on et.territoryid equals t.territoryid
                                join r in regions on t.regionid equals r.regionid
                                select new { E = ee, R = r.regiondescription }).Distinct()
                     group e by e.R into g
                     select new { R = g.Key, S = g.Select(x => x.E.lastname).Distinct().ToList() };

        foreach (var i in query3)
            foreach (var j in i.S)
                Console.WriteLine(i.R + " " + j);


        
        /*5. [1 punkt] wypisz nazwy regionĂłw oraz liczbÄ pracownikĂłw w tych regionach.*/
        
        System.Console.WriteLine("\n4. Nazwa regionu | liczba pracowinków pracująca w tych regionach");
        var query4 = from e in employees
                     join et in employeesTerritories on e.employeeid equals et.employeeid
                     join t in territories on et.territoryid equals t.territoryid
                     join r in regions on t.regionid equals r.regionid
                     group e by r into g
                     select new { R = g.Key, C = g.Distinct().Count() };
        foreach (var i in query4)
            Console.WriteLine(i.R.regiondescription + " " + i.C);

        
        //6. Następnie dla każdego pracownika wypisz liczbę dokonanych przez niego zamówień, średnią wartość zamówienia oraz maksymalną wartość zamówienia.
        System.Console.WriteLine("\n5. Liczba zamowien + average value of order + max value of order for every employee");
        var query5 = from e in employees
             join o in orders on e.employeeid equals o.employeeid
             join od in ordersDetails on o.orderid equals od.orderid
             group od by e.employeeid into employeeOrders
             select new {
                employeeid = employeeOrders.Key,
                averagePrice = employeeOrders.Sum(x => double.Parse(x.unitprice, CultureInfo.InvariantCulture) * int.Parse(x.quantity) * (1 - double.Parse(x.discount, CultureInfo.InvariantCulture))) / 
                               employeeOrders.Select(x => x.orderid).Distinct().Count(),
                numberOfOrders = employeeOrders.Select(o => o.orderid).Distinct().Count(),
                highestPrice = employeeOrders.Max(x => double.Parse(x.unitprice, CultureInfo.InvariantCulture) * int.Parse(x.quantity) * (1 - double.Parse(x.discount, CultureInfo.InvariantCulture)))
            };

        foreach (var i in query5)
        {
            Console.WriteLine(i.employeeid + ",     count: " + i.numberOfOrders + ",     avg value: " + i.averagePrice + ",     max value: " + i.highestPrice);
        }




    

    }
}

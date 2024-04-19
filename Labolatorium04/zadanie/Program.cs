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
        System.Console.WriteLine();
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

        Console.WriteLine("\n2. Nazwisko | Region | Terytorium gdzie pracuje");
        var query2 = employees
            .Join(employeesTerritories, e => e.employeeid, et => et.employeeid, (e, et) => new { Employee = e, EmployeeTerritory = et })
            .Join(territories, et => et.EmployeeTerritory.territoryid, t => t.territoryid, (et, t) => new { et.Employee, Territory = t })
            .Join(regions, et => et.Territory.regionid, r => r.regionid, (et, r) => new { et.Employee.lastname, RegionDescription = r.regiondescription, TerritoryDescription = et.Territory.territorydescription });

        foreach (var item in query2)
        {
            Console.WriteLine($"{item.lastname} - {item.RegionDescription}, {item.TerritoryDescription}");
        }      




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
        
        Console.WriteLine("\n4. Nazwa regionu | liczba pracowników pracujących w tych regionach");
        var query4 = employees
            .Join(employeesTerritories, e => e.employeeid, et => et.employeeid, (e, et) => new { Employee = e, EmployeeTerritory = et })
            .Join(territories, et => et.EmployeeTerritory.territoryid, t => t.territoryid, (et, t) => new { et.Employee, Territory = t })
            .Join(regions, et => et.Territory.regionid, r => r.regionid, (et, r) => new { Region = r.regiondescription, Employee = et.Employee })
            .GroupBy(item => item.Region)
            .Select(group => new { Region = group.Key, Count = group.Select(x => x.Employee.employeeid).Distinct().Count() });

        foreach (var item in query4)
        {
            Console.WriteLine($"{item.Region} {item.Count}");
        }


        
        //6. Następnie dla każdego pracownika wypisz liczbę dokonanych przez niego zamówień, średnią wartość zamówienia oraz maksymalną wartość zamówienia.
        System.Console.WriteLine("\n5. Liczba zamowien + srednia wartosc zamowien + maksymalna wartosc zamowien");
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

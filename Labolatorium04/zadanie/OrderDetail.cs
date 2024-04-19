using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;


public class OrderDetail
{
    public string? orderid { get; set; }
    public string? productid { get; set; }
    public string? unitprice { get; set; }
    public string? quantity { get; set; }
    public string? discount { get; set; }

    public static OrderDetail MapCsvToModel(string[] values)
    {
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        double unitprice = double.Parse(values[2], culture);
        int quantity = int.Parse(values[3], culture);
        double discount = double.Parse(values[4], culture);

        return new OrderDetail
        {
            orderid = values[0],
            productid = values[1],
            unitprice = unitprice.ToString(culture),
            quantity = quantity.ToString(),
            discount = discount.ToString(culture)
        };
    }
  

    public override string ToString()
    {
        // Uwzględnij parsowanie wartości numerycznych jako ciągów znaków w metodzie ToString
        return orderid + " " + productid + " " + unitprice.ToString() + " " + quantity.ToString() + " " + discount.ToString();
    }
}


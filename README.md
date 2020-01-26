# Web Services

* **WEB SERVICE 1 – CASE CONVERTER**
Accepts a string and converts it to all UPPER CASE or all lower case.</br>
Prototype for method:</br>
```string CaseConvert(string incoming, unsigned int flag);```</br>
Where incoming is the string to convert, and flag is the operation (1 indicates to upper, 2 indicates to lower) and the return value is the converted string.</br></br>

* **WEB SERVICE 2 – LOAN CALCULATOR**</br>
Accepts the parameters necessary to calculate the amount of a loan payment. </br>
Prototype for method :</br>
```float LoanPayment(float principle, float rate, int payments);```</br>
Where principle is the amount of the loan, rate is the going annual rate for this loan, and payments is the total number of monthly payments to be made on the loan (see [Loan Calculation](http://www.1728.com/loanform.htm) for some background). The assumption here is that all loan payments made in each period of the loan are equal and therefore only one return value is necessary. The return value is the monthly payment (in dollars and cents).</br></br>

* **WEB SERVICE 3 – RESOLVE IP ADDRESS**</br>
Looks up a given IP addresses and returns geographic information. This serves uses another web service. Here is the free web service being called: [IP Service](http://ws.cdyne.com/ip2geo/ip2geo.asmx). Note that this service will sometimes fail due to rate limiting. Additionally, this service takes an extra parameter – alicense key – that you can simply supply as “0” (zero) for testing purposes.</br>
Prototype for method: </br>
```IPInfo GetInfo(string ipAddress);```</br>
Where ipAddress is the IP address of interest, and the return datatype is given as:</br>
```
public struct IPInfo
{
  public string City;
  public string StateProvince;
  public string Country;
  public string Organization;
  public decimal Latitude;
  public decimal Longitude;
}
```

## Authors

* **Billy Parmenter** - *Services 1 & 3* - [billyParmenter](https://github.com/billyParmenter)
* **Mike Ramoutsakis** - *Service 2* - [jrmoca](https://github.com/jrmoca)

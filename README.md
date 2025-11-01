# Sankhya.Mudra.Rupantraan

A lightweight .NET Framework library for real-time currency conversion and exchange rate fetching, leveraging the Frankfurter.dev API. "Sankhya.Mudra.Rupantraan" translates from Sanskrit to "Numerical Currency Transformation," embodying its core purpose.

This package provides a simple and efficient way to integrate currency conversion capabilities into your .NET Framework applications, including VSTO Add-ins for Excel.

## Features

-   **Get All Currencies:** Retrieve a comprehensive list of all supported currency codes and their full names.
-   **Convert Currency:** Easily convert amounts between any two supported currencies based on the latest exchange rates.
-   **No API Key Required:** Utilizes the free Frankfurter.dev API, which does not require an API key for public use.
-   **Reliable:** Built with robust error handling and fallback mechanisms.

## Installation

Install the `Sankhya.Mudra.Rupantraan` NuGet package into your .NET Framework project:

```bash
Install-Package Sankhya.Mudra.Rupantraan
````

Or via the .NET CLI:

```bash
dotnet add package Sankhya.Mudra.Rupantraan
```

## Usage

Here's a quick example of how to use the `CurrencyService` to fetch currencies and perform a conversion:

```csharp
using Sankhya.Mudra.Rupantraan;
using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var currencyService = new CurrencyService();

        // Get all available currencies
        Console.WriteLine("Fetching all currencies...");
        var currencies = await currencyService.GetCurrenciesAsync();
        foreach (var currency in currencies)
        {
            Console.WriteLine($"{currency.Key}: {currency.Value}");
        }

        // Convert 100 USD to INR
        decimal amountToConvert = 100;
        string fromCurrency = "USD";
        string toCurrency = "INR";

        Console.WriteLine($"\nConverting {amountToConvert} {fromCurrency} to {toCurrency}...");
        decimal convertedAmount = await currencyService.ConvertCurrencyAsync(
            fromCurrency, 
            toCurrency, 
            amountToConvert
        );

        Console.WriteLine($"{amountToConvert} {fromCurrency} = {convertedAmount:F2} {toCurrency}");
    }
}
```

## API Details

This library relies on the [Frankfurter.dev API](https://www.frankfurter.app). All exchange rates are quoted against the Euro (EUR) by default and are sourced from the European Central Bank.

## Contribution

Feel free to open issues or submit pull requests on the GitHub repository if you find any bugs or have suggestions for improvements.

## License

This project is licensed. Check the repo for license.

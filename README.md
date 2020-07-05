# CryptoSharp

This is a complete cryptocurrency backtesting platform that I developed as my final thesis project for University of West Attica.

## Platform architecture
![Platform architecture](https://i.imgur.com/BLqhU8U.png)

## Main components are:
- .Net Core API backend
- WinForms based frontend that communicates with the backend
- CryptoCore: a library that is used in both the backend and the frontend to provide the same core functionality to both applications

## Features
- C# strategy scripting
- Script editor with C# syntax highlighting
- Live compile-time script error checking
- Add indicators directly to the chart
- Market prices and candlestick data directly from Binance
- Ability to choose any candlestick interval (5 min, 15 min, 2 hours, etc)
- Visual representation of buys and sells on the chart

## Tech used
- CS-Script (for C# code execution)
- DevExpress components (used on the frontend)
- SQLite (caches candlestick data)
- TA-Lib (technical indicators)
- FastColoredTextBox (script editor component)
- RestSharp (HTTP requests to communicate with the backend)
- Newtonsoft Json (json parser)

## Running
To compile and run, after restoring all packages from nuget, set default Binance API Key and Secret in CryptoCore.Classes.Globals.DEFAULT_BINANCE_KEY and CryptoCore.Classes.Globals.DEFAULT_BINANCE_SECRET.

Default user login is:
User: admin
Pass: 123

First you run *CryptoAPI server* (the easiest way is _dotnet CryptoAPI.dll_) and afterwards you run CryptoFront.exe, which is the frontend.



## Screenshots
#### .Net Core API server (Backend)
![.Net Core API server](https://i.imgur.com/y3NRmAs.png)

#### CryptoFront (Frontend)
![CryptoFront](https://i.imgur.com/aGwVKDK.png)

#### Sample partial C# Strategy
![Sample C# Strategy](https://i.imgur.com/rFv4X39.png)

#### Indicator Editor
![Indicator Editor](https://i.imgur.com/lTrFJIg.png)

#### Chart with a SMA and Bollinger Band indicators
![Chart with technical indicators](https://i.imgur.com/geYG3J4.png)

#### Strategy Options
![Strategy Options](https://i.imgur.com/T1UDc9B.png)

#### Chart after backtesting of a strategy
![Chart after backtest](https://i.imgur.com/PCzAV8v.png)

#### Backtest results
![Backtest result](https://i.imgur.com/21SuHCL.png)


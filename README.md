# PowerFX Trade Copier

PowerFX Trade Copier is a trade automation and synchronization platform built with **ASP.NET Core**, following **Clean Architecture** and Domain-Driven Design (DDD) principles.

The platform integrates with **Telegram** to receive and process trading signals, automatically replicating trade executions across multiple connected **cTrader accounts**. It also features a signal tracking system inspired by platforms like TraderWaves, allowing users to monitor signal performance, track trade progress, and view trading results in real time.

Designed with scalability, maintainability, and reliability in mind, the system leverages modern backend technologies such as **SignalR** for real-time communication and **Redis** for caching and performance optimization.

## Key Features

- **Telegram Signal Integration** – Receive and process trading signals from Telegram groups and channels.
- **Automated Trade Copying** – Replicate trading signals and executions across multiple connected cTrader accounts.
- **cTrader Integration** – Connect and manage multiple cTrader trading accounts.
- **Signal Tracking** – Monitor active, completed, and cancelled signals, including their trade progression and outcomes.
- **Take-Profit Monitoring** – Track multiple take-profit levels, stop-loss events, and signal status updates.
- **Trading Performance Analytics** – Track trading results, signal statistics, and historical performance.
- **Real-Time Updates** – Leverage SignalR for live signal and trade updates.
- **Scalable Architecture** – Built using Clean Architecture and DDD principles for maintainability and extensibility.

## Technology Stack

- **Backend:** ASP.NET Core
- **Architecture:** Clean Architecture, Domain-Driven Design (DDD)
- **Real-Time Communication:** SignalR
- **Caching:** Redis
- **Trading Platform:** cTrader
- **Signal Source:** Telegram

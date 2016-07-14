# Connect4Web
### A Connect 4 implementation on a modern web stack

## Demo
http://connect4web.azurewebsites.net/

## Objective
To implement a lightweight algorithm of the classic Connect Four game satisfying unit and acceptance tests, exposed with a simple REST API and visualised in a SPA web front-end.

## Technology Stack
* .NET 4.6.1
* C# 6.0
* ASP.NET MVC5 / WEB API2
* Owin
* Autofac
* Serilog
* NUnit
* Aurelia
* NPM and JSPM package managers
* Bootstrap
* Animate.CSS
* Backstretch

## Noteable Features

### Single Page Application
The SPA design removes page reloads and helps makes API interactions fast and responsive.

### Sessionful
ASP.NET sessions (tracked via cookies) retain the state of the player's board even with a full page refresh and single page application reboot. This elapses when the session times-out or the application pool is recycled. The board state is discarded when a game concludes via a win or a draw.

## Future Enhancements
* Select board size on landing page.
* Integrate Swagger for API testing and self-documentation.
* Front-end Javascript unit tests.
* Responsive layout.

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
* Selectable board dimensions on landing page.
* Integrate Swagger for API testing and self-documentation.
* Front-end Javascript unit tests.
* Responsive layout.
* Port to ASP.NET Core.

## Build Instructions
1. `git clone https://github.com/Craigology/connect4web`
2. Build solution in VS2015.
3. Run tests (requires ReSharper or other test runner support for NUnit).
4. Install NPM with [NodeJS](https://nodejs.org/en/) or [Chocolatey](https://chocolatey.org/packages/nodejs.install).
5. From command prompt (assumes NPM is in %PATH%):
  1. `cd src/Connect4.Web`
  1. `npm install`
  1. `npm install -g jspm`
  1. `jspm install -y`
  1. `npm install -g gulp`
  1. `gulp build`
6. Debug/launch site in VS2015.

# Skill Point
*transfered from gitlab

Skill Point is a web application with multiple brain, skill and cognitive mini-games 

- Backend - ASP.NET using Entity Framework and REST API 
- Frontend - Vue.js with Bootstrap

## General
- JWT authentication (authentication and refresh tokens)
- PostgreSQL database
- Automatic docker container setup for database and application server
- XUnit tests (Unit test and happy flow integration tests) using Mock and in memory database
- Architecture: Database layer, Data Access Layer (repositories), Business Logic Layer (services), API endpoints
- Automatic DTO mapping between every layer
- Swagger documentation
- Multi language (i18n) support (not implemented)

## Features
- Game results display
- Multiplayer support. Players can create lobby and join via lobby token. After the game each player results are shown in lobby menu
- Intastant replay option

<p align="center">
  <img src="/screenshots/mainpage.jpeg" width='75%' height='75%'>
</p>

## Minigames
### Typing
A Classical typing test which measures your typing skills and speed in WPM
<p align="center">
  <img src="/screenshots/typinganimation.gif" width='75%' height='75%'>
</p>

### Reaction
Measures your reaction speed in MS
<p align="center">
  <img src="/screenshots/reactionanimetaion.gif" width='75%' height='75%'>
</p>

### Number memory
Try to remember as number sequence
<p align="center">
  <img src="/screenshots/numbermemotyanimation.gif" width='75%' height='75%'>
</p>

![Build succeeded][build-shield]
![Test passing][test-shield]
[![Issues][issues-shield]][issues-url]
[![Issues][closed-shield]][issues-url]
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![License][license-shield]][license-url]

# Intelligent house Blazor
#### H5 Blazor 3 group project
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>

- [Case](#case)
- [Requirements](#requirements)
  - [Optional Requirements](#optional-requirements)
- [Architecture diagram](#architecture-diagram)
- [Summary and rundown](#summary-and-rundown)
- [Folder Structure](#folder-structure)
- [API Endpoints](#api-endpoints)
- [Permissions](#permissions)
- [MQTT Topics](#mqtt-topics)
- [Libraries](#libraries)
- [License](#license)
- [Contact](#contact)
</details>


# Case
Build a Blazor server (or webassembly) web application for the [Intelligent House](https://github.com/Thoroughbreed/H5_Embedded_Project)

- Home page for alarm
- Page for log with 4 log-levels
- Page for climate with 3 tabs for 3 rooms


<p align="right">(<a href="#top">back to top</a>)</p>


# Requirements
- [x] Henter data fra ~~Thingspeak~~ og præsenterer dem i grafer
  - [x] Vi henter data fra vores database da vi ikke bruger Thingspeak
- [x] Der skal være mulighed for at bestemme start og sluttidspunkter for data
- [x] Der skal være mulighed for at kunne styre en servo eller lignende via MQTT (MQTT.NET)

- [ ] Der laves et lokalt webApi, der ~~jævnligt henter data fra ThingSpeak og~~ gemmer dem i en database, f.eks. SQLite.
  - [x] Api henter data fra vores eksiterende database
- [x] Mobil App'en skal tilpasses, således at den nu henter sine data fra det lokale WebApi vha. en krypteret TLS forbindelse
- [ ] ~~WebApi benytter en background service til at hente data fra Thingspeak.~~
  - [x] Vi bruger ikke thingspeak

## Optional Requirements
- [x] Der laves sikkerhed på Blazor applikationen med Auth0 eller egen Duende Authorizationserver.
  - [x] Kun authentikerede brugere med Read permission må kunne hente data fra WebApi.
  - [ ] Brugere med Write permission skal også kunne slette gamle data i WebApi

<p align="right">(<a href="#top">back to top</a>)</p>


# Architecture diagram
![architecture diagram](/Docs/Architecture_Diagram.drawio.png)

<p align="right">(<a href="#top">back to top</a>)</p>


#  Summary and rundown
The **IntelliHouse2000** is a all-in-one microcontroller combo that provides climate control, monitoring and alarm/entry functions for the entire house. You can have sensors in all rooms, and set the parameters for each sensor.
If an event is triggered while the alarm is armed, no apparent function will happen in the house, but the log will be updated and the user will get a message<sup>1</sup> with the event, timestamp and what sensor triggered it. 
If however the alarm is disarmed (that is, the user is home) **IntelliHouse2000** will take action on the event.
All of this is then displayed on the Blazor for the **IntelliHouse2000**, **IntelliHouse2000Blazor** with a easy to use UI for userfrendlynes.

> If you forget to turn off your car in the garage, and the sensor detects rising CO<sub>2</sub> levels, the user will be warned, displays around the house will show the event, and the garage door will open incrementally until the sensor value returns to normal

> If the humidity in the house rises rapidly, the appropriate window will be opened incrementally until the sensor detects a drop in humidity. As an extra function<sup>2</sup> you can add weather sensor as well, so the window *doesn't* open if the humidity outside is higher than inside, or it rains.

> No matter what action have been taken (open doors, windows etc.) those will automatically close when the alarm system is armed. This happens with both perimeter and full arm.

<p align="right">(<a href="#top">back to top</a>)</p>


# Folder Structure

![Folder structure](/Docs/FolderStructure.png)

We use Helpers, Models, Services, i18ntext and Pages

And use FBF (Folder By Feature)


<p align="right">(<a href="#top">back to top</a>)</p>


# API Endpoints
**OPS:** Full api documentaiton can be found on [swagger](https://mqtt-api.tved.it/swagger/index.html)

Base URL : https://mqtt-api.tved.it/

| Method | Uri           | Description                                                            | Parameters     | Parameter formats                    |
| :----- | :------------ | :--------------------------------------------------------------------- | :------------- | :----------------------------------- |
| GET    | /             | Smoke test                                                             |                |                                      |
| GET    | /all          | Get last 50 logs                                                       |                |                                      |
| GET    | /info         | Get last 50 logs with log level info                                   |                |                                      |
| GET    | /debug        | Get last 50 logs with log level debug                                  |                |                                      |
| GET    | /system       | Get last 50 logs with log level system                                 |                |                                      |
| GET    | /critical     | Get last 50 logs with log level critical                               |                |                                      |
| GET    | /kitchen      | Get all temperature and humidity readings from the kitchen since ts    | ts (timestamp) | Datetime format: yyyy-MM-dd HH:mm:ss |
| GET    | /kitchen/1    | Get last temperature and humidity reading from the kitchen             |                |                                      |
| GET    | /bedroom      | Get all temperature and humidity readings from the bedroom since ts    | ts (timestamp) | Datetime format: yyyy-MM-dd HH:mm:ss |
| GET    | /bedroom/1    | Get last temperature and humidity reading from the bedroom             |                |                                      |
| GET    | /livingroom   | Get all temperature and humidity readings from the livingroom since ts | ts (timestamp) | Datetime format: yyyy-MM-dd HH:mm:ss |
| GET    | /livingroom/1 | Get last temperature and humidity reading from the livingroom          |                |                                      |
| GET    | /airq         | Get all air quality readings since ts                                  | ts (timestamp) | Datetime format: yyyy-MM-dd HH:mm:ss |
| GET    | /airq/1       | Get last air quality reading                                           |                |                                      |

# Permissions 
| Group name   | Description                                                  |
| :----------- | :----------------------------------------------------------- |
| IntelliRead  | Vuew Critical and Info logs, as well as all climate data     |
| IntelliWrite | Arm or disarm the alarm, set target temperature and humidity |
| IntelliAdmin | View System and User Logs                                    |

<p align="right">(<a href="#top">back to top</a>)</p>

# MQTT Topics
| Topics                               | Access   | Method  |
| :----------------------------------- | :------- | :------ |
| home/alarm/arm                       | External | Pub/Sub |
| home/alarm/alarm                     | External | Sub     |
| home/alarm/alarm                     | Internal | Pub     |
| home/climate/status/#                | External | Sub     |
| home/climate/status/[section]/[type] | Internal | Pub     |
| home/climate/servo                   | External | Pub     |
| home/log/[logLevel]/[type]           | Internal | Pub     |
| home/log/#                           | External | Sub     |

<p align="right">(<a href="#top">back to top</a>)</p>


# Libraries
| Name                                  | Version |
| :------------------------------------ | :------ |
| Auth0.AspNetCore.Authentication       | 1.0.4   |
| Blazored.Toast                        | 3.2.2   |
| Microsoft.Owin.Host.SystemWeb         | 4.2.2   |
| Microsoft.Owin.Security.Cookies       | 4.2.2   |
| Microsoft.Owin.Security.OpenIdConnect | 4.2.2   |
| MQTTnet                               | 3.1.1   |
| MQTTnet.Extensions.ManagedClient      | 3.1.1   |
| MySql.EntityFrameworkCore             | 6.0.7   |
| Radzen.Blazor                         | 4.3.4   |
| Toolbelt.Blazor.HotKeys               | 13.0.0  |
| Toolbelt.Blazor.I18nText              | 12.0.0  |

<p align="right">(<a href="#top">back to top</a>)</p>

# License
* Hardware: CC-BY-LA (Creative Commons)
* API: GPLv3
* Frontend: GPLv3
* Mobile: GPLv3

<p align="right">(<a href="#top">back to top</a>)</p>


# Contact
- Peter Hymøller - peterhym21@gmail.com
  - [![Twitter][twitter-shield-ptr]][twitter-url-ptr]
- Nicolai Heuck - nicolaiheuck@gmail.com
- Jan Andreasen - jan@tved.it
  - [![Twitter][twitter-shield]][twitter-url]

Project Link: [https://github.com/Thoroughbreed/H5_Embedded_Project](https://github.com/Thoroughbreed/H5_Embedded_Project)

<p align="right">(<a href="#top">back to top</a>)</p>


<sup>1</sup> - Informs the user via mobile app over the MQTT protocol

<sup>2</sup> - Function not built in yet

<sup>3</sup> - Logs via MQTT to a database in the following layers: Debug, Info, Critical.




<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[build-shield]: https://img.shields.io/badge/Build-passed-brightgreen.svg
[test-shield]: https://img.shields.io/badge/Tests-passed-brightgreen.svg
[contributors-shield]: https://img.shields.io/github/contributors/Thoroughbreed/H5_Intelli_Blazor.svg?style=badge
[contributors-url]: https://github.com/Thoroughbreed/H5_Intelli_Blazor/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Thoroughbreed/H5_Intelli_Blazor.svg?style=badge
[forks-url]: https://github.com/Thoroughbreed/H5_Intelli_Blazor/network/members
[issues-shield]: https://img.shields.io/github/issues/Thoroughbreed/H5_Intelli_Blazor.svg?style=badge
[closed-shield]: https://img.shields.io/github/issues-closed/Thoroughbreed/H5_Intelli_Blazor?label=%20
[issues-url]: https://github.com/Thoroughbreed/H5_Intelli_Blazor/issues
[license-shield]: https://img.shields.io/github/license/Thoroughbreed/H5_Intelli_Blazor.svg?style=badge
[license-url]: https://github.com/Thoroughbreed/H5_Intelli_Blazor/blob/master/LICENSE
[twitter-shield]: https://img.shields.io/twitter/follow/andreasen_jan?style=social
[twitter-url]: https://twitter.com/andreasen_jan
[twitter-shield-ptr]: https://img.shields.io/twitter/follow/peter_hym?style=social
[twitter-url-ptr]: https://twitter.com/peter_hym

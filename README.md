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
	- [Optionel Requirements](##Optionel-Requirements)
- [Architecture diagram](#architecture-diagram)
- [Summary and rundown](#summary-and-rundown)
- [Folder Structure](#Folder-Structure)
- [API Calls](#API-Calls)
- [MessageCenter](#MessageCenter)
- [MQTT Topics](#mqtt-topics)
- [Libraries](#libraries)
- [App Pages](#App-Pages)
- [License](#license)
- [Contact](#contact)
</details>


# Case
Build a Blazor server (or webassembly) web application for the [Intelligent House](https://github.com/Thoroughbreed/H5_Embedded_Project)

- Home page for alarm
- Page for log with 3 tabs for 3 log-levels <!-- NH_TODO: Update when Jan merges log page into develop -->
- Page for climate with 3 tabs for 3 rooms


<p align="right">(<a href="#top">back to top</a>)</p>


# Requirements
- [x] Henter data fra ~~Thingspeak~~ og præsenterer dem i grafer
  - [x] Vi henter data fra en database da vi ikke bruger Thingspeak
- [x] Der skal være mulighed for at bestemme start og sluttidspunkter for data
- [x] Der skal være mulighed for at kunne styre en servo eller lignende via MQTT (MQTT.NET)

- [ ] Der laves et lokalt webApi, der ~~jævnligt henter data fra ThingSpeak og~~ gemmer dem i en database, f.eks. SQLite.
  - [x] Api henter data fra vores eksiterende database
- [x] Mobil App'en skal tilpasses, således at den nu henter sine data fra det lokale WebApi vha. en krypteret TLS forbindelse
- [ ] ~~WebApi benytter en background service til at hente data fra Thingspeak.~~
  - [x] Vi bruger ikke thingspeak

## Optional Requirements
- [ ] Der laves sikkerhed på Blazor applikationen med Auth0 eller egen Duende Authorizationserver.
  - [ ] Kun authentikerede brugere med Read permission må kunne hente data fra WebApi.
  - [ ] Brugere med Write permission skal også kunne slette gamle data i WebApi

<p align="right">(<a href="#top">back to top</a>)</p>


# Architecture diagram
![architecture diagram](/Docs/Architecture_Diagram.drawio.png)

<p align="right">(<a href="#top">back to top</a>)</p>


#  Summary and rundown
The **IntelliHouse2000** is a all-in-one microcontroller combo that provides climate control, monitoring and alarm/entry functions for the entire house. You can have sensors in all rooms, and set the parameters for each sensor.
If an event is triggered while the alarm is armed, no apparent function will happen in the house, but the log will be updated and the user will get a message<sup>1</sup> with the event, timestamp and what sensor triggered it. 
If however the alarm is disarmed (that is, the user is home) **IntelliHouse2000** will take action on the event.
All of this is then displayed on the App for the **IntelliHouse2000**, **IntelliHouse2000App** with a easy to use UI for userfrendlynes.
> If you forget to turn off your car in the garage, and the sensor detects rising CO<sub>2</sub> levels, the user will be warned, displays around the house will show the event, and the garage door will open incrementally until the sensor value returns to normal

> If the humidity in the house rises rapidly, the appropriate window will be opened incrementally until the sensor detects a drop in humidity. As an extra function<sup>2</sup> you can add weather sensor as well, so the window *doesn't* open if the humidity outside is higher than inside, or it rains.

> No matter what action have been taken (open doors, windows etc.) those will automatically close when the alarm system is armed. This happens with both perimeter and full arm.

<p align="right">(<a href="#top">back to top</a>)</p>


# Folder Structure

![Folder structure](/Docs/FolderStructure.png)

We use MVVM, Services, Repositories and Helpers 
<!-- NH_TODO: Change -->

And uses FBF (Folder By Feature)


<p align="right">(<a href="#top">back to top</a>)</p>


# API Calls
<!-- NH_TODO: Create proper api documentaiton -->

Base URL : https://mqtt-api.tved.it/

- /
- /all
- /info
- /debug
- /system
- /critical
- /kitchen
- /bedroom
- /livingroom
- /airq

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
| Name                             | Version |
| :------------------------------- | :------ |
| Blazored.Toast                   | 3.2.2   |
| MQTTnet                          | 3.1.1   |
| MQTTnet.Extensions.ManagedClient | 3.1.1   |
| Radzen.Blazor                    | 4.3.4   |
| Toolbelt.Blazor.HotKeys          | 13.0.0  |
| Toolbelt.Blazor.I18nText         | 12.0.0  |

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

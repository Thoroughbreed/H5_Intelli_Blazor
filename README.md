![Build succeeded][build-shield]
![Test passing][test-shield]
[![Issues][issues-shield]][issues-url]
[![Issues][closed-shield]][issues-url]
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![License][license-shield]][license-url]

1. Der laves en Blazor applikation med følgende krav:
- Henter data fra Thingspeak og præsenterer dem i grafer
- Der skal være mulighed for at bestemme start og sluttidspunkter for data
- Der skal være mulighed for at kunne styre en servo eller lignende via MQTT (MQTT.NET)

2. Der laves et lokalt webApi, der jævnligt henter data fra ThingSpeak og gemmer dem i en database, f.eks. SQLite.
- Mobil App'en skal tilpasses, således at den nu henter sine data fra det lokale WebApi vha. en krypteret TLS forbindelse
- WebApi benytter en background service til at hente data fra Thingspeak.

3. Der laves sikkerhed på Blazor applikationen med Auth0 eller egen Duende Authorizationserver.
- Kun authentikerede brugere med Read permission må kunne hente data fra WebApi.
- Brugere med Write permission skal også kunne slette gamle data i WebApi


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

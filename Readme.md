# Description
The "OpenGeoDB" project is offering a broad range of data enriched with geolocations. There is, for example, a list of all German zip codes and their related cities, including latitudes and longitudes. See `OpenGeoDB/Resources/PLZ.tab` in project source code.
# Screens
### Cities list screen
Contains a list of cities, ordered alphabetically.

![Cities List iOS](https://github.com/Prin53/OpenGeoDB/blob/master/Screens/ScreenCitiesiOS.png "Cities List iOS")
![Cities List Android](https://github.com/Prin53/OpenGeoDB/blob/master/Screens/ScreenCitiesAndroid.png "Cities List Android")
### Zip codes list screen
Contains a list of zip codes for selected city.

![Zip Codes List iOS](https://github.com/Prin53/OpenGeoDB/blob/master/Screens/ScreenZipCodesiOS.png "Zip Codes List iOS")
![Zip Codes List Android](https://github.com/Prin53/OpenGeoDB/blob/master/Screens/ScreenZipCodesAndroid.png "Zip Codes List Android")
### Zip code screen
Contains a detail information about selected zip code. Also, contains a list of nearby zip codes.

![Zip Code iOS](https://github.com/Prin53/OpenGeoDB/blob/master/Screens/ScreenZipCodeiOS.png "Zip Code iOS")
![Zip Codes Android](https://github.com/Prin53/OpenGeoDB/blob/master/Screens/ScreenZipCodeAndroid.png "Zip Code Android")
# Configuration
You can configure some behaviors in the application using `Configuration` class.
* Count of visible nearby zip codes can be changed via `NearbyZipCodesCount` property.
*  To perform an update from the data source (.tab file) every time when an application starts you can set `ShouldForceUpdate` property to `true`.
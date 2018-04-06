# ThisGuyVThatGuy - Xamarin Forms app

This is a Xamarin Forms app to guess which player has a higher FanDuel Points Per Game (FPPG)
It was developed for Android on a Samsung S8 running 7.0 but with some minor changes could run on an iOS device as well
(need to add a provisioning profile). It could also run on a Windows 10 device running the latest anniversary update.

To build and run the app you need Visual Studio 2017 (or Visual Studio for Mac) with the Xamarin tools and Android SDK installed.
If you don't have that I've included an .apk at /ThisGuyVThatGuy/ThisGuyVThatGuy.Android/

I've written some UITests and Unit Tests which are in their own projects. These will run in Debug for Android, just build the app in Debug mode and thy should appear in the test explorer. You'll need to the iOS and UWP project in the configuration manager if you want to test those projects.

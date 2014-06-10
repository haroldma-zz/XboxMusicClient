# Xbox Music Portable .NET Client

## About Microsoft's Xbox Music Service

http://music.xbox.com/developer
Bring music to your app.
Unlock a new dimension in sound using the power of Xbox Music.

Reference documentation for the Xbox Music Service is available on MSDN: http://go.microsoft.com/?linkid=9840933 .

## About this solution

This solution provides a portable C# client to interface with Microsoft's Xbox Music Service.
It targets the following minimum platforms:
* .NET Framework 4.5
* Windows Store apps (Windows 8)
* Windows Phone 8
* Silverlight 5

API features include:
* Catalog search, lookup and browsing
* New releases and tops
* Preview streaming
* Public playlist lookup
* Images and deep-links

Other API features limited by the terms of the http://music.xbox.com/developer/pilot program are:
* Full track streaming for Xbox Music Pass subscribers
* Collection and playlist browsing and editing

## Sample code

Test projects include sample code.

## Getting started

Start by registering an Azure Data Market client ID and secret. See http://music.xbox.com/developer.

Create a client
```csharp
IXboxMusicClient client = XboxMusicClientFactory.CreateXboxMusicClient("MyClientId", "YourClientSecretYourClientSecretYourSecret=");
```

Use null to get your current geography.
Specify a 2 letter country code (such as "US" or "DE") to force a specific country.
```csharp
string country = null;
```

Search for albums in your current geography
```csharp
ContentResponse searchResponse = await client.SearchAsync(Namespace.music, "Foo Fighters", filter: SearchFilter.Albums, maxItems: 5, country: country);
Console.WriteLine("Found {0} albums. Here are the first 5:", searchResponse.Albums.TotalItemCount);
foreach (Album albumResult in searchResponse.Albums.Items)
{
    Console.WriteLine("{0}", albumResult.Name);
}
```

The console output is:

    Found 49 albums. Here are the first 5:
    Greatest Hits
    Foo Fighters
    There Is Nothing Left To Lose
    Wasting Light
    The Colour And The Shape

List tracks in the first album
```csharp
Album album = searchResponse.Albums.Items[0];
ContentResponse lookupResponse = await client.LookupAsync(album.Id, extras: ExtraDetails.Tracks, country: country);
```

Display information about the album
```csharp
album = lookupResponse.Albums.Items[0];
Console.WriteLine("Album: {0} (link: {1}, image: {2})", album.Name, album.GetLink(ContentExtensions.LinkAction.Play), album.GetImageUrl(800, 800));
foreach (Contributor contributor in album.Artists)
{
    Artist artist = contributor.Artist;
    Console.WriteLine("Artist: {0} (link: {1}, image: {2})", artist.Name, artist.GetLink(), artist.GetImageUrl(1920, 1080));
}
foreach (Track track in album.Tracks.Items)
{
    Console.WriteLine("Track: {0} - {1}", track.TrackNumber, track.Name);
}
```

The console output is:

    Album: Greatest Hits (link: http://music.xbox.com/Album/E9D50C02-0100-11DB-89CA-0019B92A3933?partnerID=XboxMusicClientTest&action=Play, image: http://musicimage.xboxlive.com/content/music.E9D50C02-0100-11DB-89CA-0019B92A3933/image?locale=en-GB&w=800&h=800)
    Artist: Foo Fighters (link: http://music.xbox.com/Artist/9C2A0000-0200-11DB-89CA-0019B92A3933?partnerID=XboxMusicClientTest, image: http://musicimage.xboxlive.com/content/music.9C2A0000-0200-11DB-89CA-0019B92A3933/image?locale=en-GB&w=1920&h=1080)
    Track: 1 - All My Life
    Track: 2 - Best Of You
    Track: 3 - Everlong
    Track: 4 - The Pretender
    Track: 5 - My Hero
    Track: 6 - Learn To Fly
    Track: 7 - Times Like These
    Track: 8 - Monkey Wrench
    Track: 9 - Big Me
    Track: 10 - Breakout
    Track: 11 - Long Road To Ruin
    Track: 12 - This Is A Call
    Track: 13 - Skin And Bones
    Track: 14 - Wheels
    Track: 15 - Word Forward
    Track: 16 - Everlong (Acoustic Version)

## License

Copyright (c) Microsoft Corporation
All rights reserved. 

Licensed under the Apache License, Version 2.0 (the ""License""); you may
not use this file except in compliance with the License. You may obtain a
copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY
IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
MERCHANTABLITY OR NON-INFRINGEMENT. 

See the Apache Version 2.0 License for specific language governing
permissions and limitations under the License.

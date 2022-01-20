


**Page Rank**

An application that returns the page rankings (position of a URL) for a website using Google or Bing. 

Please note that searches are not performed against live Google/Bing pages. They are searched against saved pages that reside on AWS. The pages reside in the following prefixes
https://webscraping101.s3.ap-southeast-2.amazonaws.com/google
https://webscraping101.s3.ap-southeast-2.amazonaws.com/bing
can be accessed as per below (append a Page[01-05].html) to the urls
https://webscraping101.s3.ap-southeast-2.amazonaws.com/google/Page01.html
https://webscraping101.s3.ap-southeast-2.amazonaws.com/bing/Page01.html

I've created this application to demonstrate the use of Clean Architecture in modern software development.

Requirements:

 - Return the page numbers where a URL appears in Google or Bing. If multiple positions are found, return the result as a comma separated string ie 1,2,3
 - Return 0 if no position is found
 - Keyboard is just to demonstrate form bindings in Angular. It's just for visual purposes




Requirements:
- Node v16
- .NET Core 3.1 SDK

Some notes
- Please ensure PageRank.Api is running before running the client
- The angular client is located under /Client. Please run "npm install" followed by "npm run" to start the client. 
- The client can be accessed via port 4200 - http://localhost:4200
- I've included 2 implementations for the webscraping portion of the project. A Regex and a HAP (Html Agility Pack) parser. The default parsers can be changed in SearchResultParserFactory.
- The client has been configured to send requests to http://localhost:5000 (Kestrel's default HTTP port). This can be changed in /client/src/environments/environment.ts.

Some points that I would have liked to improve
- Upgrade the backend to .NET 6
- Upgrade the frontend to Angular 13
- Further refine the UI
- Add Docker support
- Add better url validations
- Add distributed caching
- Add better logging (through Serilog with ELK)
- Enable SSL
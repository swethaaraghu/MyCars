**MyCars App**

        o	Application user here is referred as Customer. 
        o	Each Customer is given with unique customer name or user name.
        o	Customer can add any number of Vehicle.
        o	Customer can Edit, View the vehicle added by them.
        o	Search a vehicle (which was added by them) based on VIN/Driver/ License Plate.
        o	View vehicle details and location on map in dashboard.
        
**Steps to run Application:**

        o	Get all files from the both MyCars and MyCarsClient GitHub repositories
        o	Run both server(MyCars) and client(MycarsClient) solution at same time.
        o	Use Login Credentials – 
              1.	CustomerName - Raghupathy, Password – ‘123456’
              2.	CustomerName – Swethaa, Password – ‘123456’
              3.	CustomerName – Test, Password – ‘test’
        Note: New Login Credentials can be added in CustomerLoginData.json in EntityData folder in MyCars solution.
        
**Server Application - MyCars (.net Core 3.1)  **  

        GitHub Repository - https://github.com/swethaaraghu/MyCars
        o	API are created here using c#.Net.
       
**Client Application – (.netcore 3.1, JavaScript)**

        GitHub Repository - https://github.com/swethaaraghu/MycarsClient
        o	Built using Html, CSS, JavaScript and jQuery.
        o	API calls are made through jQuery Ajax.
        o	Used Mapbox for map and vehicle clustering.

**Data**

      o	Application is built without database support.
      o	Data is handled using json files in EntityData folder.
      o	However, I created a basic table script and stored procedures.
      GitHub repository - https://github.com/swethaaraghu/DBSCRIPTS
 


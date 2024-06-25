Namespace ControlsDemo.Helpers

    Public Module WizardDemoHelper

        Public VeryLongText As String = "With the new DevExpress WinForms and ASP.NET grids, the inherent performance limitations associated with large datasets are a thing of the past. By delegating server intensive operations to the database server, DevExpress LINQ-enabled grid products make what was previously far too time consuming or worse, impossible, possible again.


Back to Basics - Grouping, Sorting, Filtering, Summary Computations 

With the release of Outlook 97, Microsoft introduced to the masses an entirely new way in which to deliver information to end-users within a grid control. Component vendors such as us then released components which allowed developers to build Windows® and ASP.NET applications that mimicked the capabilities of Outlook 97's grid. Over the last 10+ years, countless individuals have come to rely on the grouping/sorting/summary computation capabilities of this new grid metaphor on the Windows and ASP.NET platforms.


The Power of an Outlook® Style Grid 

The real strength of the Outlook style grid lies in its ability to organize information for the end-user and report on that information in an effective manner. In a traditional 2 dimensional grid, a user would not have the luxury to analyze the information displayed on screen. Assume for a moment that a grid is used to display sales information. Old style 2-D grids do not allow the user to group sales information by region and to better understand the data being presented to them. But when using an Outlook style grid, the user is free to group and summarize information by any column...giving them the productivity tools needed to get their job done instantly without generating complex sales reports.


Size Matters - Handling Large Datasets 

The UI power available in an Outlook® Style grid, however, comes at a cost. That cost is dataset size. Large datasets in an ASP.NET application impact the usability of the application. When it comes to this modern grid UI, users will invariably want to analyze information and they will rarely understand why a grid performs well with a 100 records and fails with 100,000 records. To illustrate, let's continue with our previous example. Assume a developer builds a web UI that displays sales data within a grid control and during testing with 100 sales records, the web server and the components used to build the web page perform admirably. The developer then delivers the solution to market and the customer is elated by the new UI.

As the customer begins adding information to the database and the dataset size grows, problems take shape. Grouping, summary computation, sorting, and navigation speed start to bog down. The problem worsens over time and eventually the developer is left with only a single option – to restrict the number of records being rendered on screen.

The developer then delivers a modified solution to the customer and the customer asks a very logical question...Why am I not able to group and summarize sales information for my business over the last year? So what if my database has 500,000 records in it? Why can't I just see the information on screen without having to wait 2 minutes to get incorrect results?


Compromise is Not the Answer – the ASPxGridView and XtraGrid Are 

Outlook style grids are extremely powerful but this power can only be realized if the grid control can consume data effectively. If this is not true...if the grid should only be used to display limited datasets, then why bother using an Outlook style grid?

When we chose to design our grid controls for WinForms and ASP.NET, foremost in our minds was performance and optimum memory use against large datasets. Our reasoning was simple – whether a grid displays 1 record or a million, the server should respond instantly and give the end-user the means with which to operate his business without unwanted roadblocks and hurdles.


How it All Works 

Data processing is a critical feature of all grid controls. A well designed data controller frees the web server and the client from onerous tasks and allows it to perform at an optimum level regardless of server/client load by offloading database server specific chores to the database server. Data processing within the ASPxGridView and the XtraGrid is managed by our LINQ enabled Data Controller.


Let the Database Server Do What it Does Best 

No matter how well one designs a data controller, it will never do its job well if one fails to recognize that database specific operations ought to be executed on the database server. No matter how ingenious the algorithms – no matter how brilliant the technology...if the grid is forced to manage data itself, you can bet that a large dataset will eventually bring the server or the Windows Smart Client to its knees and make the application totally unusable.


Don't Kill the Web Server or the Windows Client 

The obvious question one might ask at this point is why – why should a large dataset, hundreds of users, and the need to group/sort/navigate records throughout the business day impact the application in such a massive way. The answer is simple: With ASP.NET and Windows Smart Clients, most grid controls need the entire dataset to be loaded and processed for every operation...be it a trivial operation such as record navigation from one page to the next or complex operations such as data grouping. For ASP.NET, it's the web server that is forced into this position by competing grid controls and it is the web server that has to allocate the necessary resources to keep the site running. For Windows Smart Clients, the network becomes the bottleneck as thousands upon thousands of rows of records are read into memory from the database server.

At the end of the day, that's why developers resort to filtering result sets – they need the web server and/or the Windows Smart Client to function and not fail.
Enough with Crippling Limitations 

The new LINQ-enabled ASPxGridView and XtraGrid confront the limitations we've outlined here head-on and have been engineered to free you from the hassles you otherwise would be forced to workaround.

Instead of reading the entire dataset from the data server and then managing data within the grid, our controls simply display data that has already been grouped or sorted on the data server. This is possible because of our built-in LINQ-enabled providers . This provider can produce smart queries so that all the grid needs to do is download records to be displayed within the current page. If you have 100,000 records in your data source and want to display 10 records on the page, the grid will need to download only 10 records rather than the 100,000 records required with each request/query when using competing grid controls. This means that with the ASPxGridView and XtraGrid, what was once simply impossible with competing grids (but entirely needed by end-users) can now be easily accomplished."
    End Module
End Namespace

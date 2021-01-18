CREATE DATABASE lachlanbarclaynet
go
USE [lachlanbarclaynet]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 18/01/2021 12:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostDescription] [nvarchar](255) NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[PostText] [nvarchar](max) NULL,
	[PostTypeID] [int] NOT NULL,
	[PostTitle] [nvarchar](255) NOT NULL,
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[PostUrl] [nvarchar](255) NULL,
	[Published] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostComment](
	[PostCommentId] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[PostCommentDate] [datetime] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[IsVisible] [bit] NULL,
	[PostID] [int] NULL,
 CONSTRAINT [PK_PostComment] PRIMARY KEY CLUSTERED 
(
	[PostCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostType](
	[PostTypeID] [int] NOT NULL,
	[PostTypeName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_PostType] PRIMARY KEY CLUSTERED 
(
	[PostTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NULL,
	[Password] [nvarchar](255) NULL,
	[QrCode] [nvarchar](255) NULL,
	[Attempts] [int] NOT NULL,
	[LockedOutUntil] [datetime] NULL,
	[UserEmail] [nvarchar](255) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[EmailConfirmCode] [nvarchar](50) NULL,
	[PasswordForgotCode] [nvarchar](50) NULL,
	[IsQuizAdmin] [bit] NOT NULL,
	[DoNotEmail] [bit] NOT NULL,
	[AboutMe] [nvarchar](1000) NULL,
	[NerdCred] [decimal](9, 2) NULL,
	[AvgScore] [decimal](9, 2) NULL,
	[QuizzesCompleted] [decimal](9, 2) NULL,
	[SpeakerID] [int] NULL,
	[QrCodeTemp] [varchar](32) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Post] ADD  DEFAULT ((1)) FOR [Published]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Attempts]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [EmailConfirmed]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsQuizAdmin]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [DoNotEmail]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD FOREIGN KEY([PostTypeID])
REFERENCES [dbo].[PostType] ([PostTypeID])
GO
ALTER TABLE [dbo].[PostComment]  WITH CHECK ADD  CONSTRAINT [FK_PostComment_Post] FOREIGN KEY([PostID])
REFERENCES [dbo].[Post] ([PostID])
GO
ALTER TABLE [dbo].[PostComment] CHECK CONSTRAINT [FK_PostComment_Post]
GO
INSERT [dbo].[PostType] ([PostTypeID], [PostTypeName]) VALUES (1, N'technical')
GO
INSERT [dbo].[PostType] ([PostTypeID], [PostTypeName]) VALUES (2, N'music')
GO
INSERT [dbo].[PostType] ([PostTypeID], [PostTypeName]) VALUES (3, N'other')
GO
insert into Users (USername, password, qrcode) values ('admin', null, null)
go
SET IDENTITY_INSERT [dbo].[Post] ON 
GO
INSERT [dbo].[Post] ([PostDescription], [PostDate], [PostText], [PostTypeID], [PostTitle], [PostID], [PostUrl], [Published]) VALUES (N'Error logging, application info logs and warnings all using Serilog and Seq.', CAST(N'2018-07-17T12:56:49.000' AS DateTime), N'<style type="text/css">
    .hljs { background: #F5F5F5; color: #555555 }
    .hljs-string { color: #A31515; }
    .hljs-keyword { color: #0000FF;font-weight:normal; }
    .hljs-title { color: #2B91AF; }
    .prenoborder { border: none !important; background-color: white }
    pre { margin-bottom: 2em; }
</style>

<p>I am unabashed in how awesome I think <a href="https://getseq.net/">Seq</a> is. For troubleshooting stuff in production, this thing is a goldmine. Let''s say I have some code:</p>

<pre><code class="cs hljs">
public int Divide(int x, int y)
{
    int z = x / y;
    return z;
}

int result = Divide(10, 5);

</code></pre>

<p>With Seq it''s extremely easy to add some logging that gives you all the information you need to troubleshoot this function when it''s running in production:</p>

<img src="https://static.lachlanbarclay.net/pics/SeqLogging1.png" class="img-responsive" />

<p>I won''t go into any more detail about Seq, you can <a href="https://getseq.net/">read about it yourself</a>, so here''s how I set it up.</p>

<h3>Setting up Serilog and Seq for Logging</h3>

<p>We are going to use <a href="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1">ASP.NET Core''s standard logging</a>, <a href="https://serilog.net/">Serilog</a> as the implementation, and <a href="https://getseq.net/">Seq</a> as a <a href="https://github.com/serilog/serilog-sinks-seq">serilog sink</a>. This gives us the most flexibility!</p> 

<p>First off you only need to add <b>two</b> NuGet packages:</p>

<pre>
Install-Package <a href="https://github.com/serilog/serilog-sinks-seq">Serilog.Sinks.Seq</a>
Install-Package <a href="https://github.com/serilog/serilog-aspnetcore">Serilog.AspNetCore</a>
</pre>

<p>Once you have done that, open your Program.cs class and change your Main function to look something like this:</p>

<pre><code class="cs hljs">
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using Serilog;
using Serilog.Events;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MyApp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            // yes this is deliberately outside of the try catch because if this
            // fails there''s no point in calling the logger :)

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.With(new LogEnricher())
                .WriteTo.Seq("myseqserver.com:5341", apiKey: "MyAppsApiKey")
                .CreateLogger();
                    
            try
            {
                Log.Information("Starting up My Project");

                BuildWebHost(args).Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "My Project terminated unexpectedly");
                return 1;
            }
            finally
            {
                // need to flush and close the log otherwise we might miss some
                Log.CloseAndFlush();
            }
        }
        
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseSerilog()
                    .Build();
    }
}
</code></pre>

<p>If you want to customise the Properties that are automatically added, you will need my custom LogEnricher. But you don''t <b>need</b> this.</p>

<pre><code class="cs hljs">
class LogEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent le, ILogEventPropertyFactory lepf)
    {
        // these properties are created by asp.net core, I don''t need them
        // so I''m going to remove them

        le.RemovePropertyIfPresent("SourceContext");
        le.RemovePropertyIfPresent("RequestId");
        le.RemovePropertyIfPresent("RequestPath");
        le.RemovePropertyIfPresent("ActionId");
        le.RemovePropertyIfPresent("ActionName");

        // however I definitely want to know machine name for each log entry:

        le.AddPropertyIfAbsent(lepf.CreateProperty("MachineName", Environment.MachineName));
    }
}
</code></pre>

<h3>Writing a log</h3>

<p>To write a log within a class, you can inject it into your controller (or any other class) by adding it to the constructor:</p>


<pre><code class="cs hljs">
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyApp
{
    [Route("")]
    public class MathsController : Controller
    {
        private ILogger&lt;MathsController&gt; _log;

        public MathsController(ILogger&lt;MathsController&gt; log)
        {
            _log = log;
        }

        [HttpGet("v1/divide")]
        public int Divide(int x, int y)
        {
            int z = x / y;
            _log.LogInformation("Dividing {x} by {y} gives us {z}", inputs.x, inputs.y, z);
            return z;
        }
    }
}
</code></pre>

<p>You <b>don''t have to use dependency injection</b>. However it''s quite useful if you want to write a unit test for the Divide function, but you don''t want it writing a log every time you run the unit test. In that case, you can just pass through an implementation of ILogger that doesn''t do anything, using the great <a href="https://github.com/Moq/moq4/wiki/Quickstart">Moq</a> library:</p>

<pre><code class="cs hljs">
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MathsTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestDivide()
        {
            var logger = new Mock&lt;Logger&lt;MathsController>>();
            var mathsController = new MathsController(logger.Object);

            var result = mathsController.Divide(10, 2);

            Assert.IsTrue(5, result);
        }
    }
}
</code></pre>

<p>But having said that, if you don''t want to use dependency injection, or if you wish to use Serilog''s more fancy features, you can directly access the Serilog.Log singleton:</p>

<pre><code class="cs hljs">
Serilog
    .Log
    .ForContext("Current Base Directory", AppDomain.CurrentDomain.BaseDirectory)
    .Information("Dividing {x} by {y} equals {z}", x, y, z);
</code></pre>

<h3>Global Error Handling</h3>

<p>For WebApi methods, you can implement a global exception handler by creating your own Middleware like so:</p>

<pre><code class="cs hljs">

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context /* other dependencies */)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Serilog
                .Log
                .ForContext(new ExceptionEnricher(context))
                .Error(ex, "Global error {Message}", ex.Message);
        }
    }
}

</code></pre>

<p>and then to use it, add it before your call to .UseMvc() inside your app''s Startup.cs:</p>

<pre><code class="cs hljs">
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.UseMiddleware(typeof(ErrorHandlingMiddleware));
    app.UseMvc();
}
</code></pre>

<p>Which means you''ll get some lovely error handling in your logs:</p>

<img src="https://static.lachlanbarclay.net/pics/SeqLogging2.png" class="img-responsive" />

<h3>Request Input Validation and Logging</h3>

<p>If you wish to automatically validate inputs using DataAnnotations and log any validation errors, you can create your own ActionFilter like so:</p>

<pre><code class="cs hljs">
public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            Serilog
                .Log
                .ForContext(new BadRequestEnricher(context.ModelState))
                .Warning("Bad Request to {Path}, bad fields: {NumberOfBadFields}",
                    context.HttpContext.Request.Path, context.ModelState.ErrorCount);

            // return a 400 Bad Request result
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}

class BadRequestEnricher : ILogEventEnricher
{
    ModelStateDictionary _modelState;
    public BadRequestEnricher(ModelStateDictionary modelState)
    {
        _modelState = modelState;
    }

    public void Enrich(LogEvent le, ILogEventPropertyFactory lepf)
    {
        foreach (var key in _modelState.Keys)
        {
            string message = _modelState[key].Errors[0]?.ErrorMessage;
            le.AddPropertyIfAbsent(
                lepf.CreateProperty("Invalid" + key, message ));
        }
    }
}
</code></pre>

<p>You can then either add this attribute to your Api or you can add it to run for all routes by editing your Startup.cs:</p>

<pre><code class="cs hljs">
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddMvc(options =>
        {
            options.Filters.Add(typeof(ValidateModelStateAttribute));
        })
</code></pre>

<p>This should mean that if you use Data Annotaions on your input objects like so:</p>

<pre><code class="cs hljs">
    public class DivideInputDTO
    {
        [Range(0, 100)]
        public int x { get; set; }
        public int y { get; set; }
    }
    
    [HttpGet("v1/divide")]
    public int Divide(DivideInputDTO inputs)
    {
        int z = inputs.x / inputs.y;
        _log.LogInformation("Dividing {x} by {y} gives us {z}", inputs.x, inputs.y, z);
        return z;
    }
</code></pre>

<p>(BTW can you spot the <b>"bug"</b>? Let me give you a minute...)</p>

<p>...</p>

<p>(Yes, there is no validation on the y property! Divide by zero is still possible!)
<br />
<p>Anyway, you will now log lovely validation messages:</p>

<img src="https://static.lachlanbarclay.net/pics/SeqLogging3.png" class="img-responsive" />

<h3>Is that it?</h3>

<p>I''m quite happy with this setup and it''s been serving me well. What have I missed? I''m sure something else obvious!</p>

<p>Many thanks to <a href="https://twitter.com/_bron_">Bron</a>''s <a href="http://blog.angelwebdesigns.com.au/structured-logging-with-serilog-in-asp-net-core/">serilog in aspnet core</a> article and of course the <a href="https://github.com/serilog/serilog-aspnetcore">aspnet core serilog</a> docs, and also of course Seq''s own <a href="https://docs.getseq.net/docs/using-serilog">using serilog</a> and <a href="https://docs.getseq.net/docs/using-aspnet-core">using aspnet core</a>. 
</p>

', 1, N'Using Seq and Asp Dot Net Core', 5565, N'using-seq-and-asp-dot-net-core', 1)
GO
INSERT [dbo].[Post] ([PostDescription], [PostDate], [PostText], [PostTypeID], [PostTitle], [PostID], [PostUrl], [Published]) VALUES (N'Fixing my Dad''s 1981 Noyce Archtop Guitar. Fixing the active EMG pickup that needed +18v.', CAST(N'2019-08-19T12:54:24.000' AS DateTime), N'<p>This is a photo of my Dad''s 1981 <a href="http://www.noyceguitars.com" target="_blank">Noyce</a> archtop guitar:</p>

<img data-src="https://static.lachlanbarclay.net/pics/archtop/guitar1.png" class="img-responsive lazyload" />

<p>As you can see, it''s a beautiful thing. Sounds amazing and super easy to play. It''s all fantastic, until you go to plug the guitar in, and you see this:</p>

<img data-src="https://static.lachlanbarclay.net/pics/archtop/xlr-output.png" class="img-responsive lazyload" />

<h3>What the heck??</h3>

<p>When the guitar was originally made, an active EMG pickup was installed. Apparently at the time EMG were a pretty new company! I asked on reddit and apparently I needed to look underneath the pickup to see what it was. So after a bit of screwing this is what I found:</p>

<img data-src="https://static.lachlanbarclay.net/pics/archtop/pickup-back.jpg" class="img-responsive lazyload" />

<p>I couldn''t find anything about this pickup online, but I think "A" stands for Alnico. </p>
<p>
Now because the pickup needed two 9v batteries, instead of adding a large cavity to the body of the guitar and risking reduced resonance, way back in 1981 a crafty solution was comprised. </p>

<p>Instead of the usual setup for an active pickup with one volume knob, which should look like this:</p>

<img data-src="https://static.lachlanbarclay.net/pics/archtop/wiring-diagram1.png" class="img-responsive lazyload" />

<p>They decided to get rid of the batteries, and replace the stereo output jack with an XLR output jack:</p>

<img data-src="https://static.lachlanbarclay.net/pics/archtop/wiring-diagram2.png" class="img-responsive lazyload" />

<p>And then run the output from the XLR, via a microphone lead, into a little box that contained the batteries, and converted it to a normal 1/4" output jack:</p>

<img data-src="https://static.lachlanbarclay.net/pics/archtop/wiring-diagram3.png" class="img-responsive lazyload" />

<p>So that''s how everything was wired up.</p>


<h3>The problem</h3>

<p>It was great and worked for about 10 years until the infamous "guitar drop" incident. During a gig the strap came loose, and the back of the guitar came crashing to the floor, causing a massive crack and killing the electronics. He got the crack repaired but not the pickup.</p>

<p>A few months back my Dad asked if I could fix his guitar. I know enough to be dangerous, and I knew that I couldn''t make it any worse, so I''ve spent the past few weeks detectorising all of the the above.</p>

<p>My first thought was perhaps it was just the volume knob. It had an ''interesting'' soldering job:</p>

<img data-src="https://static.lachlanbarclay.net/pics/archtop/volume-knob-wiring.jpg" class="img-responsive lazyload" />

<p>So I bought a replacement pot, soldered it in, but no luck. Still no worky.</p>

<p>I couldn''t think of what else to do, so I took all of the electronics out of the guitar just to see if I could make the pickup work. I soldered the pickup to a new volume knob, put two batteries in series, held the pickup above some guitar strings, and to my great surprise, it worked! Here''s a quick video showing it up and running:</p>

<style>.embed-container { position: relative; padding-bottom: 56.25%; height: 0; overflow: hidden; max-width: 100%; } .embed-container iframe, .embed-container object, .embed-container embed { position: absolute; top: 0; left: 0; width: 100%; height: 100%; }</style><div class=''embed-container''><iframe src=''https://www.youtube.com/embed/f703nLXr4rY'' frameborder=''0'' allowfullscreen></iframe></div>

<br />
<p>So the pickup wasn''t stuffed. Great news. Now to make it work.</p>

<p>My first thought was to get rid of the active pickup! Most archtop guitars have passive pickups, but my Dad really liked the sound of this one so we were stuck with it. I tried running the pickup on 9v, and it worked fine, so I suggested putting in a 9V battery box inside the guitar, but Dad didn''t want any major surgery done.</p>

<p>So to start with, I built a new power box that took a 1/4" stereo jack in and 1/4" jack out:</p>

<img data-src="https://static.lachlanbarclay.net/pics/archtop/newbox.png" class="img-responsive lazyload" />
<img data-src="https://static.lachlanbarclay.net/pics/archtop/powerbox2.jpg" class="img-responsive lazyload" />
<img data-src="https://static.lachlanbarclay.net/pics/archtop/newbox2.png" class="img-responsive lazyload" />

<p>And I ordered a new <a target="_blank"  href="https://luthiersupplies.com.au/les-paul-style-jack-plates">stereo output jack and plate</a> and put them inside the guitar, hooking it up to the new volume knob, and replacing the pickup wiring as well. The output jack looked much better: </p>

<img data-src="https://static.lachlanbarclay.net/pics/archtop/new-output-jack.jpg" class="img-responsive lazyload" />

<p>Then time for the final test:</p>

<style>.embed-container { position: relative; padding-bottom: 56.25%; height: 0; overflow: hidden; max-width: 100%; } .embed-container iframe, .embed-container object, .embed-container embed { position: absolute; top: 0; left: 0; width: 100%; height: 100%; }</style><div class=''embed-container''><iframe src=''https://www.youtube.com/embed/n6yWGZISJzQ'' frameborder=''0'' allowfullscreen></iframe></div>
<br /><br />
<p>What a stroke of luck, it actually works! I almost gave up about 5 times while working on this project, but I got there in the end. Now to start learning how to play jazz :) </p>






', 2, N'Fixing my Dad''s archtop', 5567, N'fixing-my-dads-archtop-guitar', 1)
GO
INSERT [dbo].[Post] ([PostDescription], [PostDate], [PostText], [PostTypeID], [PostTitle], [PostID], [PostUrl], [Published]) VALUES (N'Visual studio''s profiler can make a massive difference to the performance of your application. Here''s how to use it.', CAST(N'2019-08-30T17:18:49.000' AS DateTime), N'<style type="text/css">
 
    .prenoborder { border: none !important; background-color: white }
 </style>

<p>Do you want to optimise your app so it run faster? <a href="https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff647215(v=pandp.10)" target="_blank">Just read this</a>. </p>

<p>But hang on... there''s so much information! You could spend <b>weeks</b> optimising your app for nothing! 

<h3>Stop The Guesswork!</h3>

<p>Most people <b>guess</b> at what''s slow, based on their previous experience. The problem is it''s so easy to be wrong, and you can waste a <b>huge</b> amount of time fixing the wrong stuff. So how do you take the guesswork out and start making an actual, measurable difference? </p>

<p>Let''s take this c# code sample:</p>
                                                                          
                                                                          
                                                                          
                                                                          
                                                                          
                                                                          
                                                                          
                                                                          


<pre><code class="language-csharp">public void WriteClientNamesToDisk(int limit)
{
    for (int i=1;i<=limit;i++)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connstring))
        {
            sqlConnection.Open();
			
            string sql = $"select Name from Clients where ClientID={i}";

            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string clientName = reader.GetString(0);
                        System.IO.File.AppendAllText("outputdata.txt", $"{i}: {clientName}\n");
                    }
                }
            }
        }
    }
}</code></pre>

<p>There''s a few performance problems here. Where would you start to optimise it? What do you think is slow?</p>

<p>Yes, it''s a trick question. There''s <b>no way</b> to tell what''s actually slow in this function without running it! It might be opening the database connection that''s slow. It might be reading the data from the database that''s slow. You might have a slow hard disk, and it might be that suspicious <b>AppendAllText</b> statement.</p>

<p>Let''s take the guesswork out! Can you see this option in visual studio?</p>

<img data-src="https://static.lachlanbarclay.net/pics/performance/AnalyseMenuOption.png" class="img-responsive lazyload" alt="Visual studio analyse menu option" />

<p>Hit it, then tick "Instrumentation" (untick other options first if disabled):</p>


<img data-src="https://static.lachlanbarclay.net/pics/performance/AnalyseInstrumentation.png" class="img-responsive lazyload" alt="Release build!" />


<p>And crucially, make sure your <b>current configuration is set to Release</b>: </p>

<img data-src="https://static.lachlanbarclay.net/pics/performance/AnalyseRelease.png" class="img-responsive lazyload" alt="Release build!" />

<p>Click the Start button, and wait for the profiler to do it''s thing. BTW, sometimes the profiler fails to work. Just run it again.</p>

<p>So I wonder which function is the slowest?</p>

<img data-src="https://static.lachlanbarclay.net/pics/performance/HotPath.png" class="img-responsive lazyload" alt="Visual studio performance profiler CPU usage" />

<p>So our <b>WriteClientNamesToDisk()</b> function is taking up 98% of the execution time. Fair enough, it''s our only function. But hang on, <b>System.IO.File.AppendAllText()</b> is taking 78%? Wow. I wonder which functions are doing the most individual work?</p>

<img data-src="https://static.lachlanbarclay.net/pics/performance/MostBusyFunctions.png" class="img-responsive lazyload" alt="Visual studio performance profiler most busy functions" />

<p>Wow, that <b>AppendAllText()</b> is a killer. Surprisingly enough, the <b>DbConnection.Open()</b> is only taking up 2% of the runtime! Even though we''re doing it in a loop! (I am guessing the connection pooling is really helping us out here). So in other words, if we spent a lot of time optimising the database connections we would only get, at maximum, a 2% speed increase. Wow. If you guessed "It''s obviously the database connections" you were wrong :)  </p>
 
<p>But that''s only half the story. You need to test in as close to production-like environment as possible. What if my production database is actually on the other side of the world & under heavy load? I would expect to see the connections start to slow things down. But that''s just a guess! You need to prove it otherwise you''re wasting your time.</p>

<h3>Checking CPU Usage</h3>
 
<p>Before we do any coding, let''s get some more lower grained stats. Open the Performance Profiler again, and this time check "CPU Usage":</p>

<img data-src="https://static.lachlanbarclay.net/pics/performance/AnalyseMenuOption.png" class="img-responsive lazyload" alt="Visual studio performance profiler CPU usage" />

<p>Kick it off, and what do we get?</p>

<img data-src="https://static.lachlanbarclay.net/pics/performance/LineByLineCpuUsage.png" class="img-responsive lazyload" />

<p>Yep. That <b>AppendAllText</b> is killing us. Let''s move it to be outside of the loop and run the profiler again. What do we get this time?</p> 

<img data-src="https://static.lachlanbarclay.net/pics/performance/Improvements.png" class="img-responsive lazyload" />
<p>Wow! It''s only taking up 5% of total execution time now, what a massive improvement! Of course this has sacrificed memory usage for performance... if memory was at a premium, eg maybe you were running this in an azure function which charged you for memory allocations, maybe this change wouldn''t be so good! But in this case, if we were only pulling back a few hundred clients at a time, I''d say it''s a great trade off.</p>
<p>So looking at the above example, what needs to be improved next? Well, that pesky <b>sqlConnection.Open()</b> is taking up 13% of the runtime. That doesn''t need to be in the loop either. Let''s take it out.</p>
<img data-src="https://static.lachlanbarclay.net/pics/performance/Improvements2.png" class="img-responsive lazyload" />
<p>Not much of a difference there. What about the ExecuteReader command? Maybe that''s slow because we''re constructing a new SqlCommand object each time, what about we take that out of the loop and just set a parameter each time we call it?</p>
<img data-src="https://static.lachlanbarclay.net/pics/performance/Improvements3.png" class="img-responsive lazyload" />
<p>Didn''t seem to make much of a difference here. Possibly still a bit of an improvement long term, as we''re allocation less objects on the stack. But in terms of raw performance, there''s no proof that this fixed anything. </p>
<p>But hang on... why are we performing an individual select statement for each client? Why not pull them all back in one go?</p>
<img data-src="https://static.lachlanbarclay.net/pics/performance/Improvements4.png" class="img-responsive lazyload" />
<p>Wow! The connection open and the append text are taking up 26% of the time.. and reading the data from the database is taking up just 3%! I actually even had to call this function hundreds of times in a loop in order to collect this data, otherwise the percentage was so tiny it was immeasurable compared to the rest of the app - mostly the application startup!</p>

<p>I hope I''ve made my point. Micro optimisations are a massive waste of time. You need to focus where the important stuff is, and you need to look at your broader architecture if you really want to make a difference.</p>

<h3>So wait, what''s slow again?</h3>
<p>Even though I want you to use the profiler like there''s no tomorrow, it''s still helpful to have in the back of your mind what kind of thing will generally slow you down, so that you can avoid writing badly performing code in the first place. So what''s a real killer? Let''s start with the most likely candidates:</p>

<ol>
	<li>Disk Access</li>
	<li>Database Access</li>
	<li>Web Services</li>
</ol>

<p>These are all external systems that are outside of your control. You might be able to write a super fast bit of code that calculates PI to the millionth decimal place, but if you need to ask <a href="https://api.pi.delivery/v1/pi?start=0&numberOfDigits=100">a web service to do this for you</a>... you have no control over how long it might take to finish. Reduce these calls and avoid unless absolutely neccesary!</p>

<h3>So what''s slow in my own c# code?</h3>

<p>Once you''ve got calls to external stuff out of the way, you might need to start looking at your own code. What do you want to avoid?</p>

<ol>
	<li>Reflection</li>
	<li>Exceptions</li>
	<li>Garbage Collection</li>
	<li>Incorrect data types</li>
</ol>

<p>That a good starting point. Garbage collection covers a massive array of things. From the classic string concatenation in a loop, to the abuse of creating <b>List&lt;object&gt;</b>''s everywhere when a simple <b>IEnumerable&lt;object&gt;</b> would do. There''s a whole world of optimisations you can do about reducing the number of objects you allocate, but ultimately it all comes down to a simple fact:</p>

<h2>Fast code is code that doesn''t run</h2>

<p>Do you really need to call the web service again? Can you cache the result and avoid another call? Do you really need to allocate an object inside your function? Can it be moved to a constructor? Do you really need to call <b>.ToList()</b> or can you just use the enumerator? </p>

<p>Now you need to start reading Microsoft''s <a href="https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff647215(v=pandp.10)" target="_blank">Performance Best Practices at a Glance</a>. But don''t do them all unless you need to :)</p>



', 1, N'Optimise your app with Visual Studio''s Profiler', 5568, N'optimise-your-app-with-visual-studios-profiler', 1)
GO
INSERT [dbo].[Post] ([PostDescription], [PostDate], [PostText], [PostTypeID], [PostTitle], [PostID], [PostUrl], [Published]) VALUES (N'I built a quiz web application in asp dot net core using youtube''s player API. How did I do it?', CAST(N'2020-04-22T00:00:00.000' AS DateTime), N'<p>After seeing Jimmy Carr''s <a href="https://www.youtube.com/watch?v=weZmR5l9htk" target="_blank">lockdown quiz</a>, I thought it would be fun to film my own, except make it a bit more nerdy. So I filmed an episode:</p>

<style>.embed-container { position: relative; padding-bottom: 56.25%; height: 0; overflow: hidden; max-width: 100%; } .embed-container iframe, .embed-container object, .embed-container embed { position: absolute; top: 0; left: 0; width: 100%; height: 100%; }</style>

<div class="margin-bottom: 2em">
<div class=''embed-container''><iframe src=''https://www.youtube.com/embed/wEBrRYd0pP0'' frameborder=''0'' allowfullscreen></iframe></div>
</div>
<p>… and then thought… hey, it would be great to have a leaderboard! And an easier way to enter responses on mobile.. and a way of keeping track of which episodes you''ve completed.</p>

<p>So being the nerd I am, I built a little nerd quiz web app. </p>

<h3>Database Design</h3>

<p>So what''s the first step in building a great app? Getting the database schema designed properly. So here''s attempt one:</p>

<img src="https://static.lachlanbarclay.net/pics/quiz/dbdesign1.png" alt="database design attempt 1" class="img-responsive" />

<p>One table for the quizzes. One table for the questions. One table for the answers. About as straightforward as it gets. But then it comes to storing the answers. I decided to have a table named <b>UserQuizAnswer</b>, which stores the particular answer that the user selected, for a particular quiz. This means I can query which questions they got right and wrong, and tally up their points.</p>

<p>After using this structure for a bit I realised that the <b>UserQuizAnswer</b> table is overkill. I don''t really need to know what specific questions they got right or wrong, so for the moment, I''m just storing their overall score for the quiz. So here''s take two:</p>

<img src="https://static.lachlanbarclay.net/pics/quiz/dbdesign2.png" alt="database design attempt 2" class="img-responsive" />

<p>Much simpler, easier to understand, and easier to query and update. Yes we don''t know which questions they got wrong, but hey, I don''t need to know that information. You can work it out for yourself by seeing the answers! :)</p>

<h3>Building the App</h3>

<p>The next step was to build a web app that queries this data and lets you answer the multiple choice questions.</p>

<p>My language of choice is asp.net core, so I added a few pages to my site to pull back the quiz, embed the video and give you a multiple choice questionnaire:</p>

<img src="https://static.lachlanbarclay.net/pics/quiz/webversion1.png" alt="web ui attempt 1" class="img-responsive" />

<p>Nothing clever or particularly interesting going on here. The video is in a fixed position so you can scroll down the questionnaire while watching the video. It worked great on desktop… but on mobile it''s very difficult to use. Keeping the video in place and the quiz answers proved to be a difficult bit of CSS.</p>

<p>So how about fixing the experience for mobile? How about we embed the video at the top, and show the correct multiple choices depending on where the video is currently playing? Is that even possible?</p>

<p>Well it turns out with <a target="_blank" href="https://developers.google.com/youtube/iframe_api_reference">iframe player api</a> you''re able to communicate with the video player and grab the current play position, and do other things like pause and resume playing the video. The code is pretty simple. First you add a div:</p>

<pre><code class="js hljs">
&lt;div id="player"&gt;&lt;/div&gt;
</code></pre>

<p>
Then you add some code that loads the API''s javascript:
</p>

<pre><code class="language-js">
    var tag = document.createElement(''script'');
      tag.src = "https://www.youtube.com/iframe_api";
      var firstScriptTag = document.getElementsByTagName(''script'')[0];
      firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
</code></pre>

<p>This code loads in youtube''s javascript for interacting with the video. This code will call a function named: </p>

<pre><code class="hljs">onYouTubeIframeAPIReady()</code></pre>

<p>That you''ve previously added to the page. Once you''re inside that function, you can load up a video:</p>

<pre><code class="js hljs">
var player;

      function onYouTubeIframeAPIReady() {
        player = new YT.Player(''player'', {
          height: ''390'',
          width: ''640'',
          videoId: ''M7lc1UVf-VE'',
          events: {
            ''onReady'': onPlayerReady,
            ''onStateChange'': onPlayerStateChange
          }
        });
      }
</code></pre>

And you now have a player object that lets you interact with the video. You can play it, pause it, move to a different location, set the volume, and even load in other videos. It also fires a few events like when the user pauses the video. 

Unfortunately there''s no regular event fired when the user is playing a video to detect where they are currently watching (like at 50 seconds in). So the easiest way to do that is good old (horrible) polling:

<pre><code class="js hljs">
videoMonitor = setInterval(function () {

        var videoCurrentTimeInSeconds = player.getCurrentTime();

        console.log("Currently at location: " + videoCurrentTimeInSeconds);

}, 500);
</code></pre>

<p>This bit of code fires twice a second and asks the video for the current play location. Works great:</p>

<img src="https://static.lachlanbarclay.net/pics/quiz/playlocation.gif" alt="play location animated gif" class="img-responsive" />

<p>Beautiful! It''s now a simple matter to work out which question we are up to, and display the correct set of answers:</p>

<pre><code class="js hljs">
var questionLocations = [0, 33, 55, 83];

for (var i = 0; i < questionLocations.length-1; i++) {
	var startTime = questionLocations[i];
	var endTime = questionLocations[i + 1];

	if (videoCurrentTimeInSeconds >= startTime && videoCurrentTimeInSeconds < endTime) {

		var newQuestionID = i + 1;

		if (currentQuestion != newQuestionID) {
			$("#Question" + currentQuestion).hide();
			$("#Question" + newQuestionID).show(400);
			
			currentQuestion = newQuestionID;
		}
	}
}
</code></pre>

<p>And voila, we have a dynamic quiz that you can play on your mobile!</p>

<img src="https://static.lachlanbarclay.net/pics/quiz/quiz2.gif" alt="mobile nerd quiz" class="img-responsive" />

<p>I''ve also added little question buttons that let you skip to a question, in case you missed one. Plus if you don''t answer a question in time, it will pause the video to give you more time to answer - and then once you''ve selected your answer, it unpauses the video. The overall experience is quite nice. </p>

<h3>Bugs</h3>

<p>As usual there were a few problems - the main one was that on the iphone, the video maximises to full screen upon hitting play. This was a problem, but luckily there''s a little workaround for this. You can specify a flag named "<b>playsinline</b>" which stops it from happening. There''s a bunch of different player variables you can pass through that will affect the player. In this example, I''m also disabling the full screen option and also only showing related videos that are on my channel:</p>

<pre><code class="js hljs">
player = new YT.Player(''player'', {
	width: newWidth,
	height: newHeight,
	videoId: youtubeVideoCode,
	playerVars: {
		''playsinline'': ''1'',
		''rel'': ''0'',
		''fs'': ''0''
	},
	events: {
		''onReady'': onPlayerReady,
		''onStateChange'': onPlayerStateChange
	}
});
</code></pre>

<p>Unfortunately there''s no way to not display related videos when it''s paused. It sucks that they''ve done this, and there''s nothing you can do about it. Oh well!</p>

<h3>Automated build and release</h3>

<p>For my site, (lachlanbarclay.net), I''ve been using <a target="_blank"  href="https://github.com/features/actions">github actions</a> to build and deploy my application. The overall experience is really good - it''s fast and it''s reliable. That''s all I care about. It''s actually been great. Upon a code push the changes appear on the site in about 80 seconds. </p>

<h3>What could I do better?</h3>

<p>Overall I''m quite happy with this solution and it''s working well, but of course always love to hear feedback on better approaches! What''s something obvious that I''ve missed?  </p>
', 1, N'Building a quiz application with youtube''s player API', 5570, N'quiz-application-with-youtube-api', 1)
GO
SET IDENTITY_INSERT [dbo].[Post] OFF
GO
CREATE LOGIN lachlanbarclaynetuser WITH PASSWORD = 'lachlanbarclaynetpassword', 
	CHECK_POLICY     = OFF,
    CHECK_EXPIRATION = OFF;
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'lachlanbarclaynetuser')
BEGIN
    CREATE USER lachlanbarclaynetuser FOR LOGIN lachlanbarclaynetuser
    EXEC sp_addrolemember N'db_owner', N'lachlanbarclaynetuser'
END;
GO

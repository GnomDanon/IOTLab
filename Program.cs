using BlazorApp2.Components;
using BlazorApp2.Domain;
using Docker.DotNet;
using Docker.DotNet.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//!!!!!!!
//DockerClient client = new DockerClientConfiguration(
//    new Uri("tcp://172.29.162.127:2375"))
//     .CreateClient();



//await client.Images.CreateImageAsync(
//    new ImagesCreateParameters
//    {
//        FromImage = "nodered/node-red",
//        Tag = "alpha",
//    },
//    new AuthConfig
//    {
//        Email = "gfikbr2013@gmail.com",
//        Username = "royalempobashke",
//        Password = "Hjrth2013"
//    },
//    new Progress<JSONMessage>());

//DockerClient client = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")
//   )
//    .CreateClient();

//var imageParams = new ImagesCreateParameters
//{
//    FromImage = "nodered/node-red",
//    Tag = "latest"
//};

//var authConfig = new AuthConfig
//{
//    Email = "gfikbr2013@gmail.com",
//    Username = "Royalem Po Bashke",
//    Password = "Hjrth2013"
//};

////await client.Images.CreateImageAsync(imageParams, null, new Progress<JSONMessage>(
////    message =>
////    {
////        Console.WriteLine(message.Status);
////    }
////));



/*await M2();
static async Task M2()
{
    DockerClient client = new DockerClientConfiguration().CreateClient();

    var imageParams = new ImagesCreateParameters
    {
        FromImage = "nodered/node-red",
        Tag = "latest"
    };

    //var authConfig = new AuthConfig
    //{
    //    Email = "gfikbr2013@gmail.com",
    //    Username = "royalempobashke",
    //    Password = "Hjrth2013"
    //};

    await client.Images.CreateImageAsync(imageParams, null, new Progress<JSONMessage>(
         message =>
         {
             Console.WriteLine(message.Status);
         }
     ));

    var portToExpose = 1880;

    var a = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
    {
        Image = "nodered/node-red",
        ExposedPorts = new Dictionary<string, EmptyStruct>
        {
            {$"{portToExpose}/tcp", new EmptyStruct()}
        },
        HostConfig = new HostConfig()
        {
            DNS = new[] { "8.8.8.8", "8.8.4.4" },
            PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                    $"{portToExpose}/tcp",
                    new List<PortBinding>{
                                new PortBinding
                                {
                                    HostPort = $"{portToExpose}"
                                }
                            }
                        }
                    },
            ExtraHosts = new[] { "localhost:127.0.0.1" },
        }
    });

    await client.Containers.StartContainerAsync(a.ID, new ContainerStartParameters());
Console.WriteLine(client);  
}*/

// start sequence
Stand stand = new Stand ();
stand.CreateImages();
stand.RunNodeRed();

app.Run();

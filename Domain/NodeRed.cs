using Docker.DotNet;
using Docker.DotNet.Models;

namespace BlazorApp2.Domain
{
    public class NodeRed: IDockerService
    {
        string name = "nodered/node-red";
        public string Name { get { return name; } }
        int port = 1880;
        public int Port { get { return port; } set { port = value; } }
        string Id = "";
        ImagesCreateParameters imageParams = new ImagesCreateParameters
        {
            FromImage = "nodered/node-red",
            Tag = "latest"
        };


        public async Task CreateImage(IDockerClient client)
        {
            await client.Images.CreateImageAsync(imageParams, null, new Progress<JSONMessage>(message =>
            {
                Console.WriteLine(message.Status);
            }));
        }

        public async Task CreateContainer(IDockerClient client)
        {
            var response = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
            {
                Image = "nodered/node-red",
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    {$"{port}/tcp", new EmptyStruct()}
                },
                HostConfig = new HostConfig()
                {
                    DNS = new[] { "8.8.8.8", "8.8.4.4" },
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                    $"{port}/tcp",
                    new List<PortBinding>{
                                new PortBinding
                                {
                                    HostPort = $"{port}"
                                }
                            }
                        }
                    },
                    ExtraHosts = new[] { "localhost:127.0.0.1" },
                }
            });
            Id = response.ID;
        }

       public async Task Start(IDockerClient client) 
        {
            var a = client.Containers.ListContainersAsync(new ContainersListParameters());
            await client.Containers.StartContainerAsync(Id, new ContainerStartParameters());
            Console.WriteLine(a);
        }

        public async Task Stop(IDockerClient client)
        {
            await client.Containers.StopContainerAsync(Id, new ContainerStopParameters
            {
                WaitBeforeKillSeconds = 30
            },
            CancellationToken.None);
        }

        public async Task Kill(IDockerClient client)
        {
           await client.Containers.KillContainerAsync(Id, new ContainerKillParameters());
        }

        public async Task Delete (IDockerClient client)
        {
            await client.Containers.RemoveContainerAsync(Id, new ContainerRemoveParameters() {Force=true });
        }
    }
}

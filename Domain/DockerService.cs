using Docker.DotNet;
using Docker.DotNet.Models;

namespace BlazorApp2.Domain
{
    public abstract class DockerService : IDockerService
    {
        string _id;
        public int Port { get; set; }
        public string Name { get; internal set; }

        public async Task CreateImage(IDockerClient client)
        {
            await client.Images.CreateImageAsync(new ImagesCreateParameters()
            {
                FromImage = Name,
                Tag = "latest"
            }, null, new Progress<JSONMessage>(message =>
            {
                Console.WriteLine(message.Status);
            }));
        }

        public async Task CreateContainer(IDockerClient client)
        {
            var response = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
            {
                Image = Name,
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    {$"{Port}/tcp", new EmptyStruct()}
                },
                HostConfig = new HostConfig()
                {
                    DNS = new[] { "8.8.8.8", "8.8.4.4" },
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                    $"{Port}/tcp",
                    new List<PortBinding>{
                                new PortBinding
                                {
                                    HostPort = $"{Port}"
                                }
                            }
                        }
                    },
                    ExtraHosts = new[] { "localhost:127.0.0.1" },
                }
            });
            _id = response.ID;
        }

        public async Task Start(IDockerClient client)
        {
            await client.Containers.StartContainerAsync(_id, new ContainerStartParameters());
        }

        public async Task Stop(IDockerClient client)
        {
            await client.Containers.StopContainerAsync(_id, new ContainerStopParameters
            {
                WaitBeforeKillSeconds = 30
            },
            CancellationToken.None);
        }

        public async Task Delete(IDockerClient client)
        {
            await client.Containers.RemoveContainerAsync(_id, new ContainerRemoveParameters() { Force = true });
        }
    }
}

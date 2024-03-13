using Docker.DotNet;

namespace BlazorApp2.Domain
{
    public interface IDockerService
    {
        string Name { get; }
        int Port { get; }

        public Task CreateImage(IDockerClient client);

        public Task CreateContainer(IDockerClient client);

        public Task Start(IDockerClient client);

        public Task Stop(IDockerClient client);

        public Task Delete(IDockerClient client);
    }
}

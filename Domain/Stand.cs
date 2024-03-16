using Docker.DotNet;

namespace BlazorApp2.Domain
{
    public class Stand
    {
        DockerClient client;
        public NodeRed nodeRed;

        public Stand()
        {
            client = new DockerClientConfiguration().CreateClient();
            nodeRed = new NodeRed();
        }
        
        public Task CreateImages()
        {
            nodeRed.CreateImage(client);
            // ToDo create other images
            return Task.CompletedTask;
        }

        public Task RunNodeRed()
        {
            return Run(nodeRed);
        }

        public async Task Run(IDockerService app)
        {
            await app.CreateContainer(client);
            await app.Start(client);
        }

        public async Task StopNodeRed()
        {
            await nodeRed.Stop(client);
            await nodeRed.Delete(client);
        }


    }
}

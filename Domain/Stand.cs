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

        public Task Run(IDockerService app)
        {
            app.CreateContainer(client);
            app.Start(client);
            return Task.CompletedTask; // FIXME
        }


    }
}

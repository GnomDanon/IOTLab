using Docker.DotNet;

namespace BlazorApp2.Domain
{
    public class Stand
    {
        DockerClient client;
        public NodeRed nodeRed;
        public Mosquitto mqtt;
        public OpenHab openHab;

        public Stand()
        {
            client = new DockerClientConfiguration().CreateClient();
            nodeRed = new NodeRed();
            mqtt = new Mosquitto();
            openHab = new OpenHab();
        }
        
        public Task CreateImages()
        {
            nodeRed.CreateImage(client);
            mqtt.CreateImage(client);
            openHab.CreateImage(client);
            return Task.CompletedTask;
        }

        public Task RunNodeRed()
        {
            return Run(nodeRed);
        }

        public async Task Run(DockerService app)
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

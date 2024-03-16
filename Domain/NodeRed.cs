namespace BlazorApp2.Domain
{
    public class NodeRed: DockerService
    {
       public NodeRed()
        {
            Port = 1880;
            Name = "nodered/node-red";
        }
    }
}

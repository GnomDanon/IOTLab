namespace BlazorApp2.Domain
{
    public class OpenHab: DockerService
    {
        public OpenHab()
        {
            Port = 1884;
            Name = "openhab/openhab";
        }
    }
}

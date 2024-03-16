namespace BlazorApp2.Domain
{
    public class Mosquitto : DockerService
    {
        public Mosquitto()
        {
            Port = 1883;
            Name = "eclipse-mosquitto";
        }
    }
}

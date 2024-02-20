namespace RestAPI.Service
{
    public interface ISerializer
    {
        T Deserialize<T>(string data);
        string Serialize<T>(T data);
    }
}
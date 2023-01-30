namespace EC2.Models
{
    /// <summary>
    /// test model for automapper genertic type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MapperTest<T>
    {
        public int Id { get; set; }
        public T? Data { get; set; }
    }
}

namespace EC2.Models
{
    /// <summary>
    /// 用來預備讓Service回傳的資料型態
    /// </summary>
    public class ProductServiceResponse
    {
        public bool IsSucessful { get; set; }
        public dynamic Result { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
    }
}

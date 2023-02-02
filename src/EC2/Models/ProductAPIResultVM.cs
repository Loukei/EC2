namespace EC2.Models
{
    /// <summary>
    /// 用來預備讓<see cref="Controllers.Implement.ProductController"/>回傳的資料型態
    /// </summary>
    public class ProductAPIResultVM
    {
        public bool IsSucessful { get; set; }
        public dynamic Result { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
    }
}

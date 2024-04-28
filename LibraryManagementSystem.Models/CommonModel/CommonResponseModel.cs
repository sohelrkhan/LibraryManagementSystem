namespace LibraryManagementSystem.Models.CommonModel
{
    public class CommonResponseModel<T>
    {
        public T? Resource { get; set; }
        public string? Message { get; set; }
        public bool? Success { get; set; }
    }

    public class CommonResponseModel
    {
        public string? Message { get; set; }
        public bool? Success { get; set; }
    }
}

namespace CrudPersonasWebApi.ResponseModel
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            Succeeded = true;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}

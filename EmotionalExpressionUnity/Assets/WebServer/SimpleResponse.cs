namespace Assets.WebServer {
    // A simple response class allowing us to easily set the status code and 
    // message of the responses from our service.
    public class SimpleResponse {
        public int Status {get; set;}
        public string Message { get; set;}

        public SimpleResponse(int status, string message) {
            this.Status = status;
            this.Message = message;
        }
    }
}
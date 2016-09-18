namespace Assets.WebServer {
    // A simple response class allowing us to easily set the status code and 
    // body of the responses from our service.
    public class SimpleResponse {
        public int Status {get; set;}
        public string Response { get; set;}

        public SimpleResponse(int status, string response) {
            this.Status = status;
            this.Response = response;
        }
    }
}
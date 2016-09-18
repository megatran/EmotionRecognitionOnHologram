using System.Net;
using Assets.WebServer;
using UnityEngine;

namespace Assets { 
    public class EmotionProcessor : MonoBehaviour {
        private const string ReceivingUrl = "http://localhost:8080/emotion/";
        private HttpServer _httpServer;
        
        public void Start () {
            // Ensure that we can receive http requests through HttpListener
            if (!HttpListener.IsSupported)
            {
                Debug.Log("HttpListener is not supported--the application cannot run properly.\n" +
                          "Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            _httpServer = new HttpServer(EmotionProcessor.ProcessEmotionRequest, ReceivingUrl);
            _httpServer.Run();
            Debug.Log("Now accepting HTTP requests at " + ReceivingUrl);
        }

        // This medthod is executed by httpServer when it receives a request.
        // Params:
        //   HttpListenerRequest request: the HTTP request received by the server
        // Returns:
        //   SimpleResponse: the status code for the response, and a string to return
        public static SimpleResponse ProcessEmotionRequest(HttpListenerRequest request) {
            int status = 200;
            string response = "It's working!";

            // TODO: deserialize and process emotion data

            return new SimpleResponse(status, response);
        }

        public void OnDestroy() {
            if (_httpServer != null) {
                _httpServer.Stop();
            }
        }
    }
}
using System;
using System.Net;
using System.Text;
using System.Threading;

namespace Assets.WebServer {
    // Source: https://codehosting.net/blog/BlogEngine/post/Simple-C-Web-Server
    public class HttpServer {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, SimpleResponse> _responderMethod;

        public HttpServer(string[] prefixes, Func<HttpListenerRequest, SimpleResponse> method) {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            // URI prefixes are required, for example 
            // "http://localhost:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // A responder method is required
            if (method == null) {
                throw new ArgumentException("method");
            }

            foreach (string s in prefixes) {
                _listener.Prefixes.Add(s);
            }

            _responderMethod = method;
            _listener.Start();
        }

        public HttpServer(Func<HttpListenerRequest, SimpleResponse> method, params string[] prefixes)
            : this(prefixes, method) { }

        public void Run() {
            ThreadPool.QueueUserWorkItem((o) => {
                Console.WriteLine("Web server running...");
                try {
                    while (_listener.IsListening) {
                        ThreadPool.QueueUserWorkItem((c) => {
                            var ctx = c as HttpListenerContext;

                            try {
                                SimpleResponse resp = _responderMethod(ctx.Request);

                                ctx.Response.StatusCode = resp.Status;
                                string rstr = String.Format("{{ \"status\": {0}, \"message\": \"{1}\" }}", resp.Status, resp.Message);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                                ctx.Response.ContentType = "application/json";
                            } catch (Exception e) {
                                Console.WriteLine("Exception prepraring response string : {0}", e);
                            } finally {
                                ctx.Response.OutputStream.Close();
                            }
                        }, 
                        _listener.GetContext());
                    }
                } catch (Exception e) {
                    Console.WriteLine("Exception in web server: {0}", e);
                }
            });
        }

        public void Stop() {
            _listener.Stop();
            _listener.Close();
        }
    }
}
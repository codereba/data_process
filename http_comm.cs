using System;
using System.Windows.Forms; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace data_process
{

    public struct TO_SERVER_INFO
    {
        public string name;
        public string value; 
    };

    class http_comm
    {
        public static Int32 to_server(string url, ref TO_SERVER_INFO[] infos)
        {
            Int32 ret = 0; 
            //string post_text; 
            string reponse_text; 
            //string name; 
            //string value; 
            //byte[] post_data = null; 

            do
            {
                try
                {
                    //ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                    //ServicePointManager.DefaultConnectionLimit = 100;
                    //txtAbstract.Clear();

                    //Create the Web request
                    HttpWebRequest request = null;

                    try
                    {
                        request = (HttpWebRequest)WebRequest.Create(url);
                    }
                    catch (HttpListenerException httpEx)
                    {
                        System.Diagnostics.Debug.WriteLine("create http request error{0}\n", httpEx.Message);
                        ret = -1;
                    }

                    if (ret != 0)
                    {
                        break;
                    }

                    //post_text = "";
                    for (Int32 i = 0; i < infos.Length; i++)
                    {
                        do
                        {
                            if (0 == infos[i].name.Length)
                            {
                                break;
                            }

                            request.Headers.Add(infos[i].name, infos[i].value); 
                        }while( false ); 
                    }

                    //if (post_data == null)
                    //{
                    //    ret = -1; 
                    //    break;
                    //}

                    //string content = post_text; 
                    //try
                    //{
                    //    post_data = Encoding.UTF8.GetBytes(content);
                    //}
                    //catch (Exception sysEx)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("create http request error{0}\n", sysEx.Message);
                    //    ret = -1;
                    //}

                    //if (ret != 0)
                    //{
                    //    break;
                    //}

                    //HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                    //request.Headers.Add("Authorization", $"AWS {AccessKeyID}:{SignAuthorizationString(request, SecretKey)}");
 
                    request.Method = "POST";
                    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0b; Windows NT 6.0)";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Credentials = CredentialCache.DefaultNetworkCredentials;
                    request.ContentLength = 0;
                    request.Timeout = 1000000;
                    request.KeepAlive = false;

                    //sends the data onto the http pipe
                    Stream sw = null;
                    try
                    {
                        sw = request.GetRequestStream();
                    }
                    catch (Exception streamEx)
                    {
                        System.Diagnostics.Debug.WriteLine("receive response error{0}\n", streamEx.Message);
                        ret = -1; 
                    }

                    if (ret != 0)
                    {
                        break;
                    }

                    //catches the http response
                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (WebException WebEx)
                    {
                        MessageBox.Show(WebEx.Message);
                        ret = -1; 
                    }

                    if (ret != 0)
                    {
                        break;
                    }

                    //writes the respose to the text field
                    if (response != null)
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            reponse_text = sr.ReadLine(); 
                        }
                    }

                    //post_data = data_encoding.GetBytes(post_text);
                    //HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    //webReq.Method = "POST";
                    //webReq.ContentType = "application/x-www-form-urlencoded";

                    //webReq.ContentLength = post_data.Length;
                    //Stream newStream = webReq.GetRequestStream();
                    //newStream.Write(post_data, 0, post_data.Length);
                    //newStream.Close();
                    //HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                    //StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                    //reponse_text = sr.ReadToEnd();
                    //sr.Close();
                    //response.Close();
                    //newStream.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("error{0}\n", ex.Message);
                    ret = -1; 
                }
            } while (false); 

            return ret;
        }
    }
    
//    
//        {
//            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
//            HttpWebResponse rps = (HttpWebResponse)req.GetResponse();
            
//            Stream st = rps.GetResponseStream();
//            Stream so = new FileStream(filename, FileMode.Create);
            
//            byte[] by = new byte[rps.ContentLength];
//            int osize = st.Read(by, 0, (int)by.Length);
//            while (osize > 0)
//            {
//                so.Write(by, 0, osize);
//                osize = st.Read(by, 0, (int)by.Length);
//            }
//            so.Close();
//            st.Close();
//        }

//        Int32 htpp_request()
//{
//    UriBuilder uri = new UriBuilder("http://www.cnblogs.com/xiaokang088/rss.aspx");
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.Uri);

//            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//            var stream = response.GetResponseStream();
//            StreamReader reader = new StreamReader(stream);
//            var str = reader.ReadToEnd();
//}

//    Int32 htpp_request_aio()
//{
//    UriBuilder uri = new UriBuilder("http://www.cnblogs.com/xiaokang088/rss.aspx");
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.Uri);
//            request.BeginGetResponse(new AsyncCallback(handleResponce), request);
//}
//        private void handleResponce(IAsyncResult ar)
//        {
//            HttpWebRequest request = ar.AsyncState as HttpWebRequest;
//            var reponse = request.EndGetResponse(ar) as HttpWebResponse;

//            var stream = reponse.GetResponseStream();
//            StreamReader reader = new StreamReader(stream);
//            string str = reader.ReadToEnd();
//        }
//}

//      Int32 htpp_request()
//{
//      WebClient client = new WebClient();
//            //client.Encoding = Encoding.UTF8;
//            var address = "http://www.cnblogs.com/xiaokang088/rss.aspx";
//            string content = client.DownloadString(address);
//}

//WebRequest req=WebRequest.Create(url);
//WebResponse rep=req.GetResponse();
//Stream webstream=rep.GetResponseStream();
//StreamReader sr=new StreamReader(webstream);
//string br=sr.ReadToEnd();

//HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);                                              myReq.Timeout = timeout;
//HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
//Stream myStream = HttpWResp.GetResponseStream () ;
//StreamReader sr;
//using (sr = new StreamReader(myStream ,encode))
//{
//    strResult = sr.ReadToEnd();
//}
//sr.Close();
//HttpWResp.Close();
//myReq.Abort();

//public static Socket ConnectToSocks5Proxy(string proxyAdress, ushort proxyPort, string destAddress, ushort destPort,
//         string userName, string password)
//        {
//            IPAddress destIP = null;
//            IPAddress proxyIP = null;
//            byte[] request = new byte[257];
//            byte[] response = new byte[257];
//            byte[] tmpBuffer = new byte[40];

//            proxyIP = IPAddress.Parse(proxyAdress);                     

//            destIP = IPAddress.Parse(destAddress);

//            IPEndPoint proxyEndPoint = new IPEndPoint(proxyIP, proxyPort);
          
//            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            s.Connect(proxyEndPoint);

//            String temp;

//            temp = String.Format("CONNECT 127.0.0.1:443 HTTP/1.1\r\nUser-Agent: Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)\r\n Proxy-Connection: Keep-Alive\r\n Proxy-Authorization: Basic YWRyZDExMTphZDgxOA== \r\n\r\n", destAddress, destPort);
//            request = System.Text.Encoding.ASCII.GetBytes(temp);
//            s.Send(request, temp.Length, SocketFlags.None);
//            s.Receive(response); // Get variable length response...
//            String tempstr = System.Text.Encoding.UTF8.GetString(response);
//            if (tempstr.Substring(9, 3) == "200")
//            {
//                return s;
//            }
//            return null;
//        }

}

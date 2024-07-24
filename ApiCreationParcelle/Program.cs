using ApiCreationParcelle.Classes;
using ApiCreationParcelle.Controleurs;
using MySql.Data.MySqlClient;
using System.Net;
using ApiCreationParcelle.Controleurs;
HttpListener listener = new HttpListener();
listener.Prefixes.Add("http://localhost:8080/");
listener.Start();
Console.WriteLine("Start listening on http://localhost:8080/");

while (true)
{    
    HttpListenerContext context = listener.GetContext();
    //create a queue of all the request and for all the request we aplied the function request handler
    ThreadPool.QueueUserWorkItem(o => MainControlleur.MainRequestHandler(context));
}

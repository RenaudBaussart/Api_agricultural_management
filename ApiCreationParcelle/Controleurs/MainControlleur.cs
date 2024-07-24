using Gestion_parcelle.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ApiCreationParcelle.Controleurs;
using System.Text.Json;
using System.Text;
using System.ComponentModel;

namespace ApiCreationParcelle.Controleurs
{
    internal class MainControlleur
    {
        public static void MainRequestHandler(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            string requestMethod = request.HttpMethod;
            string absolutePath = request.Url.AbsolutePath;
            
            List<string> splitedAbsolutePath = new List<string>(absolutePath.Split("/"));
            Console.WriteLine(splitedAbsolutePath[1]);
            string endPointSelector = splitedAbsolutePath[1];
            switch (endPointSelector)
            { 
                case "chemical_list":
                    ControlerChemical.ChemicalRequestHandler(context, "GETList");
                    Console.WriteLine("Logs: Chemical list endpoint");
                    break;

                case "chemical":
                    string primaryKeySelected = splitedAbsolutePath[2];
                    ControlerChemical.ChemicalRequestHandler(context, requestMethod, primaryKeySelected);
                    Console.WriteLine("Logs: chemical endpoint");
                    break;

                default:
                    var DefaultResponse = new
                    {
                        Message = "Ressource not Found",
                    };
                    string defaultJsonRespose = JsonSerializer.Serialize(DefaultResponse);
                    byte[] defaultResponceBytesJson = Encoding.UTF8.GetBytes(defaultJsonRespose);
                    context.Response.OutputStream.Write(defaultResponceBytesJson);
                    context.Response.Close();
                    Console.WriteLine("Response Send");
                    break;
            }
        }
    }
}

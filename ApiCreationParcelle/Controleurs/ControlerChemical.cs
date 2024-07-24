using Gestion_parcelle.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiCreationParcelle.Controleurs
{
    internal class ControlerChemical
    {
        public static void ChemicalRequestHandler(HttpListenerContext context ,string requestMethod,string? primaryKeySelected = null) 
        {
            switch (requestMethod)
            {
                case "GETList":
                    List<ChemicalElement> chemicalElements = new List<ChemicalElement>(ChemicalElement.FetchDataBaseList());
                    string getListjsonRespose = JsonSerializer.Serialize(chemicalElements);
                    byte[] getListresponceBytesJson = Encoding.UTF8.GetBytes(getListjsonRespose);
                    context.Response.OutputStream.Write(getListresponceBytesJson);
                    Console.WriteLine("Response Send");
                    break;
                case "GET":
                    ChemicalElement getSpecificElement = ChemicalElement.FetchDataBaseSpecificId(primaryKeySelected);
                    string getListResponcJson = JsonSerializer.Serialize(getSpecificElement);
                    byte[] getListResponceBytesJson = Encoding.UTF8.GetBytes(getListResponcJson);
                    context.Response.OutputStream.Write(getListResponceBytesJson);
                    Console.WriteLine("Response Send");
                    break;
                case "POST":
                    HttpListenerRequest postRequest = context.Request;
                    Stream postRequestBody = postRequest.InputStream;
                    StreamContent PostContentBody= new StreamContent(context.Request.InputStream);
                    string requestBodyContent;
                    ChemicalElement chemicalElementPosted;
                    try { 
                        using (StreamReader reader = new StreamReader(postRequestBody, Encoding.UTF8))
                        {
                            requestBodyContent = reader.ReadToEnd();
                            chemicalElementPosted = JsonSerializer.Deserialize<ChemicalElement>(requestBodyContent);
                            Console.WriteLine($"{chemicalElementPosted.ElementCode} / {chemicalElementPosted.ElementTag} / {chemicalElementPosted.ElementUnit} added to Data Base");
                            ChemicalElement.AddObjectToDB(chemicalElementPosted.ElementCode, chemicalElementPosted.ElementTag, chemicalElementPosted.ElementUnit);
                            var postSuccesResponse = new
                            {
                                Message = $"object with element code:{chemicalElementPosted.ElementCode} added to db ",
                            };
                            string postSuccesJsonRespose = JsonSerializer.Serialize(postSuccesResponse);
                            byte[] postSuccesByteRespose = Encoding.UTF8.GetBytes(postSuccesJsonRespose);
                            context.Response.OutputStream.Write(postSuccesByteRespose);
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    context.Response.Close();
                    break;
                case "PUT":
                    HttpListenerRequest putRequest = context.Request;
                    Stream putRequestBody = putRequest.InputStream;
                    StreamContent putContentBody = new StreamContent(context.Request.InputStream);
                    string putBodyContent;
                    ChemicalElement chemicalElementToUpdate;
                    try
                    {
                        using (StreamReader reader = new StreamReader(putRequestBody, Encoding.UTF8))
                        {
                            putBodyContent = reader.ReadToEnd();
                            chemicalElementToUpdate = JsonSerializer.Deserialize<ChemicalElement>(putBodyContent);
                            Console.WriteLine($"{chemicalElementToUpdate.ElementCode} / {chemicalElementToUpdate.ElementTag} / {chemicalElementToUpdate.ElementUnit} updated in Data Base");
                            ChemicalElement.UpdateObjectInDB(chemicalElementToUpdate.ElementCode, chemicalElementToUpdate.ElementTag, chemicalElementToUpdate.ElementUnit);
                            var putSuccessResponse = new
                            {
                                Message = $"Object with element code:{chemicalElementToUpdate.ElementCode} updated in db",
                            };
                            string putSuccessJsonResponse = JsonSerializer.Serialize(putSuccessResponse);
                            byte[] putSuccessByteResponse = Encoding.UTF8.GetBytes(putSuccessJsonResponse);
                            context.Response.OutputStream.Write(putSuccessByteResponse);
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    context.Response.Close();
                    break;
                case "DELETE":
                    HttpListenerRequest deleteRequest = context.Request;
                    Stream deleterequestBody = deleteRequest.InputStream;
                    StreamContent contentBody = new StreamContent(context.Request.InputStream);
                    string deleteBodyContent;
                    ChemicalElement chemicalElementToDelete;
                    try
                    {
                        using (StreamReader reader = new StreamReader(deleterequestBody, Encoding.UTF8))
                        {
                            deleteBodyContent = reader.ReadToEnd();
                            chemicalElementToDelete = JsonSerializer.Deserialize<ChemicalElement>(deleteBodyContent);
                            Console.WriteLine($"{chemicalElementToDelete.ElementCode} removed from Data Base");
                            ChemicalElement.RemoveFromDB(chemicalElementToDelete.ElementCode);
                            var deleteSuccesResponse = new
                            {
                                Message = $"Object with element code:{chemicalElementToDelete.ElementCode} deleted from the data base",
                            };
                            string deleteSuccesJsonRespose = JsonSerializer.Serialize(deleteSuccesResponse);
                            byte[] deleteSuccesByteRespose = Encoding.UTF8.GetBytes(deleteSuccesJsonRespose);
                            context.Response.OutputStream.Write(deleteSuccesByteRespose);
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                    context.Response.Close();
                    break;
                default:
                    Console.WriteLine("Error: Method not suported");
                    var DefaultResponse = new
                    {
                        Message = "Method not suported",
                    };
                    string defaultJsonRespose = JsonSerializer.Serialize(DefaultResponse);
                    byte[] defaultResponceBytesJson = Encoding.UTF8.GetBytes(defaultJsonRespose);
                    context.Response.OutputStream.Write(defaultResponceBytesJson);
                    context.Response.Close();
                    Console.WriteLine("Response Send");
                    break;
            }
            context.Response.Close();
            
        }
    }
}

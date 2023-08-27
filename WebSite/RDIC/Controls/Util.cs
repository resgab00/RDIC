using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using RDIC.Models;

namespace RDIC.Controls
{
    public class Util
    {
        public static string CreateXML(Object YourClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
            //string strView = CreateXML(YourClassObject);
        }

        public static Object CreateObject(string XMLString,Object YourClassObject){            
            XmlSerializer oXmlSerializer =new XmlSerializer(YourClassObject.GetType()); 
            //The StringReader will be the stream holder for the existing XML file 
            YourClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString)); 
            //initially deserialized, the data is represented by an object without a defined type 
            return YourClassObject;
        }

        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public static void SendMail(string MailFrom, string MailTo, string Subject, string Body)
        {
            MasterData MD = Data.GetMasterData();
            SmtpClient client = new SmtpClient(MD.SmtpAddress);
            string Password = EncoderHelper.Decod(MD.SmtpPassword);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(MD.SmtpUser, Password);

            MailMessage mm = new MailMessage(MailFrom, MailTo, Subject, Body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mm.IsBodyHtml = true;
            try
            {
                client.Send(mm);
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
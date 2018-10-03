using System;
using System.Net;
using System.Text;


namespace ConsoleApp1
{
   

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite Ip");
            string ip = Console.ReadLine();
            Console.WriteLine("Digete a Porta");
            string door = Console.ReadLine();
            string local_network_print = "http://" + ip + ":" + door + "/";

            //WebServer ws = new WebServer(SendResponse, "http://10.11.0.94:8090/");
            WebServer ws = new WebServer(SendResponse, local_network_print);
            ws.Run();
            Console.WriteLine("Servidor Web Executando na rede em {0} ", local_network_print);
            Console.ReadKey();
            ws.Stop();
        }

        public static string SendResponse(HttpListenerRequest request)
        {


            string people = convert_to_ut8(request.QueryString["pessoa"]);
                      
                   
            TSCLIB_DLL.openport("USB");                                           //Open specified printer driver
            TSCLIB_DLL.setup("25.4", "279.4", "4", "8", "0", "0", "0");                  //Setup the media size and sensor type info
            TSCLIB_DLL.clearbuffer();  //Clear image buffer

            //Label
            TSCLIB_DLL.windowsfont(25, 1200, 36, 90, 3, 0, "ARIAL", "Pessoa: ");  //Draw windows font
            TSCLIB_DLL.windowsfont(75, 1200, 36, 90, 3, 0, "ARIAL", "Data e Hora: ");  //Draw windows font
            TSCLIB_DLL.windowsfont(0, 250, 12, 90, 3, 0, "ARIAL", "POWER RIGHT");  //Draw windows font

            //Paramentros
            TSCLIB_DLL.windowsfont(25, 1000, 36, 90, 3, 0, "ARIAL", people);  //Draw windows font
            TSCLIB_DLL.windowsfont(75, 960, 36, 90, 3, 0, "ARIAL", string.Format("{0}", DateTime.Now));  //Draw windows font

            //Bar Code
            TSCLIB_DLL.barcode("75", "1400", "128", "50", "1", "270", "1", "1", "12345678910"); //Drawing barcode


            TSCLIB_DLL.printlabel("1", "1");  //Print labels
            TSCLIB_DLL.closeport(); //Close specified printer driver   


             Console.WriteLine("Print Ok");
             Console.WriteLine("--------");

            return string.Format("<HTML><BODY>My web page</BODY></HTML>");

        }

         public static string convert_to_ut8(string text){

            byte[] bytes = Encoding.Default.GetBytes(text);

            return Encoding.UTF8.GetString(bytes);
        }
    }
}



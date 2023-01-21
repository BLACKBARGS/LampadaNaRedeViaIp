using System;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Configurações da lâmpada
        LampSettings lamp = new LampSettings()
        {
            IP = "192.168.100.11",
            MAC = "28:6D:CD:07:91:07",
            Port = 8883,
            IsOn = false,
            Color = "white",
            Brightness = 50
        };
        
        string turnOnCommand = "turnOn";
        
        // Comando para desligar a lâmpada
        string turnOffCommand = "turnOff";

        // Comando para mudar a cor da lâmpada
        string changeColorCommand = "color " + lamp.Color;

        // Comando para mudar a brilho da lâmpada
        string changeBrightnessCommand = "brightness " + lamp.Brightness;
        // Menu de opções
        while (true)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1 - Ligar lâmpada");
            Console.WriteLine("2 - Desligar lâmpada");
            Console.WriteLine("3 - Mudar cor da lâmpada");
            Console.WriteLine("4 - Mudar brilho da lâmpada");
            Console.WriteLine("5 - Obter estado atual da lâmpada");
            Console.WriteLine("6 - Sair");

        // Ligando a lâmpada
        using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
        using (NetworkStream stream = client.GetStream())
        {
            byte[] command = Encoding.ASCII.GetBytes(turnOnCommand);
            stream.Write(command, 0, command.Length);
            lamp.IsOn = true;
            Console.WriteLine("Lâmpada ligada!");
        }

        // Mudando a cor da lâmpada
        using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
        using (NetworkStream stream = client.GetStream())
        {
            byte[] command = Encoding.ASCII.GetBytes(changeColorCommand);
            stream.Write(command, 0, command.Length);
            Console.WriteLine("Lâmpada cor alterada para " + lamp.Color);
        }

        // Mudando o brilho da lâmpada
        using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
        using (NetworkStream stream = client.GetStream())
        {
            byte[] command = Encoding.ASCII.GetBytes(changeBrightnessCommand);
            stream.Write(command, 0, command.Length);
            Console.WriteLine("Lâmpada brilho alterado para " + lamp.Brightness);
        }

        // Obtendo o estado atual da lâmpada
        string getStatusCommand = "status";
        using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
        using (NetworkStream stream = client.GetStream())
        {
            byte[] command = Encoding.ASCII.GetBytes(getStatusCommand);
            stream.Write(command, 0, command.Length);
            byte[] response = new byte[1024];
            int bytesRead = stream.Read(response, 0, response.Length);
            string status = Encoding.ASCII.GetString(response, 0, bytesRead);
                        Console.WriteLine("Estado atual da lâmpada: " + status);
        }

        // Desligando a lâmpada
        using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
        using (NetworkStream stream = client.GetStream())
        {
            byte[] command = Encoding.ASCII.GetBytes(turnOffCommand);
            stream.Write(command, 0, command.Length);
            lamp.IsOn = false;
            Console.WriteLine("Lâmpada desligada!");
        }

        Console.WriteLine("Programa finalizado.");
        Console.ReadKey();
        int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] command = Encoding.ASCII.GetBytes(turnOnCommand);
                        stream.Write(command, 0, command.Length);
                        lamp.IsOn = true;
                        Console.WriteLine("Lâmpada ligada!");
                    }
                    break;
                case 2:
                    using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] command = Encoding.ASCII.GetBytes(turnOffCommand);
                        stream.Write(command, 0, command.Length);
                        lamp.IsOn = false;
                        Console.WriteLine("Lâmpada desligada!");
                    }
                    break;
                case 3:
                    Console.WriteLine("Informe a cor desejada (ex: red, green, blue):");
                    string color = Console.ReadLine();
                    changeColorCommand += color;
                    using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] command = Encoding.ASCII.GetBytes(changeColorCommand);
                        stream.Write(command, 0, command.Length);
                        Console.WriteLine("Lâmpada cor alterada para " + color);
                    }
                    break;
                case 4:
                    Console.WriteLine("Informe o brilho desejado (0-100):");
                    int brightness = int.Parse(Console.ReadLine());
                    changeBrightnessCommand += brightness;
                    using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] command = Encoding.ASCII.GetBytes(changeBrightnessCommand);
                        stream.Write(command, 0, command.Length);
                        Console.WriteLine("Lâmpada brilho alterado para " + brightness);
                    }
                    break;
                case 5:
                    using (TcpClient client = new TcpClient(lamp.IP, lamp.Port))
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] command = Encoding.ASCII.GetBytes("status");
                        stream.Write(command, 0, command.Length);
                        byte[] response = new byte[1024];
                        int bytesRead = stream.Read(response, 0, response.Length);
                        string status = Encoding.ASCII.GetString(response, 0, bytesRead);
                        Console.WriteLine("Estado atual da lâmpada: " + status);
                    }
                    break;
                case 6:
                    Console.WriteLine("Programa finalizado.");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Escolha novamente.");
                    break;
            }
        }
    }
}
    


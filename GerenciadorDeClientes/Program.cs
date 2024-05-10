using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.CodeDom;
using System.Runtime.Serialization.Formatters.Binary;

namespace GerenciadorDeClientes
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string Nome;
            public string Email;
            public string CPF;
        }

        static List<Cliente> Clientes = new List<Cliente>();

        static void Main(string[] args)
        {
            Carregar();

            string op;
            bool continuar = true;

            Console.WriteLine("\t\t!!Gerenciador de clientes!!");

            while (continuar)
            {
                Console.Write("\nSelecione uma das opções: \n[1] - Adicionar Cliente\n[2] - Listar Clientes\n[3] - Remover Cliente\n[4] - Sair\nOpção: ");
                op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Add();
                        Salvar();
                        break;
                    case "2":
                        Lista();
                        break;
                    case "3":
                        Remove();
                        Salvar();
                        break;
                    case "4":
                        continuar = false;
                        Console.WriteLine("Pressione ENTER para encerrar!");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Opção invalida... tente novamente!");
                        break;
                }
            }
        }
        static void Add()
        {
            bool Aux = false, repetido = true;
            int i;
            Cliente cliente = new Cliente();
            Console.WriteLine("Nome do cliente: ");
            cliente.Nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.Email = Console.ReadLine();

            while (repetido)
            {
                Aux = false;
                Console.WriteLine("CPF do cliente: ");
                cliente.CPF = Console.ReadLine();

                for(i = 0; i < Clientes.Count; i++)
                {
                    if (Clientes[i].CPF == cliente.CPF)
                    {
                        Console.WriteLine("CPF ja consta na Lista... tente novamente!");
                        Aux = true;
                        break;
                    }
                }
                if (Aux != true)
                    repetido = false;
            }
            Clientes.Add(cliente);
        }

        static void Lista()
        {
            int i;
            if(Clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente foi cadastrado ainda!");
            }
            else
            {
                for (i = 0; i < Clientes.Count; i++)
                {
                    Console.WriteLine("======================================");
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {Clientes[i].Nome}");
                    Console.WriteLine($"Email: {Clientes[i].Email}");
                    Console.WriteLine($"CPF: {Clientes[i].CPF}");
                }
            }
        }

        static void Remove()
        {
            int ID;

            Lista();

            Console.WriteLine("Entre com o ID do cliente que deseja remover: ");
            ID = int.Parse(Console.ReadLine());
            if (ID < 0 || ID > Clientes.Count)
            {
                Console.WriteLine("ID invalido!");
            }
            else
            {
                Clientes.RemoveAt(ID);
            }
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("clientes.data", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, Clientes);

            stream.Close();
        }
        static void Carregar()
        {
            FileStream stream = new FileStream("clientes.data", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter enconder = new BinaryFormatter();

                Clientes = (List<Cliente>)enconder.Deserialize(stream);

                if (Clientes == null)
                {
                    Clientes = new List<Cliente>();
                }
            }
            catch
            {
                Clientes = new List<Cliente>();
            }

            stream.Close();
        }
    }
}

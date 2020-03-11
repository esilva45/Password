using System;

namespace Password {
    class Program {
        static void Main(string[] args) {

            try {
                string pass = Convert.ToString(args[0]);

                if (pass != "") {
                    Console.WriteLine("Senha:" + pass + " Criptografia: " + SecurePassword.Encrypt(pass));
                } else {
                    Console.WriteLine("Informe a senha");
                }
            } catch {
                Console.WriteLine("Informe a senha");
            }
        }
    }
}

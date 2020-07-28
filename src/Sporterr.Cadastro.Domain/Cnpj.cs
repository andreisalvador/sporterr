using System;

namespace Sporterr.Cadastro.Domain
{
    public struct Cnpj
    {
        private static readonly int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        public string Value { get; private set; }

        private Cnpj(string value)
        {
            Value = value;
        }

        public static implicit operator Cnpj(string value)
            => Parse(value);

        public static bool TryParse(string value, out Cnpj cnpj)
        {
            cnpj = default;

            if (IsValid(value))
            {
                cnpj = new Cnpj(value);
                return true;
            }

            return false;
        }

        public static Cnpj Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            if (TryParse(value, out Cnpj cnpj))
                return cnpj;

            throw new ArgumentException("Parâmetro de entrada inválido para geração de Cnpj.");
        }

        public static bool IsValid(string cnpj)
        {
            int soma;
            int resto;
            int digito;
            Span<char> numerosCnpj;

            if (HasMask(cnpj))
                numerosCnpj = RemoveMask(ref cnpj);
            else
                numerosCnpj = cnpj.ToCharArray();

            if (numerosCnpj.Length != 14)
                return false;

            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += (numerosCnpj[i] - '0') * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto;

            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += (i == 12 ? digito : (numerosCnpj[i] - '0')) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            
            return numerosCnpj[12] - '0' == digito && numerosCnpj[13] - '0' == resto;
        }

        private static Span<char> RemoveMask(ref string cnpj)
        {
            Span<char> cnpjNumbers = stackalloc char[14];
            int index = 0;

            for (int i = 0; i < cnpj.Length; i++)
                if (char.IsNumber(cnpj[i]))
                    cnpjNumbers[index++] = cnpj[i];

            return cnpjNumbers.ToArray();
        }

        private static bool HasMask(string value) => value.Contains('.');
    }
}

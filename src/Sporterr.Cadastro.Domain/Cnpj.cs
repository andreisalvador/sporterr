using System;

namespace Sporterr.Cadastro.Domain
{
    public struct Cnpj
    {
        private static readonly byte[] multiplicador1 = new byte[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly byte[] multiplicador2 = new byte[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
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
            ReadOnlySpan<char> numerosCnpj;

            if (HasMask(cnpj))
                numerosCnpj = RemoveMask(cnpj);
            else
                numerosCnpj = cnpj.AsSpan();

            if (numerosCnpj.Length != 14)
                return false;

            soma = 0;

            for (byte i = 0; i < 12; i++)
                soma += (numerosCnpj[i] - '0') * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto;

            soma = 0;

            for (byte i = 0; i < 13; i++)
                soma += (i == 12 ? digito : (numerosCnpj[i] - '0')) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            return numerosCnpj[12] - '0' == digito && numerosCnpj[13] - '0' == resto;
        }
        private static Span<char> RemoveMask(string cnpj)
        {
            Span<char> cnpjNumbers = new char[14];
            int index = 0;

            for (int i = 0; i < cnpj.Length; i++)
            {
                int digit = cnpj[i] - '0';

                if (digit >= 0 && digit <= 9)
                    cnpjNumbers[index++] = cnpj[i];
            }

            return cnpjNumbers;
        }

        private static bool HasMask(string value) => value.Contains('.');
    }
}

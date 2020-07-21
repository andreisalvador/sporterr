using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

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
            string digito;
            char[] numerosCnpj;
            ReadOnlySpan<char> tempCnpj;

            if (HasMask(cnpj))
                numerosCnpj = RemoveMask(cnpj);
            else
                numerosCnpj = cnpj.ToCharArray();

            if (numerosCnpj.Count() != 14)
                return false;

            tempCnpj = numerosCnpj.AsSpan(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += (tempCnpj[i] - '0') * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj.ToString() + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += (tempCnpj[i] - '0') * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        private static char[] RemoveMask(string cnpj)
        {
            char[] cnpjNumbers = new char[14];
            int index = 0;

            for (int i = 0; i < cnpj.Length; i++)
                if (char.IsNumber(cnpj[i]))
                    cnpjNumbers[index++] = cnpj[i];

            return cnpjNumbers;
        }

        private static bool HasMask(string value) => value.Contains('.');

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pokemon.Util;

namespace Pokemon.Packets
{
    public static class Rsa
    {
        #region Constants
        static BigInteger otServerP = new BigInteger("14299623962416399520070177382898895550795403345466153217470516082934737582776038882967213386204600674145392845853859217990626450972452084065728686565928113", 10);
        static BigInteger otServerQ = new BigInteger("7630979195970404721891201847792002125535401292779123937207447574596692788513647179235335529307251350570728407373705564708871762033017096809910315212884101", 10);
        static BigInteger otServerD = new BigInteger("46730330223584118622160180015036832148732986808519344675210555262940258739805766860224610646919605860206328024326703361630109888417839241959507572247284807035235569619173792292786907845791904955103601652822519121908367187885509270025388641700821735345222087940578381210879116823013776808975766851829020659073", 10);
        static BigInteger otServerM = new BigInteger(Constants.RSAKey.OpenTibia, 10);
        static BigInteger otServerE = new BigInteger("65537", 10);
        static BigInteger otServerDP = new BigInteger("11141736698610418925078406669215087697114858422461871124661098818361832856659225315773346115219673296375487744032858798960485665997181641221483584094519937", 10);
        static BigInteger otServerDQ = new BigInteger("4886309137722172729208909250386672706991365415741885286554321031904881408516947737562153523770981322408725111241551398797744838697461929408240938369297973", 10);
        static BigInteger otServerInverseQ = new BigInteger("5610960212328996596431206032772162188356793727360507633581722789998709372832546447914318965787194031968482458122348411654607397146261039733584248408719418", 10);
        static BigInteger cipM = new BigInteger(Constants.RSAKey.RealTibia, 10);
        static BigInteger cipE = new BigInteger("65537", 10);
        #endregion

        #region Public Functions

        public static bool RsaCipEncrypt(ref byte[] buffer, int position)
        {
            return RsaEncrypt(cipE, cipM, ref buffer, position);
        }

        public static bool RsaOTEncrypt(ref byte[] buffer, int position)
        {
            return RsaEncrypt(otServerE, otServerM, ref buffer, position);
        }

        public static bool RsaEncrypt(BigInteger e, BigInteger m, ref byte[] buffer, int position)
        {
            byte[] temp = new byte[128];

            Array.Copy(buffer, position, temp, 0, 128);

            BigInteger input = new BigInteger(temp);
            BigInteger output = input.modPow(e, m);
            // it's sometimes possible for the results to be a byte short
            // and this can break some software so we 0x00 pad the result
            Array.Copy(GetPaddedValue(output), 0, buffer, position, 128);

            return true;
        }

        public static bool RsaOTDecrypt(ref byte[] buffer, int position, int length)
        {
            if (length - position != 128)
                return false;

            byte[] temp = new byte[128];
            Array.Copy(buffer, position, temp, 0, 128);

            BigInteger input = new BigInteger(temp);
            BigInteger output;

            BigInteger m1 = input.modPow(otServerDP, otServerP);
            // m2 = c^dq mod q
            BigInteger m2 = input.modPow(otServerDQ, otServerQ);
            BigInteger h;

            if (m2 > m1)
            {
                // thanks to benm!
                h = otServerP - ((m2 - m1) * otServerInverseQ % otServerP);
                output = m2 + otServerQ * h;
            }
            else
            {
                // h = (m1 - m2) * qInv mod p
                h = (m1 - m2) * otServerInverseQ % otServerP;
                // m = m2 + q * h;
                output = m2 + otServerQ * h;
            }

            Array.Copy(GetPaddedValue(output), 0, buffer, position, 128);
            return true;
        }
        #endregion

        #region Private Functions

        private static byte[] GetPaddedValue(BigInteger value)
        {
            byte[] result = value.getBytes();

            int length = (1024 >> 3);
            if (result.Length >= length)
                return result;

            // left-pad 0x00 value on the result (same integer, correct length)
            byte[] padded = new byte[length];
            System.Buffer.BlockCopy(result, 0, padded, (length - result.Length), result.Length);
            // temporary result may contain decrypted (plaintext) data, clear it
            Array.Clear(result, 0, result.Length);
            return padded;
        }

        #endregion
    }
}

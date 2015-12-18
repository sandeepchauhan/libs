using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications
{
    public class ICICICCStmtParser
    {
        private static List<string> GetSanitizedAllWords(string filePath)
        {
            List<string> allWords = new List<string>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string l in lines)
            {
                if (!string.IsNullOrWhiteSpace(l))
                {
                    allWords.AddRange(l.Split(null));
                }
            }

            List<string> sanitizedWords = new List<string>();
            foreach (string word in allWords)
            {
                sanitizedWords.AddRange(BreakWordIfItContainsReferenceNumber(word));
            }

            allWords = null;
            int indexOfFirstWordOfFirstTransaction = 0;
            for (int i = 0; i < sanitizedWords.Count; i++)
            {
                DateTime localDate;
                CultureInfo ci = CultureInfo.InvariantCulture;
                if (DateTime.TryParseExact(sanitizedWords[i], "dd/MM/yyyy", ci, DateTimeStyles.None, out localDate))
                {
                    indexOfFirstWordOfFirstTransaction = i;
                }
                else if (IsTransactionRefNumber(sanitizedWords[i]))
                {
                    break;
                }
            }

            sanitizedWords = sanitizedWords.Skip(indexOfFirstWordOfFirstTransaction).ToList();
            return sanitizedWords;
        }

        public static List<BankTransaction> Parse(string filePath)
        {
            List<string> AllWords = GetSanitizedAllWords(filePath);
            List<BankTransaction> AllTransactions = new List<BankTransaction>();
            BankTransaction currentTransaction = null;
            float lastAmount = 0;
            string lastDetails = string.Empty;
            bool isCreditTransaction = false;
            foreach (string word in AllWords)
            {
                DateTime localDate;
                float localAmount;
                CultureInfo ci = CultureInfo.InvariantCulture;
                if (DateTime.TryParseExact(word, "dd/MM/yyyy", ci, DateTimeStyles.None, out localDate))
                {
                    if (currentTransaction == null)
                    {
                        currentTransaction = new BankTransaction();
                        currentTransaction.Date = localDate;
                    }
                    else
                    {
                        currentTransaction.Amount = (int)lastAmount;
                        currentTransaction.Details = lastDetails;
                        if (isCreditTransaction)
                        {
                            currentTransaction.Amount *= -1;
                            currentTransaction.Type = BankTransactionType.Credit;
                        }

                        AllTransactions.Add(currentTransaction);
                        currentTransaction = new BankTransaction();
                        currentTransaction.Date = localDate;
                        isCreditTransaction = false;
                        lastDetails = string.Empty;
                    }
                }
                else if (IsTransactionRefNumber(word))
                {
                    currentTransaction.ReferenceNumber = word;
                }
                else if (word.Split(new char[] { '.' }).Length == 2 && word.Split(new char[] { '.' })[1].Length == 2 && float.TryParse(word, out localAmount))
                {
                    lastAmount = localAmount;
                }
                else if (word.Equals("CR"))
                {
                    isCreditTransaction = true;
                }
                else
                {
                    lastDetails += word + " ";
                }
            }

            currentTransaction.Amount = (int)lastAmount;
            if (lastDetails.Length > 70)
            {
                lastDetails = lastDetails.Substring(0, 70);
            }

            currentTransaction.Details = lastDetails;
            if (isCreditTransaction)
            {
                currentTransaction.Amount *= -1;
                currentTransaction.Type = BankTransactionType.Credit;
            }

            AllTransactions.Add(currentTransaction);

            AllTransactions.ForEach(x =>
            {
                x.InstrumentType = BankTransactionInstrumentType.CreditCard;
                x.SourceFile = filePath.Split(new string[] { "ccs" }, StringSplitOptions.RemoveEmptyEntries)[1];
            });

            return AllTransactions;
        }

        private static bool IsTransactionRefNumber(string word)
        {
            if (word.Length != 23)
            {
                return false;
            }

            foreach (char c in word)
            {
                ushort num;
                if (!UInt16.TryParse(c.ToString(), out num))
                {
                    return false;
                }
            }

            return true;
        }

        private static List<string> BreakWordIfItContainsReferenceNumber(string word)
        {
            int wordLength = word.Length;
            if (wordLength > 23)
            {
                int maxCharsToSkip = wordLength - 23;
                for (int i = 1; i <= maxCharsToSkip; i++)
                {
                    if (IsTransactionRefNumber(word.Substring(i)))
                    {
                        string word1 = word.Substring(0, i);
                        string word2 = word.Substring(i);
                        return new List<string>() { word1, word2 };
                    }
                }

            }

            return new List<string>() { word };
        }
    }
}

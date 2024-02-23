using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;

namespace HomeBankingMindHub.Services
{
    public class TransactionsService : ITransactionsService
    {
        private IClientRepository _clientRepository;
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;

        public TransactionsService(IClientRepository clientRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public string Transaction(TransferDTO transferDTO, string email)
        {
            //VALIDACIONES DE NULOS
                #region
                Client client = _clientRepository.FindByEmail(email);

                if (client == null)
                {
                    return "No existe el cliente";
                }

                if (transferDTO.FromAccountNumber == string.Empty || transferDTO.ToAccountNumber == string.Empty)
                {
                    return "Cuenta de origen o cuenta de destino no proporcionada.";
                }

                if (transferDTO.FromAccountNumber == transferDTO.ToAccountNumber)
                {
                    return "No se permite la transferencia a la misma cuenta.";
                }

                if (transferDTO.Amount == 0 || transferDTO.Description == string.Empty)
                {
                    return "Monto o descripcion no proporcionados.";
                }

                if(transferDTO.Amount < 0)
                {
                    return "Monto invalido";
                }
                #endregion

                //buscamos las cuentas
                Account fromAccount = _accountRepository.GetAccountByNumber(transferDTO.FromAccountNumber);
                if (fromAccount == null)
                {
                    return "Cuenta de origen no existe";
                }

                //controlamos el monto
                if (fromAccount.Balance < transferDTO.Amount)
                {
                    return "Fondos insuficientes";
                }

                //buscamos la cuenta de destino
                Account toAccount = _accountRepository.GetAccountByNumber(transferDTO.ToAccountNumber);
                if (toAccount == null)
                {
                    return "Cuenta de destino no existe";
                }

                //demas logica para guardado
                //comenzamos con la inserrcion de las 2 transacciones realizadas
                //desde toAccount se debe generar un debito por lo tanto lo multiplicamos por -1
                _transactionRepository.Save(new Transaction
                {
                    Type = TransactionType.DEBIT.ToString(),
                    Amount = transferDTO.Amount * -1,
                    Description = transferDTO.Description + " " + toAccount.Number,
                    AccountId = fromAccount.Id,
                    Date = DateTime.Now,
                });

                //ahora una credito para la cuenta fromAccount
                _transactionRepository.Save(new Transaction
                {
                    Type = TransactionType.CREDIT.ToString(),
                    Amount = transferDTO.Amount,
                    Description = transferDTO.Description + " " + fromAccount.Number,
                    AccountId = toAccount.Id,
                    Date = DateTime.Now,
                });

                //seteamos los valores de las cuentas, a la ccuenta de origen le restamos el monto
                fromAccount.Balance = fromAccount.Balance - transferDTO.Amount;
                //actualizamos la cuenta de origen
                _accountRepository.Save(fromAccount);

                //a la cuenta de destino le sumamos el monto
                toAccount.Balance = toAccount.Balance + transferDTO.Amount;
                //actualizamos la cuenta de destino
                _accountRepository.Save(toAccount);
            return "ok";
        }
    }
}

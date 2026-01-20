import { useEffect, useState } from "react";
import { getTransactions, deleteTransaction } from "../../api/transactions.api";
import type { Transaction } from "../../models/Transaction";
import TransactionForm from "./TransactionForm";

export default function TransactionsPage() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  const loadTransactions = async () => {
    const data = await getTransactions();
    setTransactions(data);
  };
  
  useEffect(() => {
    (async () => {
      const data = await getTransactions();
      setTransactions(data);
    })();
  }, []);

  const handleDelete = async (id: number) => {
    if (!confirm("Excluir transação?")) return;

    await deleteTransaction(id);
    loadTransactions();
  };

  return (
    <div>
      <h1>Transações</h1>

      <TransactionForm onCreated={loadTransactions} />

      <table>
        <thead>
          <tr>
            <th>Descrição</th>
            <th>Valor</th>
            <th>Tipo</th>
            <th>Categoria</th>
            <th>Pessoa</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {transactions.map(t => (
            <tr key={t.id}>
              <td>{t.descricao}</td>
              <td>R$ {t.valor.toFixed(2)}</td>
              <td>{t.tipo}</td>
              <td>{t.categoria}</td>
              <td>{t.pessoa}</td>
              <td>
                <button onClick={() => handleDelete(t.id)}>
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
